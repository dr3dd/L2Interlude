using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;
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
        
        public override async Task Process(PlayerInstance playerInstance)
        {
            LoggerManager.Info($"Magic Heal Points: {_healPoint}");
            await Task.CompletedTask;
        }
    }
}