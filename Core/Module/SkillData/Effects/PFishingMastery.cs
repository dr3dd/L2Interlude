using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.Module.SkillData.Effects;

public class PFishingMastery : Effect
{
    public PFishingMastery(IList<string> param, SkillDataModel skillDataModel)
    {
        
    }
    
    public override Task Process(Character currentInstance, Character targetInstance)
    {
        throw new System.NotImplementedException();
    }
}