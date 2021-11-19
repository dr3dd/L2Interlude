using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;
using L2Logger;

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
        public override async Task Process(PlayerInstance playerInstance)
        {
            LoggerManager.Info($"Magic Attack: {_magicDamage}");
            await Task.CompletedTask;
        }
    }
}