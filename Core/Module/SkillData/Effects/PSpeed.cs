using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.Module.SkillData.Effects
{
    public class PSpeed : Effect
    {
        private int _effectSpeed;

        public override async Task Process(string[] param, SkillDataModel skill, PlayerInstance playerInstance)
        {
            _effectSpeed = Convert.ToInt32(param[2]);
            await Task.Run(() =>
            {
                SkillDataModel = skill;
                StartEffectTask(1200 * 1000, playerInstance);
            });
        }

        public int GetEffectSpeed()
        {
            return _effectSpeed;
        }
    }
}