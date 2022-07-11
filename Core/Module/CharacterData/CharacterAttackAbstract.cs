using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData
{
    public abstract class CharacterAttackAbstract
    {
        private readonly Character _character;
        protected int _attackEndTime;
        protected bool _attacking;

        protected CharacterAttackAbstract(Character character)
        {
            _character = character;
        }
        
        public int CalculateTimeBetweenAttacks(Character target, WeaponType weaponType)
        {
            int atkSpd;
            switch (weaponType)
            {
                case WeaponType.Bow:
                {
                    atkSpd = _character.GetPhysicalAttackSpeed();
                    return ((1500 * 345) / atkSpd);
                }
                case WeaponType.Dagger:
                {
                    atkSpd = _character.GetPhysicalAttackSpeed();
                    break;
                }
                default:
                {
                    atkSpd = _character.GetPhysicalAttackSpeed();
                    break;
                }
            }
            return CalculateSkill.CalcPAtkSpd(atkSpd);
        }
        
        /// <summary>
        /// Get the Attack Reuse Delay of the Weapon
        /// </summary>
        /// <param name="target"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public int CalculateReuseTime(Character target, Weapon weapon)
        {
            var reuse = weapon.ReuseDelay;
            // only bows should continue for now
            if (reuse == 0)
            {
                return 0;
            }

            reuse *= 1;// _character.Stat.GetReuseModifier(target);
		
            var atkSpd = _character.GetPhysicalAttackSpeed();

            return weapon.WeaponType switch
            {
                WeaponType.Bow => ((reuse * 345) / atkSpd),
                _ => ((reuse * 312) / atkSpd)
            };
        }

        protected void SetAttacking() => _attacking = true;
        public bool IsAttackingNow() => _attackEndTime > Initializer.TimeController().GetGameTicks();

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
}