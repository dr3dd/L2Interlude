using Core.Module.ItemData;

namespace Core.Module.CharacterData.PhysicalAttack
{
    public interface ICharacterPhysicalAttackValidator
    {
        bool IsValid(Character target);
        bool IsValidBowAttack(WeaponType weaponType);
    }
}