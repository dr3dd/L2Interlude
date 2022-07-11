using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Helpers;
using L2Logger;

namespace Core.TaskManager
{
    internal sealed class AttackStanceTaskManager
    {
        private static volatile AttackStanceTaskManager _instance;
        private readonly ConcurrentDictionary<Character, long> _attackStanceTasks;
        private const long CombatTime = 15000;

        private AttackStanceTaskManager()
        {
            _attackStanceTasks = new ConcurrentDictionary<Character, long>();
            
            TaskManagerScheduler.ScheduleAtFixedRate(Run, 0, 1000);
        }

        private async Task Run()
        {
            long current = DateTimeHelper.CurrentUnixTimeMillis();
            try
            {
                foreach (KeyValuePair<Character, long> entry in _attackStanceTasks)
                {
                    Character character = entry.Key;
                    if ((current - entry.Value) > CombatTime)
                    {
                        //await actor.SendBroadcastPacketAsync(new AutoAttackStop(actor.ObjectId));
                        character.CharacterDesire().SetAutoAttacking(false);
                        _attackStanceTasks.TryRemove(character, out _);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + " " + ex.Message);
            }
        }

        public void AddAttackStanceTask(Character actor)
        {
            _attackStanceTasks.TryAdd(actor, DateTimeHelper.CurrentUnixTimeMillis());
        }
        
        public void RemoveAttackStanceTask(Character actor)
        {
            _attackStanceTasks.TryRemove(actor, out _);
        }
        
        public bool HasAttackStanceTask(Character actor)
        {
            return _attackStanceTasks.ContainsKey(actor);
        }
        
        
        public static AttackStanceTaskManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                if (_instance == null)
                    _instance = new AttackStanceTaskManager();
                return _instance;
            }
        }
    }
}