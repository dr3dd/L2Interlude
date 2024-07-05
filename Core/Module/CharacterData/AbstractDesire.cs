using System;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Core.TaskManager;

namespace Core.Module.CharacterData
{
    public abstract class AbstractDesire
    {
        protected bool _clientMoving;
        protected int _clientMovingToPawnOffset;
        private bool _clientAutoAttacking;
        private Desire _desire;
        protected readonly Character _character;
        private Character _attackTarget;
        /** Different internal state flags */
        private int _moveToPawnTimeout;
        public Character FollowTarget { get; set; }
        
        protected internal Character AttackTarget
        {
            set
            {
                lock (this)
                {
                    _attackTarget = value;
                }
            }
            get => _attackTarget;
        }

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
                    DesireAttackAsync((Character) arg0);
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
        protected abstract Task DesireAttackAsync(Character target);
        protected abstract Task DesireActiveAsync();
        
        /// <summary>
        /// TODO Refactor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public async Task MoveToAsync(int x, int y, int z)
        {
            // Set AI movement data
            _clientMoving = true;
            _clientMovingToPawnOffset = 0;
            await _character.CharacterMovement().MoveToLocation(x, y, z, 0);
            await _character.SendPacketAsync(new CharMoveToLocation(_character));
            await _character.SendToKnownPlayers(new CharMoveToLocation(_character));
        }

        protected internal async Task<bool> MaybeMoveToPawnAsync(WorldObject target, int offset)
        {
            // skill radius -1
            if (offset < 0)
            {
                return await Task.FromResult(false);
            }
            
            var offsetWithCollision = (int) (offset + _character.CharacterCombat().GetCollisionRadius());
            if (target is Character character)
            {
                offsetWithCollision += (int) _character.CharacterCombat().GetCollisionRadius();
            }

            if (!_character.CharacterZone().IsInsideRadius(target, offsetWithCollision, false, false))
            {
                Character follow = FollowTarget;
                await MoveToPawnAsync(target, offset);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task MoveToPawnAsync(WorldObject pawn, int offset)
        {
            var sendPacket = true;
            if (offset < 10)
            {
                offset = 10;
            }
            if (_clientMoving)
            {
                if (_clientMovingToPawnOffset == offset)
                {
                    if (Initializer.TimeController().GetGameTicks() < _moveToPawnTimeout)
                    {
                        return;
                    }
                    sendPacket = false;
                }
            }
            // Set AI movement data
            _clientMoving = true;
            _clientMovingToPawnOffset = offset;
            _moveToPawnTimeout = Initializer.TimeController().GetGameTicks();
            _moveToPawnTimeout += /* 1000 */ 200 / Initializer.TimeController().MillisInTick;
            // Calculate movement data for a move to location action and add the actor to movingObjects of GameTimeController
            await _character.CharacterMovement().MoveToLocation(pawn.GetX(), pawn.GetY(), pawn.GetZ(), offset);
            if (!_character.CharacterMovement().IsMoving)
            {
                return;
            }
            _clientMovingToPawnOffset = 0;
            var packet = new CharMoveToLocation(_character);
            await _character.SendToKnownPlayers(packet);
            await _character.SendPacketAsync(packet);
        }
        
        public async Task ClientStopMovingAsync(Location pos)
        {
            // Stop movement of the Creature
            if (_character.CharacterMovement().IsMoving)
            {
                await _character.CharacterMovement().StopMoveAsync(pos);
            }
            
            _clientMovingToPawnOffset = 0;

            if (!_clientMoving && (pos == null)) return;
            _clientMoving = false;
            // Send a Server->Client packet StopMove to the actor and all PlayerInstance in its _knownPlayers
            await _character.SendToKnownPlayers(new StopMove(_character));

            if (pos == null) return;
            // Send a Server->Client packet StopRotation to the actor and all PlayerInstance in its _knownPlayers
            StopRotation stopRotation = new StopRotation(_character, pos.GetHeading(), 0);
            await _character.SendPacketAsync(stopRotation);
            await _character.SendToKnownPlayers(stopRotation);
        }

        public async Task ClientStoppedMovingAsync()
        {
            if (_clientMovingToPawnOffset > 0) // movetoPawn needs to be stopped
            {
                _clientMovingToPawnOffset = 0;
                await _character.SendToKnownPlayers(new StopMove(_character));
            }
            _clientMoving = false;
        }
        
        public async Task ClientStartAutoAttackAsync()
        {
            if (!_clientAutoAttacking)
            {
                // Send a Server->Client packet AutoAttackStart to the actor and all PlayerInstance in its _knownPlayers
                await _character.SendToKnownPlayers(new AutoAttackStart(_character.ObjectId));
                SetAutoAttacking(true);
            }
        }
        
        public async Task ClientStopAutoAttackAsync()
        {
            if (_clientAutoAttacking)
            {
                await _character.SendToKnownPlayers(new AutoAttackStop(_character.ObjectId));
                SetAutoAttacking(false);
            }
            AttackStanceTaskManager.Instance.AddAttackStanceTask(_character);
        }
        
        public void SetAutoAttacking(bool isAutoAttacking)
        {
            _clientAutoAttacking = isAutoAttacking;
        }
        
        protected async Task ClientActionFailedAsync()
        {
            await _character.SendActionFailedPacketAsync();
        }
        
        public void StopFollow()
        {
            CharacterFollowTaskManager.Instance.Remove(_character);
            FollowTarget = null;
        }
        
        public bool IsFollowing()
        {
            return (FollowTarget != null) && ((_desire == Desire.FollowDesire) || CharacterFollowTaskManager.Instance.IsFollowing(_character));
        }
        
        public async Task StartFollowAsync(Character target, int range)
        {
            StopFollow();
            FollowTarget = target;
            if (range == -1)
            {
                await CharacterFollowTaskManager.Instance.AddNormalFollow(_character, range);
            }
            else
            {
                await CharacterFollowTaskManager.Instance.AddAttackFollow(_character, range);
            }
        }
        
        public async Task StartFollowAsync(Character target)
        {
            await StartFollowAsync(target, -1);
        }
    }
}