using System.Threading.Tasks;
using Core.Module.CharacterData;
using L2Logger;

namespace Core.Module.Player;

public class PlayerNotifyEvent : CharacterNotifyEvent
{
    private readonly PlayerInstance _playerInstance;
    public PlayerNotifyEvent(PlayerInstance playerInstance) : base(playerInstance)
    {
        _playerInstance = playerInstance;
    }

    private Character GetAttackTarget() => _playerInstance.CharacterDesire().AttackTarget;

    public override async Task ThinkAttackAsync()
    {
        var target = GetAttackTarget();
        if (target == null)
        {
            return;
        }

        if (CheckTargetLostOrDead(target))
        {
            // Notify the target
            _playerInstance.CharacterDesire().AttackTarget = null;
            return;
        }

        if (await _playerInstance.CharacterDesire().MaybeMoveToPawnAsync(target, _playerInstance.PlayerCombat().GetPhysicalAttackRange()))
        {
            return;
        }
        await _character.CharacterDesire().ClientStopMovingAsync(null);
        //LoggerManager.Info($"Start Attack Target: {target.ObjectId}");
        await _playerInstance.PhysicalAttack().DoAttackAsync(target).ContinueWith(_playerInstance.CharacterDesire().HandleException);
    }
}