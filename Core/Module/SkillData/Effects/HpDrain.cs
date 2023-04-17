using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.Module.SkillData.Effects
{
    public class HpDrain : Effect
    {
        public HpDrain(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            var s = param;
        }
        public override Task Process(Character currentInstance, Character targetInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}