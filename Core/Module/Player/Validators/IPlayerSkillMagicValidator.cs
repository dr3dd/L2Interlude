using System.Threading.Tasks;
using Core.Module.SkillData;

namespace Core.Module.Player.Validators
{
    public interface IPlayerSkillMagicValidator
    {
        Task<bool> IsValid(PlayerInstance playerInstance, SkillDataModel skill);
    }
}