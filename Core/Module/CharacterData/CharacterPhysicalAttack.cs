using System.Threading.Tasks;
using Core.Module.CharacterData.PhysicalAttack;
using Core.Module.ItemData;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;

namespace Core.Module.CharacterData
{
    public class CharacterPhysicalAttack : CharacterAttackAbstract
    {
        private readonly Character _character;
        private ICharacterPhysicalAttackValidator Validator { get; }
        
        public CharacterPhysicalAttack(Character character) : base(character)
        {
            _character = character;
            Validator = new GeneralCharacterPhysicalAttackValidator(character);
        }
        
        public async Task DoAttackAsync(Character target)
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
            SetAttackEndTime(timeAtk);
            
            var isSoulShotCharged = IsSoulShotCharged(weapon);
            var ssGrade = GetCrystalType(weapon);
            var attackHit = GetAttackHit(weapon, target, isSoulShotCharged);
            await SendHitAnimation(attackHit, ssGrade);

            if (IsBowAttack(weapon))
            {
                //await DoBowAttack(weapon, hitTask, timeAtk, reuse);
                return;
            }
            if (IsDualWeaponAttack(weapon))
            {
                //DoDualWeaponAttack(target, weapon, timeToHit, timeAtk, reuse);
                return;
            }
            var hitProcess = new ProcessHit(attackHit);
            DoRegularAttack(hitProcess, timeToHit, timeAtk, reuse);
        }
        
        private void DoRegularAttack(IRunnable hitTask, int timeToHit, int timeAtk, int reuse)
        {
            TaskManagerScheduler.Schedule(hitTask.Run, timeToHit);
            TaskManagerScheduler.Schedule(() => { _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtReadyToAct); },
                timeAtk + reuse);
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
            _attackEndTime = Initializer.TimeController().GetGameTicks();
            _attackEndTime += (timeAtk / Initializer.TimeController().MillisInTick);
            _attackEndTime -= 1;
        }
        
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private bool IsSoulShotCharged(Weapon weapon)
        {
            return false;
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
        
        /// <summary>
        /// If the Server->Client packet Attack contains at least 1 hit, send the Server->Client packet Attack
        /// to the Character AND to all PlayerInstance in the _KnownPlayers of the Character 
        /// </summary>
        /// <param name="attackHit"></param>
        /// <param name="ssGrade"></param>
        private async Task SendHitAnimation(AttackHit attackHit, CrystalType ssGrade)
        {
            Attack attack = new Attack(_character, attackHit.IsSoulShot, (int)ssGrade);
            attack.AddHit(attackHit.HitTarget.ObjectId, attackHit.Damage, attackHit.IsMissedHit, attackHit.IsCriticalHit, attackHit.IsShield);
            if (attack.HasHits())
            {
                await _character.SendPacketAsync(attack);
                await _character.SendToKnownPlayers(attack);
                //DischargeSoulShot(weapon, isSoulShotCharged);
            }
        }
        
        private AttackHit GetAttackHit(Weapon weapon, Character target, bool isSoulShotCharged)
        {
            // Calculate if hit is missed or not
            var missAttack = CalculateSkill.CalcHitMiss(_character, target);
            // Calculate if shield defense is efficient
            var isShield = CalculateSkill.CalcShieldUse(_character, target);
            // Calculate if hit is critical
            var isCritical = CalculateSkill.CalcCrit(_character.CharacterCombat().GetCriticalRate());
            
            // Select the type of attack to start
            if (weapon.WeaponType == WeaponType.Pole)
            {
                //return GetAttackHitByPole(attack, target, timeToHit);
            }
            if (weapon.WeaponType == WeaponType.Dual)
            {
                //return GetAttackHitByDual(attack, target, timeToHit);
            }
            // Calculate physical damages
            var damage = (int) CalculateSkill.CalcPhysDam(_character, target, isShield, isCritical, false, isSoulShotCharged);
            return new AttackHit(_character, target, damage, isShield, isCritical, missAttack, isSoulShotCharged);
        }
    }
}