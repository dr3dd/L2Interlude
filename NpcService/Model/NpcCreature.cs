using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using Microsoft.Extensions.DependencyInjection;
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
        public NpcCreature(DefaultNpc defaultNpc, NpcServerRequest npcServerRequest, IServiceProvider serviceProvider)
        {
            NpcObjectId = npcServerRequest.NpcObjectId;
            PlayerObjectId = npcServerRequest.PlayerObjectId;
            _npcService = serviceProvider.GetRequiredService<NpcService>();
            _desire = new Desire(NpcObjectId, PlayerObjectId, _npcService);
            _defaultNpc = defaultNpc;
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

        public void Teleport(Talker talker, IList<TeleportList> position, string shopName, string empty, string s, string empty1, int i, object makeFString)
        {
            var url = @"<a action=""bypass -h teleport_goto##objectId#?teleportId=#id#"" msg=""811;#Name#""> #Name# - #Price# Adena </a><br1>";
            string html = null;
            for (var i1 = 0; i1 < position.Count; i1++)
            {
                var teleportName = position[i1].Name;
                var replace = url.Replace("#objectId#", NpcObjectId.ToString());
                replace = replace.Replace("#id#", i1.ToString());
                replace = replace.Replace("#Name#", teleportName);
                replace = replace.Replace("#Price#", position[i1].Price.ToString());
                html += replace;
            }

            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.TeleportRequest,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = PlayerObjectId,
                Html = "<html><body>&$556;<br><br>" + html + "</body></html>"
            };
            _npcService.SendMessageAsync(npcServiceResponse);
            
        }

        public object MakeFString(int i, string empty, string s, string empty1, string s1, string empty2)
        {
            return 1;
        }
    }
}