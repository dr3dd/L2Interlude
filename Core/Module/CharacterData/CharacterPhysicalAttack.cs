using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData.PhysicalAttack;
using Core.Module.ItemData;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;

namespace Core.Module.CharacterData;

public class CharacterPhysicalAttack : CharacterAttackAbstract
{
    private readonly Character _character;
    private int _disableBowAttackEndTime;
    private ICharacterPhysicalAttackValidator Validator { get; }
    private readonly ReaderWriterLockSlim _attackLock = new();

    public CharacterPhysicalAttack(Character character) : base(character)
    {
        _character = character;
        Validator = new GeneralCharacterPhysicalAttackValidator(character);
    }

    public async Task DoAttackAsync(Character target)
    {
        if (!_attackLock.TryEnterWriteLock(0))
        {
            return;
        }
        try
        {
            if (!Validator.IsValid(target))
            {
                await _character.SendActionFailedPacketAsync();
                return;
            }
            await RechargeAutoSoulShotAsync();
            //AddKnownObject(target);
            await StopMoving();
            SetAttacking();
            SetHeading(target);
            var weapon = GetActiveWeapon();

            // Get the Attack Speed of the Creature (delay (in milliseconds) before next attack)
            // the hit is calculated to happen halfway to the animation - might need further tuning e.g. in bow case
            var timeAtk = CalculateTimeBetweenAttacks(target, weapon.WeaponType);
            var timeToHit = timeAtk / 2;
            var reuse = CalculateReuseTime(target, weapon);
            var isSoulShotCharged = IsSoulShotCharged(weapon);
            var ssGrade = GetCrystalType(weapon);

            var attack = new Attack(_character, target, isSoulShotCharged, (int) ssGrade);
            var hitted = ExecuteAttack(weapon, attack, target, timeAtk, timeToHit, reuse);
        
            // Check if hit isn't missed
            if (!hitted)
            {
                await AbortAttackAsync(); // Abort the attack of the Creature and send Server->Client ActionFailed packet
            }
            if (attack.HasHits())
            {
                await SendAttackPacketsAsync(attack);
            }
            ScheduleNextAttack(timeAtk + reuse);
        } 
        finally
        {
            _attackLock.ExitWriteLock();
        }
    }

    private void ScheduleNextAttack(int delay)
    {
        TaskManagerScheduler.Schedule(() => { _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtReadyToAct); },
            delay);
    }

    private async Task SendAttackPacketsAsync(Attack attack)
    {
        await _character.SendPacketAsync(attack);
        await _character.SendToKnownPlayers(attack);
        //DischargeSoulShot(weapon, isSoulShotCharged);
    }

    private bool ExecuteAttack(Weapon weapon, Attack attack, Character target, int timeAtk, int timeToHit, int reuse)
    {
        if (IsBowAttack(weapon))
        {
            if (!CanUseRangeWeapon())
            {
                return false;
            }
            SetAttackEndTime(timeToHit + (reuse / 2));
            return DoAttackHitByBow(attack, target, timeAtk, reuse);
        }
        SetAttackEndTime(timeAtk);
        return DoAttackHitSimple(attack, target, timeToHit);
    }

    private bool DoAttackHitSimple(Attack attack, Character target, int timeToHit)
    {
        var damage = 0;
        var isShield = false;
        var isCritical = false;
        
        // Calculate if hit is missed or not
        var missAttack = CalculateSkill.CalcHitMiss(_character, target);
        if (!missAttack)
        {
            // Calculate if shield defense is efficient
            isShield = CalculateSkill.CalcShieldUse(_character, target);
            // Calculate if hit is critical
            isCritical = CalculateSkill.CalcCrit(_character.CharacterCombat().GetCriticalRate());

            // Calculate physical damages
            damage = (int) CalculateSkill.CalcPhysDam(_character, target, isShield, isCritical, false, attack.Soulshot);
        }
        ProcessAttackHit(attack, target, damage, isShield, isCritical, missAttack, timeToHit);
        // Return true if hit isn't missed
        return !missAttack;
    }

    private void ProcessAttackHit(Attack attack, Character target, int damage, bool isShield, bool isCritical, bool missAttack, int timeToHit)
    {
        var attackHit = new AttackHit(_character, target, damage, isShield, isCritical, missAttack, attack.Soulshot);
        var hitProcess = new ProcessHit(attackHit);
        TaskManagerScheduler.Schedule(hitProcess.Run, timeToHit);
        attack.AddHit(target.ObjectId, damage, missAttack, isCritical, isShield);
    }

    private bool DoAttackHitByBow(Attack attack, Character target, int timeAtk, int reuse)
    {
        var damage = 0;
        var isShield = false;
        var isCritical = false;
        
        // Calculate if hit is missed or not
        var missAttack = CalculateSkill.CalcHitMiss(_character, target);
        if (!missAttack)
        {
            // Calculate if shield defense is efficient
            isShield = CalculateSkill.CalcShieldUse(_character, target);
            // Calculate if hit is critical
            isCritical = CalculateSkill.CalcCrit(_character.CharacterCombat().GetCriticalRate());

            // Calculate physical damages
            damage = (int) CalculateSkill.CalcPhysDam(_character, target, isShield, isCritical, false, attack.Soulshot);
            damage = (int) (damage * ((target.CalculateDistance3D(target.GetX(), target.GetY(), target.GetZ()) / 4000) + 0.8));
        }
        // Check if the Creature is a Player
        if (_character.IsPlayer())
        {
            // Consume arrows
            //ReduceArrowCount();
            _character.SendPacketAsync(new SetupGauge(SetupGauge.Red, timeAtk + reuse));
        }
        ProcessAttackHit(attack, target, damage, isShield, isCritical, missAttack, timeAtk + reuse);
        ManageBowAttackTiming(timeAtk, reuse);
        // Return true if hit isn't missed
        return !missAttack;
    }
    
    private void ManageBowAttackTiming(int timeAtk, int reuse)
    {
        var gameTime = Initializer.TimeController().GetGameTicks();
        _disableBowAttackEndTime = gameTime + ((timeAtk + reuse) / Initializer.TimeController().MillisInTick);
        if (_disableBowAttackEndTime < gameTime)
        {
            _disableBowAttackEndTime = int.MaxValue;
        }
    }

    private bool CanUseRangeWeapon()
    {
        if (_character.IsPlayer())
        {
            if (_disableBowAttackEndTime <= Initializer.TimeController().GetGameTicks())
            {
                // Set the period of bow no re-use
                _disableBowAttackEndTime = (5 * Initializer.TimeController().TicksPerSecond) + Initializer.TimeController().GetGameTicks();
            }
            else
            {
                TaskManagerScheduler.Schedule(() => { _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtReadyToAct); },
                    1000);
                _character.SendActionFailedPacketAsync();
                return false;
            }
        }

        return true;
    }
        
    private async Task StopMoving()
    {
        if (_character.CharacterMovement().IsMoving)
        {
            await _character.CharacterMovement().StopMoveAsync(_character.WorldObjectPosition().WorldPosition());
        }
    }
        
    private void SetAttackEndTime(int timeAtk)
    {
        var currentTime = DateTime.Now.Ticks * 100;
        AttackEndTime = currentTime + TimeSpan.FromMilliseconds(timeAtk).Ticks * 100;
    }
        
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    private bool IsSoulShotCharged(Weapon weapon)
    {
        return true;
    }
        
    private Weapon GetActiveWeapon() => _character.GetActiveWeaponItem();
        
    private CrystalType GetCrystalType(Weapon weapon)
    {
        return weapon.CrystalType;
    }

    private async Task<int> RechargeAutoSoulShotAsync()
    {
        return await Task.FromResult(1);
    }
        
    /// <summary>
    /// Is Bow Attack
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    private bool IsBowAttack(Weapon weapon)
    {
        return weapon.WeaponType == WeaponType.Bow;
    }
        
    /// <summary>
    /// Is Dual Weapon Attack
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    private bool IsDualWeaponAttack(Weapon weapon)
    {
        return weapon.WeaponType switch
        {
            WeaponType.Dual => true,
            WeaponType.DualFist => true,
            _ => false
        };
    }
        
    /// <summary>
    /// Heading calculation on every attack
    /// </summary>
    /// <param name="target"></param>
    private void SetHeading(Character target) =>
        _character.Heading =
            CalculateRange.CalculateHeadingFrom(
                _character.GetX(), _character.GetY(), target.GetX(), target.GetY()
            );
}