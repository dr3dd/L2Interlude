using System;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using L2Logger;

namespace Core.Module.CharacterData.PhysicalAttack
{
    public class ProcessHit : IRunnable
    {
	    private readonly AttackHit _attackHit;
	    private readonly Character _character;
	    private readonly Character _hitTarget;
        public ProcessHit(AttackHit attackHit)
        {
	        _attackHit = attackHit;
	        _character = _attackHit.Character;
	        _hitTarget = _attackHit.HitTarget;
        }

        public async Task Run()
        {
            try
            {
                await OnHitProcess();
            }
            catch (Exception e)
            {
                LoggerManager.Info("fixme:hit task unhandled exception " + e);
            }
        }

        private async Task OnHitProcess()
		{
			// If the attacker/target is dead or use fake death, notify the AI with EVT_CANCEL
			// and send a Server->Client packet ActionFailed (if attacker is a PlayerInstance)
			
			if (_character.PhysicalAttack().IsAttackAborted())
			{
				_character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtCancel);
				return;
			}
			var damage = _attackHit.Damage;
			var isCriticalHit = _attackHit.IsCriticalHit;
			var isMissedHit = _attackHit.IsMissedHit;
			
			// If attack isn't aborted, send a message system (critical hit, missed...) to attacker/target if they are PlayerInstance
			await SendDamageMessage(damage, isMissedHit, isCriticalHit);
			if (_attackHit.IsMissedHit)
			{
				SystemMessage sm = new SystemMessage(SystemMessageId.AvoidedS1Attack);
				sm.AddString(_character.CharacterName);
				await _hitTarget.SendPacketAsync(sm);
				return;
			}
			
			if (damage > 0)
			{
				var weapon = _character.GetActiveWeaponItem();
				ReflectedDamage(weapon, damage);
				//await target.ReduceCurrentHpAsync(damage, _character);
				_hitTarget.CharacterStatus().DecreaseCurrentHp(damage);
				await _hitTarget.SendStatusUpdate();
				// Notify AI with EVT_ATTACKED
				_hitTarget.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtAttacked, _character);
				await _character.CharacterNotifyEvent().ClientStartAutoAttackAsync();
			}
			
			// Launch weapon Special ability effect if available
			/*
			Weapon activeWeapon = _character.GetActiveWeaponItem();
			if (activeWeapon != null)
			{
				//activeWeapon.GetSkillEffects(this, target, crit);
			}
			*/
			return;
		}

        private void ReflectedDamage(Weapon weapon, int damage)
        {
	        var isBow = weapon.WeaponType == WeaponType.Bow;
	        if (!isBow) // Do not reflect or absorb if weapon is of type bow
	        {
		        // Absorb HP from the damage inflicted
		        double absorbPercent = 0;
		        //double absorbPercent = _character.Stat.GetCalc().CalcStat(CharacterStatId.AbsorbDamagePercent, 0, null, null);
		        if (absorbPercent > 0)
		        {
			        int maxCanAbsorb = (int) (_character.CharacterBaseStatus().GetMaxHp() - _character.CharacterStatus().CurrentHp);
			        int absorbDamage = (int) ((absorbPercent / 100) * damage);
			        if (absorbDamage > maxCanAbsorb)
			        {
				        absorbDamage = maxCanAbsorb; // Can't absord more than max hp
			        }
						
			        if (absorbDamage > 0)
			        {
				        //setCurrentHp(getStatus().getCurrentHp() + absorbDamage);
			        }
		        }
					
		        // Reduce HP of the target and calculate reflection damage to reduce HP of attacker if necessary
		        /*
		        double reflectPercent = target.Stat.GetCalc().CalcStat(CharacterStatId.ReflectDamagePercent, 0, null, null);
		        if (reflectPercent > 0)
		        {
			        int reflectedDamage = (int) ((reflectPercent / 100) * damage);
			        damage -= reflectedDamage;
			        if (reflectedDamage > target.Stat.GetMaxHp())
			        {
				        reflectedDamage = (int) target.Stat.GetMaxHp();
			        }
			        
			        //getStatus().reduceHp(reflectedDamage, target, true);
		        }
		        */
	        }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="isMissedHit"></param>
        /// <param name="isPhysicalCriticalHit"></param>
        private async Task SendDamageMessage(int damage, bool isMissedHit, bool isPhysicalCriticalHit)
        {
	        // Check if hit is missed
	        if (isMissedHit)
	        {
		        await _character.SendPacketAsync(new SystemMessage(SystemMessageId.MissedTarget));
		        return;
	        }
	        // Check if hit is critical
	        if (isPhysicalCriticalHit)
	        {
		        await _character.SendPacketAsync(new SystemMessage(SystemMessageId.CriticalHit));
	        }
	        await _character.SendPacketAsync(new SystemMessage(SystemMessageId.YouDidS1Dmg).AddNumber(damage));
	        await _hitTarget.SendPacketAsync(new SystemMessage(SystemMessageId.S1GaveYouS2Dmg)
		        .AddString(_character.CharacterName).AddNumber(damage));
        }
    }
}