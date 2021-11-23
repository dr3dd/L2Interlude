using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.Module.SkillData.Effects
{
    public class FatalBlow : Effect
    {
        public override Task Process(PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}