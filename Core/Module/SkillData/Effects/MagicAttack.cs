using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.Module.SkillData.Effects
{
    public class MagicAttack : Effect
    {
        public MagicAttack(object param, SkillDataModel skillDataModel)
        {
            var d = param;
            SkillDataModel = skillDataModel;
        }
        public override Task Process(PlayerInstance playerInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}