using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.DoorData;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Handlers;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData
{
    public class NpcAi
    {
        private readonly NpcInstance _npcInstance;
        private readonly WorldInit _worldInit;
        private readonly DefaultNpc _defaultNpc;
        private readonly ConcurrentDictionary<int, Task> _tasks;
        private readonly NpcAiTeleport _aiTeleport;
        private int _additionalTime;
        private readonly CancellationTokenSource _cts;
        public NpcAiSm Sm { get; } //PTS object AI require

        public DefaultNpc GetDefaultNpc() => _defaultNpc;
        
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _aiTeleport = new NpcAiTeleport(this);
            _worldInit = npcInstance.ServiceProvider.GetRequiredService<WorldInit>();
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
                Race = GetNpcRaceId(_npcInstance.GetStat().Race)
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

        public void Talked(PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            _defaultNpc.Talked(talker);
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

        public void InstantTeleport(Talker talker, int posX01, int posY01, int posZ01)
        {
            throw new NotImplementedException();
        }

        public int OwnItemCount(Talker talker, int friendShip1)
        {
            throw new NotImplementedException();
        }

        public string MakeFString(int i, string empty, string s, string empty1, string s1, string empty2)
        {
            return "TmpItemName";
        }

        public async Task ShowSkillList(Talker talker, string empty)
        {
            var player = (PlayerInstance) _worldInit.GetWorldObject(talker.ObjectId);
            await _npcInstance.ShowSkillList(player);
        }

        public async Task ShowGrowSkillMessage(Talker talker, int skillNameId, string empty)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="talkerOccupation"></param>
        /// <returns></returns>
        public bool IsInCategory(int p0, int talkerOccupation)
        {
            return (p0 == talkerOccupation);
        }

        public bool IsNewbie(Talker talker)
        {
            throw new NotImplementedException();
        }

        public void AddUseSkillDesire(Talker talker, int p1, int p2, int p3, int p4)
        {
            throw new NotImplementedException();
        }

        public void AddAttackDesire(Talker attacked, int i, int f0)
        {
            throw new NotImplementedException();
        }

        public async Task MenuSelect(int askId, int replyId, PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance);
            if (_defaultNpc is NewbieGuide newbieGuide)
            {
                await newbieGuide.MenuSelected(talker, askId, replyId);
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
            
        }

        public async Task ShowEnchantSkillList(Talker talker)
        {
            throw new NotImplementedException();
        }

        public async Task ShowEnchantSkillMessage(Talker talker, int skillNameId)
        {
            throw new NotImplementedException();
        }
    }
}