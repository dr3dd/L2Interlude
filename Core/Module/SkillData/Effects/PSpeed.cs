using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.SkillData.Effects
{
    public class PSpeed : Effect
    {
        private readonly int _effectSpeed;
        private readonly int _abnormalTime;
        private readonly IReadOnlyList<string> _params;
        public PSpeed(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _params = param;
            _effectSpeed = Convert.ToInt32(param[2]);
            _abnormalTime = skillDataModel.AbnormalTime;
            SkillDataModel = skillDataModel;
        }

        public override async Task Process(PlayerInstance playerInstance, Character targetInstance)
        {
            var effectResult = CanPlayerUseSkill(playerInstance, targetInstance);
            if (effectResult.IsNotValid)
            {
                await playerInstance.SendPacketAsync(new SystemMessage(effectResult.SystemMessageId));
                return;
            }
            await StartEffectTask(_abnormalTime * 1000, targetInstance);
        }

        public int GetEffectSpeed()
        {
            return _effectSpeed;
        }
    }
}