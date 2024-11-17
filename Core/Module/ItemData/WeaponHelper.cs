namespace Core.Module.ItemData;

public class WeaponHelper
{
    public static WeaponType GetWeaponType(string weaponType)
    {
        return weaponType.ToLower() switch
        {
            "none" => WeaponType.None,
            "sword" => WeaponType.Sword,
            "dagger" => WeaponType.Dagger,
            "blunt" => WeaponType.Blunt,
            "pole" => WeaponType.Pole,
            "bow" => WeaponType.Bow,
            "dualfist" => WeaponType.DualFist,
            "fist" => WeaponType.Fist,
            "dual" => WeaponType.Dual,
            "etc" => WeaponType.Etc,
            _ => WeaponType.None
        };
    }
}