using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using L2Logger;
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
        public int Level { get; set; }
        public bool CanBeAttacked { get; set; }
        private readonly Desire _desire;
        private readonly NpcService _npcService;
        private int _additionalTime; 
        private CancellationTokenSource _cts;
        private ActionDesire _currentDesire;
        public bool IsActiveNpc { get; set; }
        private Task _aiTask;
        public NpcCreature(DefaultNpc defaultNpc, NpcServerRequest npcServerRequest, IServiceProvider serviceProvider)
        {
            NpcObjectId = npcServerRequest.NpcObjectId;
            PlayerObjectId = npcServerRequest.PlayerObjectId;
            _npcService = serviceProvider.GetRequiredService<NpcService>();
            _desire = new Desire(this, PlayerObjectId, _npcService);
            _defaultNpc = defaultNpc;
            _tasks = new ConcurrentDictionary<int, Task>();
            Sm = this;
            CanBeAttacked = npcServerRequest.CanBeAttacked;
            Race = 1;
            Level = npcServerRequest.Level;
            StartAiTask();
        }
        
        public void SetDesire(ActionDesire desire)
        {
            _currentDesire = desire;
        }

        public ActionDesire GetCurrentDesire()
        {
            return _currentDesire;
        }
        
        public void ShowPage(Talker talker, string fnHi)
        {
            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.Talked,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = talker.ObjectId,
                FnHi = fnHi
            };
            _npcService.SendMessageAsync(npcServiceResponse);
        }
        
        public void AddEffectActionDesire (NpcCreature sm, int actionId, int moveAround, int desire)
        {
            if (!IsActiveNpc)
            {
                return;
            }
            _additionalTime = moveAround;
            _desire.AddEffectActionDesire(sm, actionId, moveAround, desire);
        }

        public void AddMoveAroundDesire(int moveAround, int desire)
        {
            if (!IsActiveNpc)
            {
                return;
            }
            _additionalTime = moveAround * 1000;
            ScheduleAtFixedRate(() =>
            {
                _desire.AddMoveAroundDesire(moveAround, desire);
            }, _additionalTime, _additionalTime, _cts.Token);
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
        
        private void StartAiTask()
        {
            if (!CanBeAttacked)
            {
                //return;
            }
            _cts = new CancellationTokenSource();
            _aiTask = ScheduleAtFixedRate(Run, 1000, 1000, _cts.Token);
        }
        
        private void StopAiTask()
        {
            if (!_aiTask.IsCanceled)
            {
                _cts.Cancel();
            }
        }

        
        private void Run()
        {
            if (!IsActiveNpc)
            {
                return;
            }

            if (_currentDesire == ActionDesire.AddEffectActionDesire)
            {
                var d = 1;
            }
            LoggerManager.Info("CurrentDesire: " + _currentDesire);
        }
        
        private Task ScheduleAtFixed(Action action, int delay)
        {
            return Task.Run( async () =>
            {
                await Task.Delay(delay);
                action.Invoke();
            });
        }
        
        public Task ScheduleAtFixedRate(Action action, int delay, int period, CancellationToken token)
        {
            try
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        using var timer = new TaskTimer(period).CancelWith(token).Start(delay);
                        foreach (var task in timer)
                        {
                            await task;
                            action.Invoke();
                        }
                    }
                    catch (TaskCanceledException)
                    {

                    }
                }, token);
            }
            catch (Exception ex)
            {
                LoggerManager.Error("ScheduleAtFixedRate: " + ex.Message);
                throw;
            }
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
                PlayerObjectId = talker.ObjectId,
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

        /// <summary>
        /// TODO possible bug with PlayerObjectId
        /// </summary>
        /// <param name="doorName1"></param>
        /// <param name="p1"></param>
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

        public void ShowSkillList(Talker talker, string empty)
        {
            var npcServiceResponse = new NpcServerResponse
            {
                EventName = EventName.ShowSkillList,
                NpcObjectId = NpcObjectId,
                PlayerObjectId = talker.ObjectId
            };
            _npcService.SendMessageAsync(npcServiceResponse);
        }

        public void ShowGrowSkillMessage(Talker talker, int skillNameId, string empty)
        {
            throw new NotImplementedException();
        }

        public void AddAttackDesire(Talker attacker, int actionId, int desire)
        {
            _desire.AddAttackDesire(attacker, 1, desire);
        }
    }
}