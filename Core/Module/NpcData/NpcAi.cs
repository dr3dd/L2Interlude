using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using NpcAi.Ai;
using NpcAi.Handlers;

namespace Core.Module.NpcData
{
    public class NpcAi
    {
        private readonly NpcInstance _npcInstance;
        private readonly DefaultNpc _defaultNpc;
        private readonly ConcurrentDictionary<int, Task> _tasks;
        private int _additionalTime;
        private readonly CancellationTokenSource _cts;
        public NpcAi Sm { get; set; } //PTS object AI require
        public DefaultNpc GetDefaultNpc() => _defaultNpc;
        
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
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
        }

        public void NoDesire()
        {
            _defaultNpc.NoDesire();
        }

        public void Attacker()
        {
            throw new System.NotImplementedException();
        }

        public void Talked()
        {
            throw new System.NotImplementedException();
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
            await _npcInstance.SendToKnownPlayers(new SocialAction(_npcInstance.ObjectId, actionId));
        }
    }
}