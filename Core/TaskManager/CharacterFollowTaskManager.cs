using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.WorldData;
using Helpers;
using L2Logger;

namespace Core.TaskManager
{
    internal sealed class CharacterFollowTaskManager
    {
        private static volatile CharacterFollowTaskManager _instance;
        private readonly ConcurrentDictionary<Character, int> _normalFollowCharacters;
        private readonly ConcurrentDictionary<Character, int> _attackFollowCharacters;
        private bool _workingNormal;
        private bool _workingAttack;

        public static CharacterFollowTaskManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                if (_instance == null)
                    _instance = new CharacterFollowTaskManager();
                return _instance;
            }
        }

        private CharacterFollowTaskManager()
        {
            _normalFollowCharacters = new ConcurrentDictionary<Character, int>();
            _attackFollowCharacters = new ConcurrentDictionary<Character, int>();

            NormalFollowCharacter();
            AttackFollowCharacter();
        }

        private void AttackFollowCharacter()
        {
            TaskManagerScheduler.ScheduleAtFixedRate(async () =>
            {
                if (_workingAttack)
                {
                    return;
                }
                _workingAttack = true;
                foreach (var (key, value) in _attackFollowCharacters)
                {
                    await Follow(key, value);
                }
                _workingAttack = false;
            }, 500, 500);
        }

        private void NormalFollowCharacter()
        {
            TaskManagerScheduler.ScheduleAtFixedRate(async () =>
            {
                if (_workingNormal)
                {
                    return;
                }
                _workingNormal = true;
                foreach (var (key, value) in _normalFollowCharacters)
                {
                    await Follow(key, value);
                }
                _workingNormal = false;
                
            }, 1000, 1000);
        }
        
        private async Task Follow(Character character, int range)
        {
            try
            {
                CharacterDesire desire = character.CharacterDesire();
                WorldObject followTarget = desire.FollowTarget;
                if (followTarget == null)
                {
                    desire.AddDesire(Desire.IdleDesire, character);
                    return;
                }
			
                int followRange = range == -1 ? Rnd.Next(50, 100) : range;
                if (!character.CharacterZone().IsInsideRadius(followTarget, followRange, true, false))
                {
                    if (!character.CharacterZone().IsInsideRadius(followTarget, 3000, true, false))
                    {
                        // If the target is too far (maybe also teleported).
                        desire.AddDesire(Desire.IdleDesire, character);
                        return;
                    }
                    await desire.MoveToPawnAsync(followTarget, followRange);
                }
            }
            catch (Exception e)
            {
                // Ignore.
                LoggerManager.Info("CharacterFollowTaskManager:" + e.Message);
            }
        }
        
        public bool IsFollowing(Character character)
        {
            return _normalFollowCharacters.ContainsKey(character) || _attackFollowCharacters.ContainsKey(character);
        }
        
        public async Task AddNormalFollow(Character character, int range)
        {
            await Follow(character, range);
            _normalFollowCharacters.TryAdd(character, range);
        }
	
        public async Task AddAttackFollow(Character character, int range)
        {
            await Follow(character, range);
            _attackFollowCharacters.TryAdd(character, range);
        }

        public void Remove(Character character)
        {
            _normalFollowCharacters.Remove(character, out _);
            _attackFollowCharacters.Remove(character, out _);
        }
    }
}