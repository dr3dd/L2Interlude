using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Helpers;
using L2Logger;

namespace NpcService.Ai
{
    public abstract class DefaultNpc
    {
        protected int ResidenceId = 0;
        protected int DesirePqSize = 50;
        protected int FavorListSize = 30;
        public virtual int MoveAroundSocial { get; set; } = 0;
        public virtual int MoveAroundSocial1 { get; set; } = 0;
        public virtual int MoveAroundSocial2 { get; set; } = 0;
        
        protected double IdleDesireDecayRatio = 0.000000;
        protected double MoveAroundDecayRatio = 0.000000;
        protected double DoNothingDecayRatio = 0.000000;
        protected double AttackDecayRatio = 0.000000;
        protected double ChaseDecayRatio = 0.000000;
        protected double FleeDecayRatio = 0.000000;
        protected double GetItemDecayRatio = 0.000000;
        protected double FollowDecayRatio = 0.000000;
        protected double DecayingDecayRatio = 0.000000;
        protected double MoveToWayPointDecayRatio = 0.000000;
        protected double UseSkillDecayRatio = 0.000000;
        protected double MoveToDecayRatio = 0.000000;
        protected double EffectActionDecayRatio = 0.000000;
        protected double MoveToTargetDecayRatio = 0.000000;
        protected double IdleDesireBoostValue = 0.000000;
        protected double MoveAroundBoostValue = 0.000000;
        protected double DoNothingBoostValue = 0.000000;
        protected double AttackBoostValue = 0.000000;
        protected double ChaseBoostValue = 0.000000;
        protected double FleeBoostValue = 0.000000;
        protected double GetItemBoostValue = 0.000000;
        protected double FollowBoostValue = 0.000000;
        protected double DecayingBoostValue = 0.000000;
        protected double MoveToWayPointBoostValue = 0.000000;
        protected double UseSkillBoostValue = 0.000000;
        protected double MoveToBoostValue = 0.000000;
        protected double EffectActionBoostValue = 0.000000;
        protected double MoveToTargetBoostValue = 0.000000;
        public int NpcObjectId { get; set; }
        public int PlayerObjectId { get; set; }
        
        protected IServiceProvider ServiceProvider { get; }
        protected NpcService NpcService { get; }
        public NpcServerRequest NpcServerRequest { get; set; }
        public DefaultNpc MySelf { get; set; }
        public DefaultNpc Sm { get; set; }
        public Talker Talker { get; set; }

        public abstract void Created();
        public abstract void Talked(Talker talker);
        
        public abstract void TimerFiredEx(int timerId);
        private readonly ConcurrentDictionary<int, Task> _tasks;

        protected DefaultNpc(IServiceProvider serviceProvider, NpcService npcService)
        {
            _tasks = new ConcurrentDictionary<int, Task>();
            ServiceProvider = serviceProvider;
            NpcService = npcService;
        }
        
        public void AddEffectActionDesire (DefaultNpc npc, int actionId, int moveAround, int desire)
        {
            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.EffectActionDesire,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = PlayerObjectId,
                SocialId = actionId
            };
            NpcService.SendMessageAsync(npcServiceResponse);
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
                TimerFiredEx(timerId);
            }, delay);
            _tasks.TryAdd(timerId, currentTimer);
        }
        
        private Task ScheduleAtFixed(Action action, int delay)
        {
            return Task.Run( async () =>
            {
                await Task.Delay(delay);
                action.Invoke();
            });
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
            NpcService.SendMessageAsync(npcServiceResponse);
        }

        public int OwnItemCount(Talker talker, int friendShip1)
        {
            return 0;
        }
    }
}