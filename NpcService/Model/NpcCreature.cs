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
        public NpcCreature Sm { get; set; }
        public int NpcObjectId { get; set; }
        public int PlayerObjectId { get; set; }
        public int Race { get; set; }
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
            Sm = this;
            Race = 1;
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
        
        public void AddEffectActionDesire (NpcCreature sm, int actionId, int moveAround, int desire)
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

        public async Task Teleport(Talker talker, IList<TeleportList> position, string shopName, string empty, string s,
            string empty1, int itemId, string itemName)
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
            await _npcService.SendMessageAsync(npcServiceResponse);
            
        }

        public string MakeFString(int i, string empty, string s, string empty1, string s1, string empty2)
        {
            return "TmpItemName";
        }

        public bool CastleIsUnderSiege()
        {
            return false;
        }

        public void CastleGateOpenClose2(string doorName1, int p1)
        {
            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.CastleGateOpenClose,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = PlayerObjectId,
                DoorName = doorName1,
                OpenClose = p1
            };
            _npcService.SendMessageAsync(npcServiceResponse);
        }

        public void InstantTeleport(Talker talker, int posX01, int posY01, int posZ01)
        {
            throw new NotImplementedException();
        }

        /**
         * TODO dummy
         */
        public bool IsNewbie(Talker talker)
        {
            return true;
        }

        /**
         * TODO dummy
         */
        public bool IsInCategory(int p0, object occupation)
        {
            return true;
        }

        /// <summary>
        /// AddUseSkillDesire
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="pchSkillId"></param>
        /// <param name="skillClassification"></param>
        /// <param name="castingMethod"></param>
        /// <param name="desire"></param>
        public void AddUseSkillDesire(Talker talker, int pchSkillId, int skillClassification, int castingMethod, int desire)
        {
            _desire.AddUseSkillDesire(talker, pchSkillId, skillClassification, castingMethod, desire);
        }
    }
}