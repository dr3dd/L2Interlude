using Core.Enums;
using Core.Module.CategoryData;
using Core.Module.DoorData;
using Core.Module.FStringData;
using Core.Module.ItemData;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Handlers;
using Core.Module.NpcAi.Models;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly NpcChoice _npcChoice;
        private int _additionalTime;
        private readonly CancellationTokenSource _cts;
        public Talker _talker;
        public NpcAiSm Sm { get; } //PTS object AI require
        public int StartX { get; }
        public int StartY { get; }
        public int StartZ { get; }

        public DefaultNpc GetDefaultNpc() => _defaultNpc;
        public NpcInstance NpcInstance() => _npcInstance;
        public NpcChoice GetChoice() => _npcChoice;
        public Talker GetTalker() => _talker;
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _aiTeleport = new NpcAiTeleport(this);
            _npcAiSell = new NpcAiSell(this);
            _npcChoice = new NpcChoice(this);
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

            StartX = _npcInstance.GetX();
            StartY = _npcInstance.GetY();
            StartZ = _npcInstance.GetZ();
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
            _npcInstance.NpcDesire().StartProcessingDesires();
        }

        public void NoDesire()
        {
            _defaultNpc.NoDesire();
        }

        public void Attacked(PlayerInstance playerInstance, int damage)
        {
            var attacker = new Talker(playerInstance);
            
            var desire = _npcInstance.NpcDesire().GetDesire();
            _defaultNpc.Attacked(attacker, damage);
            //_npcInstance.NpcDesire().AddDesire(Desire.AttackDesire, playerInstance);
            //_defaultNpc.Attacked(attacker, damage);
        }

        public async Task Talked(PlayerInstance playerInstance, bool _from_choice, int _code, int _choiceN)
        {
            _talker = new Talker(playerInstance);
            await _defaultNpc.Talked(_talker, _from_choice, _code, _choiceN);
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
            var itemInstance = talker.PlayerInstance.PlayerInventory().GetInventoryItemByItemId(itemId);
            if (itemInstance != null)
            {
                return itemInstance.Amount;
            }
            return 0;
        }

        public int OwnItemCount(Talker talker, string itemName)
        {
            var itemId = _itemInit.GetItemByName(itemName).ItemId;
            return OwnItemCount(talker, itemId);
        }

        /// <summary>
        /// Get String Name from fstring file
        /// </summary>
        public string MakeFString(int stringId, string empty, string s, string empty1, string s1, string empty2)
        {
            return _fString.GetFString(stringId);
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

        public async Task AddAttackDesire(Talker attacker, int i, int f0)
        {
            await _npcInstance.NpcDesire().AddAttackDesire(attacker, i, f0);
        }

        public async Task TalkSelected(PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            await _defaultNpc.TalkSelected(talker);
        }
        public async Task TalkSelected(string fhtml0, PlayerInstance playerInstance, bool fromChoice, int choice, int option)
        {
            var talker = new Talker(playerInstance);
            await _defaultNpc.TalkSelected(fhtml0, talker, fromChoice, choice, option);
        }
        public async Task QuestAccepted(int questId, PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            await _defaultNpc.QuestAccepted(questId, talker);
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

        public async Task ShowRadar(Talker talker, int x, int y, int z, RadarPositionType type)
        {
            await talker.PlayerInstance.SendPacketAsync(new RadarControl(RadarControlType.SHOW, type, x, y, z));
        }

        public async Task DeleteRadar(Talker talker, int x, int y, int z, RadarPositionType type)
        {
            await talker.PlayerInstance.SendPacketAsync(new DeleteRadar(type, x, y, z));
        }

        public async Task DeleteAllRadar(Talker talker, RadarPositionType type)
        {
            await talker.PlayerInstance.SendPacketAsync(new RadarControl(RadarControlType.DELETE_ALL, type, 0, 0, 0));
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

        public async Task GiveItem1(Talker talker, string itemName, int count)
        {
            int item_id = _itemInit.GetItemByName(itemName).ItemId;
            await talker.PlayerInstance.PlayerInventory().AddOrUpdate().AddOrUpdateItemToInventory(item_id, count);
        }

        public async Task DeleteItem1(Talker talker, string itemName, int itemCount)
        {
            int item_id = _itemInit.GetItemByName(itemName).ItemId;
            await talker.PlayerInstance.PlayerInventory().AddOrUpdate().DestroyItemInInventoryById(item_id, itemCount);
        }
        
        /// <summary>
        /// TODO by default false
        /// </summary>
        /// <param name="talker"></param>
        /// <returns></returns>
        public bool IsMyLord(Talker talker)
        {
            if (talker.PlayerInstance.IsGM)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="talker"></param>
        /// <returns></returns>
        public bool HavePledgePower(Talker talker, PledgePower pledgePower)
        {
            return true;
        }
        internal async Task ShowSystemMessage(Talker talker, int messageId)
        {
            await talker.PlayerInstance.SendPacketAsync(new SystemMessage((SystemMessageId)messageId));
        }

        internal async Task SetCurrentQuestID(string quest_name)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            await SetCurrentQuestID(quest_id);
        }
        internal async Task SetCurrentQuestID(int quest_id)
        {
            LoggerManager.Warn("SetCurrentQuestID NotImplementedException");
            await Task.FromResult(1);
        }

        internal uint GetInventoryInfo(Talker talker, int param)
        {
            LoggerManager.Warn("GetInventoryInfo NotImplementedException");
            uint result = uint.MaxValue;
            switch (param)
            {
                case 0: //m_nNumInventory
                    result = 0;
                    break;
                case 1: //m_nMaxInventoryNum
                    result = 100;
                    break;
                case 2: //m_nInventoryWeight
                    result = 0;
                    break;
                case 3: //m_dCarryWeight
                    result = 100;
                    break;
                case 4: //m_nCurrentQuestInventoryNum
                    result = 0;
                    break;
                case 5://m_nMaxQuestInventoryNum
                    result = 100;
                    break;
                case 6: //m_nCurrentArtifactInventoryNum
                    result = 0;
                    break;
                case 7: //m_nMaxArtifactInventoryNum
                    result = 100;
                    break;
            }

            return result;
        }

        internal void VoiceEffect(Talker talker, string v1, int v2)
        {
            LoggerManager.Warn("VoiceEffect NotImplementedException");
        }

        internal void IncrementParam(Talker talker, ParameterType parameterType, int value)
        {
            LoggerManager.Warn($"IncrementParam {parameterType} - {value} NotImplementedException");
        }

        internal int GetIndexFromCreature(Talker talker)
        {
            LoggerManager.Warn("GetIndexFromCreature NotImplementedException");
            return 0;
        }

        internal void SetMemoStateEx(Talker talker, int quest_id, int slot, int state)
        {
            LoggerManager.Warn("SetMemoStateEx NotImplementedException");
        }

        internal int CastleGetPledgeId()
        {
            LoggerManager.Warn("Castle_GetPledgeId NotImplementedException");
            return 0;
        }
        /// <summary>
        /// Assign file name to variable fhtml0
        /// </summary>
        /// <param name="fhtml0"></param>
        /// <param name="fileName"></param>
        internal void FHTML_SetFileName(ref string fhtml0, string fileName)
        {
            fhtml0 = Initializer.HtmlCacheInit().GetHtmlText(fileName);
        }
        /// <summary>
        /// Replace in line fhtml0 variable replaceStr with string
        /// </summary>
        /// <param name="fhtml0"></param>
        /// <param name="replaceStr"></param>
        /// <param name="value"></param>
        internal void FHTML_SetStr(ref string fhtml0, string replaceStr, string value)
        {
            fhtml0 = fhtml0.Replace($"<?{replaceStr}?>", value);
        }
        /// <summary>
        /// Replace in line fhtml0 variable replaceStr with number
        /// </summary>
        /// <param name="fhtml0"></param>
        /// <param name="replaceStr"></param>
        /// <param name="value"></param>
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

        internal void AddMoveToDesire(int startX, int startY, int startZ, int v)
        {
            _npcInstance.NpcDesire().AddMoveToDesire(startX, startY, startZ, v);
        }

        internal int GetOlympiadWaitingCount()
        {
            LoggerManager.Warn("GetOlympiadWaitingCount NotImplementedException");
            return 0;
        }

        internal int GetClassFreeOlympiadWaitingCount()
        {
            LoggerManager.Warn("GetClassFreeOlympiadWaitingCount NotImplementedException");
            return 0;
        }

        internal int IsMainClass(Talker talker)
        {
            LoggerManager.Warn("IsMainClass NotImplementedException");
            return 1;
        }

        internal int GetOlympiadPoint(Talker talker)
        {
            LoggerManager.Warn("GetOlympiadPoint NotImplementedException");
            return 0;
        }

        internal void AddClassFreeOlympiad(Talker talker)
        {
            LoggerManager.Warn("AddClassFreeOlympiad NotImplementedException");
        }

        internal void AddOlympiad(Talker talker)
        {
            LoggerManager.Warn("AddOlympiad NotImplementedException");
        }

        internal void RemoveOlympiad(Talker talker)
        {
            LoggerManager.Warn("RemoveOlympiad NotImplementedException");
        }

        internal int GetStatusForOlympiadField(int fieldId)
        {
            LoggerManager.Warn($"GetStatusForOlympiadField {fieldId} NotImplementedException");
            return 0;
        }

        internal string GetPlayer1ForOlympiadField(int fieldId)
        {
            LoggerManager.Warn($"GetPlayer1ForOlympiadField {fieldId} NotImplementedException");
            return "Player1";
        }

        internal string GetPlayer2ForOlympiadField(int fieldId)
        {
            LoggerManager.Warn($"GetPlayer2ForOlympiadField {fieldId} NotImplementedException");
            return "Player2";
        }

        internal int GetPreviousOlympiadPoint(Talker talker)
        {
            LoggerManager.Warn("GetPreviousOlympiadPoint NotImplementedException");
            return 0;
        }

        internal void DeletePreviousOlympiadPoint(Talker talker, int v)
        {
            LoggerManager.Warn("DeletePreviousOlympiadPoint NotImplementedException");
        }

        internal int GetRankByOlympiadRankOrder(int reply, int rank)
        {
            LoggerManager.Warn($"GetRankByOlympiadRankOrder {rank} NotImplementedException");
            return 0;
        }

        internal string GetNameByOlympiadRankOrder(int reply, int rank)
        {
            LoggerManager.Warn($"GetNameByOlympiadRankOrder {rank} NotImplementedException");
            return "NONE";
        }

        internal void ObserveOlympiad(Talker talker, int fieldId)
        {
            LoggerManager.Warn($"ObserveOlympiad field{fieldId}  NotImplementedException");
        }

        public async Task AddLogEx(int v, Talker talker, int ask, int i0)
        {
            LoggerManager.Warn($"AddLogEx NotImplementedException");
            await Task.FromResult(1);
        }

        internal void AddMoveSuperPointDesire(string name, int v1, int v2)
        {
            LoggerManager.Warn($"AddMoveSuperPointDesire name {name}  NotImplementedException");
        }

        internal void ChangeMoveType(int type)
        {
            LoggerManager.Warn($"ChangeMoveType {type}  NotImplementedException");
        }

        internal int CastleGetPledgeState(Talker talker)
        {
            LoggerManager.Warn($"CastleGetPledgeState NotImplementedException");
            return 2;
        }

        internal async Task ShowQuestInfoList(Talker talker)
        {
            await talker.PlayerInstance.SendPacketAsync(new ExShowQuestInfo());
        }

        internal async Task ShowTelPosListPage(Talker talker, IList<TeleportList> position)
        {
            await _npcInstance.NpcRadar().Radar(talker, position);
        }
        /// <summary>
        /// Is there a variable in memo
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="questName"></param>
        /// <returns></returns>
        internal bool HaveMemo(Talker talker, string questName)
        {
            int questId = Initializer.QuestPchInit().GetQuestIdByName(questName);
            return HaveMemo(talker, questId);
        }
        internal bool HaveMemo(Talker talker, int questId)
        {
            return talker.PlayerInstance.PlayerQuest().HaveMemo(questId);
        }
        /// <summary>
        /// Шs the quest fully completed
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="questName"></param>
        /// <returns></returns>
        internal bool GetOneTimeQuestFlag(Talker talker, string quest_name)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            return GetOneTimeQuestFlag(talker, quest_id);
        }
        /// <summary>
        /// Шs the quest fully completed
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="quest_id"></param>
        /// <returns></returns>
        internal bool GetOneTimeQuestFlag(Talker talker, int quest_id)
        {
            return talker.PlayerInstance.PlayerCharacterInfo().GetOneTimeFlag(quest_id);
        }
        /// <summary>
        /// Add variant choice for player
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="questName"></param>
        internal void AddChoice(int choice, string questName/*, int msgIndex*/)
        {
            _talker.AddQuestChoice(choice, questName);
        }
        /// <summary>
        /// Show page choice for player
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        internal async Task ShowChoicePage(Talker talker, int option)
        {
            await _npcChoice.ShowChoice(talker, option);
        }
        /// <summary>
        /// Return Quests count from player
        /// </summary>
        /// <param name="talker"></param>
        /// <returns></returns>
        internal int GetMemoCount(Talker talker)
        {
            return talker.PlayerInstance.PlayerQuest().GetMemoCount();
        }

        internal int GetCurrentTick()
        {
            return Environment.TickCount;
        }

        internal void AddLog(int logType, Talker talker, int quest_id)
        {
            LoggerManager.Warn($"AddLog {quest_id} NotImplementedException");
        }

        public async Task SoundEffect(Talker talker, string sound_file)
        {
            LoggerManager.Warn($"SoundEffect {sound_file} NotImplementedException");
            await talker.PlayerInstance.SendPacketAsync(new PlaySound(sound_file));
        }

        internal void SetOneTimeQuestFlag(Talker talker, string quest_name, bool state)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            SetOneTimeQuestFlag(talker, quest_id, state);
        }

        internal void SetOneTimeQuestFlag(Talker talker, int quest_id, bool state)
        {
            talker.PlayerInstance.PlayerCharacterInfo().SetOneTimeFlag(quest_id, state);
        }

        public async Task SetMemo(Talker talker, string quest_name)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            await SetMemo(talker, quest_id);
        }
        public async Task SetMemo(Talker talker, int quest_id)
        {
            await talker.PlayerInstance.PlayerQuest().SetMemo(quest_id);
        }

        public async Task SetFlagJournal(Talker talker, int quest_id, int flag)
        {
            await talker.PlayerInstance.PlayerQuest().SetFlagJournal(quest_id, flag);
        }
        public int GetMemoState(Talker talker, string quest_name)
        {
            return GetMemoStateEx(talker, quest_name, 0);
        }
        public int GetMemoStateEx(Talker talker, string quest_name, int slot)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            return GetMemoStateEx(talker, quest_id, slot);
        }
        public int GetMemoStateEx(Talker talker, int quest_id, int slot)
        {
            LoggerManager.Warn("GetIndexFromCreature NotImplementedException");
            return talker.PlayerInstance.PlayerQuest().GetMemoStateEx(quest_id, slot);
        }

        public async Task RemoveMemo(Talker talker, string quest_name)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            await RemoveMemo(talker, quest_id);
        }

        public async Task RemoveMemo(Talker talker, int quest_id)
        {
            await talker.PlayerInstance.PlayerQuest().RemoveMemo(quest_id);
        }

        public async Task SetMemoState(Talker talker, string quest_name, int state)
        {
            int quest_id = Initializer.QuestPchInit().GetQuestIdByName(quest_name);
            await SetFlagJournal(talker, quest_id, state);
        }
    }
}