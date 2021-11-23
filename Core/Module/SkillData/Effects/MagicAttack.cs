using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.SkillData.Effects
{
    public class MagicAttack : Effect
    {
        private readonly int _magicDamage;
        public MagicAttack(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _magicDamage = Convert.ToInt32(param[1]);
            SkillDataModel = skillDataModel;
        }
        public override async Task Process(PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            var effectResult = CanPlayerUseSkill(playerInstance, targetInstance);
            if (effectResult.IsNotValid)
            {
                await playerInstance.SendPacketAsync(new SystemMessage(effectResult.SystemMessageId));
                return;
            }
            
            var isMagicalCriticalHit = false;
            var isBss = false;
            var isSs = false;
            var damage = CalculateSkill.CalcMagicDam(playerInstance, targetInstance, _magicDamage, isSs, isBss, isMagicalCriticalHit);
            targetInstance.PlayerStatus().DecreaseCurrentHp(damage);
            await SendStatusUpdate(targetInstance);
            //LoggerManager.Info($"Magic Attack: {damage}"); debug
            await SendDamageMessage(playerInstance, targetInstance, damage, isMagicalCriticalHit);
        }

        private async Task SendDamageMessage(PlayerInstance playerInstance, PlayerInstance targetInstance, double damage,
            bool isMagicalCriticalHit)
        {
            await playerInstance.PlayerMessage().SendDamageMessageAsync(targetInstance, damage, isMagicalCriticalHit);
        }

        private async Task SendStatusUpdate(PlayerInstance targetInstance)
        {
            await targetInstance.SendPacketAsync(new StatusUpdate(targetInstance));
        }
    }
}