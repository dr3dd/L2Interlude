using System.Collections.Generic;
using System.Linq;
using Core.Module.SkillData;

namespace Core.Module.CharacterData
{
    public static class CalculateStats
    {
        public static void Calculate(IEnumerable<SkillDataModel> effects, AbnormalType abnormalType)
        {
            foreach (var skillDataModel in effects.Where(e => e.AbnormalType == abnormalType))
            {
                
            }
        }
    }
}