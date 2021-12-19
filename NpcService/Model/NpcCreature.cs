using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Helpers;
using NpcService.Ai;

namespace NpcService.Model
{
    public class NpcCreature
    {
        private readonly DefaultNpc _defaultNpc;
        private readonly ConcurrentDictionary<int, Task> _tasks;
        public int Sm { get; set; }
        public int NpcObjectId { get; set; }
        public int PlayerObjectId { get; set; }
        private readonly Desire _desire;
        private readonly NpcService _npcService;
        private int _additionalTime; 
        public NpcCreature(DefaultNpc defaultNpc, NpcServerRequest npcServerRequest)
        {
            NpcObjectId = npcServerRequest.NpcObjectId;
            PlayerObjectId = npcServerRequest.PlayerObjectId;
            _desire = new Desire(NpcObjectId, PlayerObjectId, defaultNpc.NpcService);
            _defaultNpc = defaultNpc;
            _npcService = _defaultNpc.NpcService;
            _tasks = new ConcurrentDictionary<int, Task>();
        }
        
        public void ShowPage(Talker talker, string fnHi)
        {
            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.Talked,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = PlayerObjectId,
                FnHi = fnHi
            };
            _npcService.SendMessageAsync(npcServiceResponse);
        }
        
        public void AddEffectActionDesire (int sm, int actionId, int moveAround, int desire)
        {
            _additionalTime = moveAround;
            _desire.AddEffectActionDesire(sm, actionId, moveAround, desire);
        }

        public void AddMoveAroundDesire(int moveAround, int desire)
        {
            _additionalTime = moveAround * 1000;
            _desire.AddMoveAroundDesire(moveAround, desire);
        }
            
            
        
        public int OwnItemCount(Talker talker, int friendShip1)
        {
            return 0;
        }
        
        public void AddTimerEx(int timerId, int delay)
        {
            if (_tasks.ContainsKey(timerId))
            {
                return;
            }
            var currentTimer = ScheduleAtFixed(() =>
            {
                _tasks.TryRemove(timerId, out _);
                _defaultNpc.TimerFiredEx(timerId);
            }, delay + _additionalTime);
            _tasks.TryAdd(timerId, currentTimer);
            _additionalTime = 0;
        }
        
        private Task ScheduleAtFixed(Action action, int delay)
        {
            return Task.Run( async () =>
            {
                await Task.Delay(delay);
                action.Invoke();
            });
        }
    }
}