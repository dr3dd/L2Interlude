using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Handlers;
using Core.Module.Player;
using Core.Module.WorldData;
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
        private int _additionalTime;
        private readonly CancellationTokenSource _cts;
        public NpcAi Sm { get; set; } //PTS object AI require
        public int Race { get; set; }
        public int Level { get; set; }

        public DefaultNpc GetDefaultNpc() => _defaultNpc;
        
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            Level = _npcInstance.Level;
            _worldInit = npcInstance.ServiceProvider.GetRequiredService<WorldInit>();
            _tasks = new ConcurrentDictionary<int, Task>();
            _cts = new CancellationTokenSource();
            var npcName = _npcInstance.GetStat().Name;
            var npcType = _npcInstance.GetStat().Type;
            _defaultNpc = NpcHandler.GetNpcHandler(npcName, npcType);
            var npcAiData = _npcInstance.GetStat().NpcAiData;
            NpcAiDefault.SetDefaultAiParams(_defaultNpc, npcAiData);
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

        public void Attacked(Character character)
        {
            var attacker = new Talker(character.ObjectId, character.Level);
            _npcInstance.NpcDesire().AddDesire(Desire.AttackDesire, character);
            //_defaultNpc.Attacked(attacker, damage);
        }

        public void Talked(Character character)
        {
            var talker = new Talker(character.ObjectId, character.Level);
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
        
        public async Task AddEffectActionDesire (NpcAi sm, int actionId, int moveAround, int desire)
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
            var url = @"<a action=""bypass -h teleport_goto##objectId#?teleportId=#id#"" msg=""811;#Name#""> #Name# - #Price# Adena </a><br1>";
            string html = null;
            for (var i1 = 0; i1 < position.Count; i1++)
            {
                var teleportName = position[i1].Name;
                var replace = url.Replace("#objectId#", _npcInstance.ObjectId.ToString());
                replace = replace.Replace("#id#", i1.ToString());
                replace = replace.Replace("#Name#", teleportName);
                replace = replace.Replace("#Price#", position[i1].Price.ToString());
                html += replace;
            }

            var Html = "<html><body>&$556;<br><br>" + html + "</body></html>";
            var player = (PlayerInstance) _worldInit.GetWorldObject(talker.ObjectId);
            await _npcInstance.ShowTeleportList(Html, player);
        }

        public async Task TeleportRequested(PlayerInstance playerInstance)
        {
            var talker = new Talker(playerInstance.ObjectId, playerInstance.Level);
            await _defaultNpc.TeleportRequested(talker);
        }

        public bool CastleIsUnderSiege()
        {
            throw new System.NotImplementedException();
        }

        public void CastleGateOpenClose2(string doorName1, int p1)
        {
            throw new System.NotImplementedException();
        }

        public void InstantTeleport(Talker talker, int posX01, int posY01, int posZ01)
        {
            throw new System.NotImplementedException();
        }

        public int OwnItemCount(Talker talker, int friendShip1)
        {
            throw new System.NotImplementedException();
        }

        public string MakeFString(int i, string empty, string s, string empty1, string s1, string empty2)
        {
            throw new System.NotImplementedException();
        }

        public void ShowSkillList(Talker talker, string empty)
        {
            throw new System.NotImplementedException();
        }

        public void ShowGrowSkillMessage(Talker talker, int skillNameId, string empty)
        {
            throw new System.NotImplementedException();
        }

        public bool IsInCategory(int p0, int talkerOccupation)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNewbie(Talker talker)
        {
            throw new System.NotImplementedException();
        }

        public void AddUseSkillDesire(Talker talker, int p1, int p2, int p3, int p4)
        {
            throw new System.NotImplementedException();
        }

        public void AddAttackDesire(Talker attacked, int i, int f0)
        {
            throw new System.NotImplementedException();
        }
    }
}