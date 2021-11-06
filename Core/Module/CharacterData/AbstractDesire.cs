﻿using System;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.Module.WorldData;

namespace Core.Module.CharacterData
{
    public abstract class AbstractDesire
    {
        private Desire _desire;
        private readonly Character _character;

        protected AbstractDesire(Character character)
        {
            _desire = Desire.IdleDesire;
            _character = character;
        }
        
        public void ChangeDesire(Desire desire)
        {
            _desire = desire;
        }
        
        public Character GetCharacter() => _character;
        
        public Desire GetDesire()
        {
            return _desire;
        }

        public void AddDesire(Desire desire, object arg0)
        {
            switch (desire)
            {
                case Desire.IdleDesire:
                    break;
                case Desire.ActiveDesire:
                    break;
                case Desire.RestDesire:
                    break;
                case Desire.AttackDesire:
                    break;
                case Desire.CastDesire:
                    CastDesireAsync((SkillDataModel) arg0);
                    break;
                case Desire.MoveToDesire:
                    MoveToDesireAsync((Location) arg0);
                    break;
                case Desire.FollowDesire:
                    break;
                case Desire.PickUpDesire:
                    break;
                case Desire.InteractDesire:
                    IntentionInteractAsync((WorldObject) arg0);
                    break;
                case Desire.MoveToInABoatDesire:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desire), desire, null);
            }
        }
        protected abstract Task MoveToDesireAsync(Location arg0);
        protected abstract Task CastDesireAsync(SkillDataModel arg0);
        protected abstract Task IntentionInteractAsync(WorldObject worldObject);
    }
}