using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using L2Logger;

namespace Core.Module.SkillData.Effects
{
    public class Heal : Effect
    {
        private readonly int _healPoint;

        public Heal(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _healPoint = Convert.ToInt32(param[1]);
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
            var heal = (_healPoint + Math.Sqrt(targetInstance.PlayerCombat().GetMagicalAttack()));
            targetInstance.PlayerStatus().IncreaseCurrentHp(heal);
            await targetInstance.SendPacketAsync(new StatusUpdate(targetInstance));
            
            LoggerManager.Info($"Magic Heal Points: {heal}");
            SystemMessage sm = new SystemMessage(SystemMessageId.S1HpRestored);
            sm.AddNumber((int) heal);
            await targetInstance.SendPacketAsync(sm);
        }
    }
}