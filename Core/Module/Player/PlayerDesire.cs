using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerDesire : CharacterDesire
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerDesireCast _playerDesireCast;
        private int _moveToPawnTimeout;
        public PlayerDesire(PlayerInstance playerInstance) : base(playerInstance)
        {
            _playerInstance = playerInstance;
            _playerDesireCast = new PlayerDesireCast(_playerInstance);
        }

        protected override async Task CastDesireAsync(SkillDataModel skill)
        {
            ChangeDesire(Desire.CastDesire);
            await _playerDesireCast.DoCastAsync(skill);
        }

        protected override async Task IntentionInteractAsync(WorldObject worldObject)
        {
            if (GetDesire() == Desire.RestDesire)
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }
            ChangeDesire(Desire.InteractDesire);
            await MoveToWorldObjectAsync(worldObject, 60);
        }

        private async Task MoveToWorldObjectAsync(WorldObject worldObject, int offset)
        {
            if (offset < 10)
            {
                offset = 10;
            }
            bool sendPacket = true;
            if (_clientMoving && (_playerInstance.PlayerTargetAction().GetTarget() == worldObject))
            {
                if (_clientMovingToPawnOffset == offset)
                {
                    if (Initializer.TimeController().GetGameTicks() < _moveToPawnTimeout)
                    {
                        return;
                    }
                    sendPacket = false;
                }
                // Set AI movement data
                _clientMoving = true;
                _clientMovingToPawnOffset = offset;
                _moveToPawnTimeout = Initializer.TimeController().GetGameTicks();
                _moveToPawnTimeout += /* 1000 */ 200 / Initializer.TimeController().MillisInTick;
                
                // Calculate movement data for a move to location action and add the actor to movingObjects of GameTimeController
                _playerInstance.CharacterMovement().MoveToLocation(worldObject.GetX(), worldObject.GetY(), worldObject.GetZ(), offset);
                await _playerInstance.SendPacketAsync(new CharMoveToLocation(_playerInstance));
                if (!_playerInstance.CharacterMovement().IsMoving)
                {
                    await _playerInstance.SendActionFailedPacketAsync();
                    return;
                }
                if (sendPacket)
                {
                    await _playerInstance.SendToKnownPlayers(new MoveToPawn(_playerInstance, (Character) worldObject, offset));
                }
            }
        }

        protected internal void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(GetType().Name + ": " + obj.Exception);
            }
        }
        
        public bool IsCastingNow()
        {
            return _playerDesireCast.IsCastingNow();
        }

        public bool IsSkillDisabled(SkillDataModel skill)
        {
            return _playerDesireCast.IsSkillDisabled(skill);
        }
        
        private async Task ProceedAttackAsync()
        {
            Character target = AttackTarget;
            if (await MaybeMoveToPawnAsync(target, _playerInstance.PlayerCombat().GetBaseAttackRange()))
            {
                return;
            }
            LoggerManager.Info($"Start Attack Target: {target.ObjectId}");
            await _playerInstance.PhysicalAttack().DoAttackAsync(target).ContinueWith(HandleException);
        }
    }
}