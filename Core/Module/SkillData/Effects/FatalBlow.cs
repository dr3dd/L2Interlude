using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket;
using L2Logger;

namespace Core.Module.SkillData.Effects
{
    public class FatalBlow : Effect
    {
        private readonly int _damage;

        /// <summary>
        /// i_fatal_blow
        /// power
        /// blow rate
        /// crit rate
        /// </summary>
        /// <param name="param"></param>
        /// <param name="skillDataModel"></param>
        public FatalBlow(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _damage = Convert.ToInt32(param[1]);
            SkillDataModel = skillDataModel;
        }
        public override async Task Process(Character currentInstance, Character targetInstance)
        {
            var effectResult = CanPlayerUseSkill(currentInstance, targetInstance);
            if (effectResult.IsNotValid)
            {
                await currentInstance.SendPacketAsync(new SystemMessage(effectResult.SystemMessageId));
                return;
            }
            targetInstance.CharacterStatus().DecreaseCurrentHp(_damage);
            await SendStatusUpdate(targetInstance);
            LoggerManager.Info($"FatalBlow Attack: {_damage}");
        }
    }
}