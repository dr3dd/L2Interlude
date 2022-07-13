using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.SkillData.Effects
{
    public class FatalBlow : Effect
    {
        public override Task Process(Character currentInstance, Character targetInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}