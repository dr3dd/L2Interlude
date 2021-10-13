using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.Module.SkillData.Effects
{
    public class PSpeed : Effect
    {
        private readonly int _effectSpeed;
        private readonly int _abnormalTime;
        public PSpeed(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _effectSpeed = Convert.ToInt32(param[2]);
            _abnormalTime = skillDataModel.AbnormalTime;
            SkillDataModel = skillDataModel;
        }

        public override async Task Process(PlayerInstance playerInstance)
        {
            await StartEffectTask(_abnormalTime * 1000, playerInstance);
        }

        public int GetEffectSpeed()
        {
            return _effectSpeed;
        }
    }
}