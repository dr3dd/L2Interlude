using System;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket.CharacterPacket;

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
        protected abstract Task IntentionAttackAsync(Character target);
        
        /// <summary>
        /// TODO Refactor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public async Task MoveToAsync(int x, int y, int z)
        {
            // Calculate movement data for a move to location action and add the actor to movingObjects of GameTimeController
            _character.CharacterMovement().MoveToLocation(x, y, z, 0);
            // Send a Server->Client packet CharMoveToLocation to the actor and all L2Player in its _knownPlayers
            
            foreach (WorldObject worldObject in _character.CharacterKnownList().GetKnownObjects().Values)
            {
                if (worldObject is PlayerInstance playerInstance)
                {
                    await playerInstance.SendPacketAsync(new CharMoveToLocation(_character));
                }
            }
        }
    }
}