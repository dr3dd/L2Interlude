using System;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData;

public abstract class CharacterAttackAbstract
{
    private readonly Character _character;
    protected long AttackEndTime;
    private bool _attacking;

    protected CharacterAttackAbstract(Character character)
    {
        _character = character;
    }

    /// <summary>
    /// the Attack Speed of the Creature (delay (in milliseconds) before next attack).
    /// </summary>
    /// <param name="target"></param>
    /// <param name="weaponType"></param>
    /// <returns></returns>
    protected int CalculateTimeBetweenAttacks(Character target, WeaponType weaponType)
    {
        return 500000 / _character.CharacterCombat().GetPhysicalAttackSpeed();
    }

    /// <summary>
    /// Get the Attack Reuse Delay of the Weapon
    /// </summary>
    /// <param name="target"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    protected int CalculateReuseTime(Character target, Weapon weapon)
    {
        if ((weapon == null) || (weapon.ReuseDelay == 0))
        {
            return 0;
        }
        return weapon.ReuseDelay * 333 / _character.CharacterCombat().GetPhysicalAttackSpeed();
    }

    protected void SetAttacking() => _attacking = true;
    public bool IsAttackingNow() => AttackEndTime > DateTime.Now.Ticks * 100;

    public bool IsAttackAborted()
    {
        return !_attacking;
    } 

    public async Task AbortAttackAsync()
    {
        if (IsAttackingNow())
        {
            _attacking = false;
            await _character.SendPacketAsync(new ActionFailed());
        }
    }
}