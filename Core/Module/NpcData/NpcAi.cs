using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CategoryData;
using Core.Module.CharacterData;
using Core.Module.DoorData;
using Core.Module.ItemData;
using Core.Module.FStringData;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Handlers;
using Core.Module.NpcAi.Models;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Google.Protobuf.WellKnownTypes;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using static NLog.LayoutRenderers.Wrappers.ReplaceLayoutRendererWrapper;

namespace Core.Module.NpcData
{
    public class NpcAi
    {
        private readonly NpcInstance _npcInstance;
        private readonly ItemDataInit _itemInit;
        private readonly WorldInit _worldInit;
        private readonly FStringInit _fString;
        private readonly DefaultNpc _defaultNpc;
        private readonly ConcurrentDictionary<int, Task> _tasks;
        private readonly NpcAiTeleport _aiTeleport;
        private readonly NpcAiSell _npcAiSell;
        private int _additionalTime;
        private readonly CancellationTokenSource _cts;
        public NpcAiSm Sm { get; } //PTS object AI require

        public DefaultNpc GetDefaultNpc() => _defaultNpc;
        public NpcInstance NpcInstance() => _npcInstance;
        
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _aiTeleport = new NpcAiTeleport(this);
            _npcAiSell = new NpcAiSell(this);
            _worldInit = npcInstance.ServiceProvider.GetRequiredService<WorldInit>();
            _fString = npcInstance.ServiceProvider.GetRequiredService<FStringInit>();
            _itemInit = npcInstance.ServiceProvider.GetRequiredService<ItemDataInit>();            
            _tasks = new ConcurrentDictionary<int, Task>();
            _cts = new CancellationTokenSource();
            var npcName = _npcInstance.GetStat().Name;
            var npcType = _npcInstance.GetStat().Type;
            _defaultNpc = NpcHandler.GetNpcHandler(npcName, npcType);
            
            var npcAiData = _npcInstance.GetStat().NpcAiData;
            NpcAiDefault.SetDefaultAiParams(_defaultNpc, npcAiData);
            
            Sm = new NpcAiSm
            {
                Level = _npcInstance.Level,
                Race = GetNpcRaceId(_npcInstance.GetStat().Race),
                Name = _npcInstance.GetStat().Name,
                ResidenceId = 1 //Should be Id of ClanHall
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        private int GetNpcRaceId(string race)
        {
            return race switch
            {
                "human" => 0,
                "elf" => 1,
                "darkelf" => 2,
                "orc" => 3,
                "dwarf" => 4,
                _ => -1
            };
        }
        
        public void Created()
        {
            _defaultNpc.MySelf = this;
            _defaultNpc.Created();
            _defaultNpc.NoDesire();
        }

        public void NoDesire()
        {
            _defaultNpc.NoDesire();
        }

        public void Attacked(PlayerInstance playerInstance)
        {
            var attacker = new Talker(playerInstance);
            _npcInstance.NpcDesire().AddDesire(Desire.AttackDesire, playerInstance);
            //_defaultNpc.Attacked(attacker, damage);
        }

        public async Task Talked(PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            await _defaultNpc.Talked(talker);
        }
        
        public void AddTimerEx(int timerId, int delay)
        {
            if (_tasks.ContainsKey(timerId))
            {
                return;
            }
            var currentTimer = TaskManagerScheduler.ScheduleAtFixed(() =>
            {
                _tasks.TryRemove(timerId, out _);
                _defaultNpc.TimerFiredEx(timerId);
            }, delay + _additionalTime, _cts.Token);
            _tasks.TryAdd(timerId, currentTimer);
            _additionalTime = 0;
        }
        
        public async Task AddEffectActionDesire (NpcAiSm sm, int actionId, int moveAround, int desire)
        {
            await _npcInstance.NpcDesire().AddEffectActionDesire(actionId, moveAround, desire);
            //await _npcInstance.SendToKnownPlayers(new SocialAction(_npcInstance.ObjectId, actionId));
        }

        public async Task AddMoveAroundDesire(int moveAround, int desire)
        {
            await _npcInstance.NpcDesire().AddMoveAroundDesire(moveAround, desire);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="fnHi"></param>
        public async Task ShowPage(Talker talker, string fnHi)
        {
            var player = (PlayerInstance) _worldInit.GetWorldObject(talker.ObjectId);
            await _npcInstance.ShowPage(player, fnHi);
        }
        public async Task ShowHTML(Talker talker, string htmlText)
        {
            var player = (PlayerInstance) _worldInit.GetWorldObject(talker.ObjectId);
            await _npcInstance.ShowHTML(player, htmlText);
        }
        
        public async Task Teleport(Talker talker, IList<TeleportList> position, string shopName, string empty, string s,
            string empty1, int itemId, string itemName)
        {
            await _npcInstance.NpcTeleport().Teleport(talker, position, shopName, string.Empty, string.Empty,
                string.Empty, itemId, itemName);
        }

        public async Task TeleportRequested(PlayerInstance playerInstance)
        {
            await _aiTeleport.TeleportRequested(playerInstance);
        }

        public bool CastleIsUnderSiege()
        {
            return false;
        }

        public async Task CastleGateOpenClose2(string doorName1, int openClose)
        {
            var doorInit = _npcInstance.ServiceProvider.GetRequiredService<DoorDataInit>();
            var doorInstance = doorInit.GetDoorInstance(doorName1);
            foreach (var (objectId, worldObject) in doorInstance.DoorKnownList().GetKnownObjects())
            {
                if (worldObject is PlayerInstance targetInstance)
                {
                    await targetInstance.SendPacketAsync(new DoorStatusUpdate(doorInstance, targetInstance, openClose));
                }
            }
        }

        public async Task InstantTeleport(Talker talker, int posX01, int posY01, int posZ01)
        {
            await _npcInstance.NpcTeleport().TeleportToLocation(talker.PlayerInstance, posX01, posY01, posZ01);
        }

        public int OwnItemCount(Talker talker, int itemId)
        {
            return talker.PlayerInstance.PlayerInventory().GetItemInstance(itemId).Amount;
        }

        public int OwnItemCount(Talker talker, string itemName)
        {
            var itemId = _itemInit.GetItemByName(itemName).ItemId;
            return OwnItemCount(talker, itemId);
        }

        public string MakeFString(int stringId, string empty, string s, string empty1, string s1, string empty2)
        {
            return _fString.GetFString(i);
        }

        public async Task ShowSkillList(Talker talker, string empty)
        {
            var player = (PlayerInstance) _worldInit.GetWorldObject(talker.ObjectId);
            await _npcInstance.NpcLearnSkill().ShowSkillList(player);
        }

        public async Task ShowGrowSkillMessage(Talker talker, int skillNameId, string empty)
        {
            LoggerManager.Warn("ShowGrowSkillMessage NotImplementedException");
            await Task.FromResult(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="talkerOccupation"></param>
        /// <returns></returns>
        public bool IsInCategory(int groupId, string talkerOccupation)
        {
            var categoryPchInit = _npcInstance.ServiceProvider.GetRequiredService<CategoryPchInit>();
            var categoryName = categoryPchInit.GetCategoryNameById(groupId);
            
            var categoryData = _npcInstance.ServiceProvider.GetRequiredService<CategoryDataInit>();
            var categoryList = categoryData.GetCategoryListByName(categoryName);

            return categoryList.Any(s => s == talkerOccupation);
        }

        public bool IsNewbie(Talker talker)
        {
            return talker.Level < 25;
        }

        public void AddUseSkillDesire(Talker talker, int p1, int p2, int p3, int p4)
        {
            LoggerManager.Warn("AddUseSkillDesire NotImplementedException");
        }

        public void AddAttackDesire(Talker attacked, int i, int f0)
        {
            LoggerManager.Warn("AddAttackDesire NotImplementedException");
        }

        public async Task TalkSelected(PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            await _defaultNpc.TalkSelected(talker);
        }
        
        public async Task MenuSelect(int askId, int replyId, PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            if (_defaultNpc is NewbieGuide newbieGuide)
            {
                await newbieGuide.MenuSelected(talker, askId, replyId);
                return;
            } 
            if (_defaultNpc is Merchant merchant)
            {
                await merchant.MenuSelected(talker, askId, replyId);
                return;
            }
            await _defaultNpc.MenuSelected(talker, askId, replyId, String.Empty);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        public void DeleteRadar(Talker talker, int p1, int p2, int p3, int p4)
        {
            LoggerManager.Warn("DeleteRadar NotImplementedException");
        }

        public async Task ShowEnchantSkillList(Talker talker)
        {
            LoggerManager.Warn("ShowEnchantSkillList NotImplementedException");
            await Task.FromResult(1);
        }

        public async Task ShowEnchantSkillMessage(Talker talker, int skillNameId)
        {
            LoggerManager.Warn("ShowEnchantSkillMessage NotImplementedException");
            await Task.FromResult(1);
        }

        public async Task ShowMultiSell(int i, Talker talker)
        {
            LoggerManager.Warn("ShowMultiSell NotImplementedException");
            await Task.FromResult(1);
        }

        public async Task Sell(Talker talker, IEnumerable<BuySellList> sellList, string shopName, string fnBuy, string empty, string p5)
        {
            await _npcAiSell.ShowSellDialog(talker.PlayerInstance, sellList);
        }

        public async Task SellPreview(Talker talker, IList<BuySellList> sellList0, string shopName, string fnBuy, string empty, string p5)
        {
            LoggerManager.Warn("SellPreview NotImplementedException");
            await Task.FromResult(1);
        }

        public async Task ShowRadar(Talker talker, int v1, int v2, int v3, int v4)
        {
            LoggerManager.Warn("ShowRadar NotImplementedException");
            await Task.FromResult(1);
        }

        public async Task DeleteAllRadar(Talker talker, int v)
        {
            LoggerManager.Warn("DeleteAllRadar NotImplementedException");
            await Task.FromResult(1);
        }

        internal int GetSSQStatus()
        {
            LoggerManager.Warn("GetSSQStatus NotImplementedException");
            return 0;
        }

        internal int GetSSQPart(Talker talker)
        {
            LoggerManager.Warn("GetSSQPart NotImplementedException");
            return 0;
        }

        internal void DeleteItem1(Talker talker, string itemName, int itemCount)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// TODO by default false
        /// </summary>
        /// <param name="talker"></param>
        /// <returns></returns>
        public bool IsMyLord(Talker talker)
        {
            return false;
        }
        
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="talker"></param>
        /// <returns></returns>
        public bool HavePledgePower(Talker talker)
        {
            return false;
        }
        
        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public int Castle_GetPledgeId()
        {
            return 0;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public string Castle_GetPledgeName()
        {
            return "";
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public string Castle_GetOwnerName()
        {
            return "";
        }

        public Int16 Residence_GetTaxRateCurrent()
        {
            return 15;
        }

        public void FHTML_SetFileName(out string fhtml0, string fnWyvernOwner)
        {
            fhtml0 = fnWyvernOwner;
        }

        public void FHTML_SetStr(string fhtml0, string myPledgeName, string castleGetPledgeName)
        {
            throw new NotImplementedException();
        }

        public void FHTML_SetInt(string fhtml0, string currentTaxRate, short residenceGetTaxRateCurrent)
        {
            LoggerManager.Warn("DeleteItem1 NotImplementedException");
        }

        internal async Task ShowSystemMessage(Talker talker, int messageId)
        {
            await talker.PlayerInstance.SendPacketAsync(new SystemMessage((SystemMessageId)messageId));
        }

        internal void SetCurrentQuestID(string v)
        {
            LoggerManager.Warn("SetCurrentQuestID NotImplementedException");
        }

        internal int GetInventoryInfo(Talker talker, int v)
        {
            LoggerManager.Warn("GetInventoryInfo NotImplementedException");
            return 0;
        }

        internal void VoiceEffect(Talker talker, string v1, int v2)
        {
            LoggerManager.Warn("VoiceEffect NotImplementedException");
        }

        internal void GiveItem1(Talker talker, string v1, int v2)
        {
            LoggerManager.Warn("GiveItem1 NotImplementedException");
        }

        internal void IncrementParam(Talker talker, int v1, int v2)
        {
            LoggerManager.Warn("IncrementParam NotImplementedException");
        }

        internal int GetIndexFromCreature(Talker talker)
        {
            LoggerManager.Warn("GetIndexFromCreature NotImplementedException");
            return 0;
        }

        internal int GetMemoStateEx(Talker talker, int v1, string v2)
        {
            LoggerManager.Warn("GetIndexFromCreature NotImplementedException");
            return 0;
        }

        internal void SetMemoStateEx(Talker talker, int v1, string v2, int v3)
        {
            LoggerManager.Warn("SetMemoStateEx NotImplementedException");
        }

        internal bool Castle_GetPledgeId()
        {
            LoggerManager.Warn("Castle_GetPledgeId NotImplementedException");
            return false;
        }

        internal void FHTML_SetFileName(ref string fhtml0, string fileName)
        {
            fhtml0 = Initializer.HtmlCacheInit().GetHtmlText(fileName);
        }

        internal void FHTML_SetStr(ref string fhtml0, string replaceStr, string value)
        {
            fhtml0 = fhtml0.Replace($"<?{replaceStr}?>", value);
        }
        internal void FHTML_SetInt(ref string fhtml0, string replaceStr, int value)
        {
            FHTML_SetStr(ref fhtml0, replaceStr, value.ToString());
        }

        internal string Castle_GetPledgeName()
        {
            LoggerManager.Warn("Castle_GetPledgeName NotImplementedException");
            return "NONE";
        }

        internal string Castle_GetOwnerName()
        {
            LoggerManager.Warn("Castle_GetOwnerName NotImplementedException");
            return "NONE";
        }

        internal int Residence_GetTaxRateCurrent()
        {
            LoggerManager.Warn("Residence_GetTaxRateCurrent NotImplementedException");
            return 0;
        }

        public async Task ShowFHTML(Talker talker, string fhtml0)
        {
            await ShowHTML(talker, fhtml0);
        }

        internal void AddDoNothingDesire(int v1, int v2)
        {
            LoggerManager.Warn("AddDoNothingDesire NotImplementedException");
        }

        internal void AddMoveToDesire(object start_x, object start_y, object start_z, int v)
        {
            LoggerManager.Warn("AddMoveToDesire NotImplementedException");
        }
    }
}