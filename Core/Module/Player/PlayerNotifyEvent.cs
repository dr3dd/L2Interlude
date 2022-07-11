using System.Threading.Tasks;
using Core.Module.CharacterData;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerNotifyEvent : CharacterNotifyEvent
    {
        private readonly PlayerInstance _playerInstance;
        public PlayerNotifyEvent(PlayerInstance playerInstance) : base(playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        public override async Task ThinkAttackAsync()
        {
            Character target =_playerInstance.CharacterDesire().AttackTarget;
            if (target == null)
            {
                return;
            }
            if (await _playerInstance.CharacterDesire().MaybeMoveToPawnAsync(target, _playerInstance.PlayerCombat().GetBaseAttackRange()))
            {
                return;
            }
            LoggerManager.Info($"Start Attack Target: {target.ObjectId}");
            await _playerInstance.PhysicalAttack().DoAttackAsync(target).ContinueWith(_playerInstance.CharacterDesire().HandleException);
        }
    }
}