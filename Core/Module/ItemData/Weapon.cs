namespace Core.Module.ItemData
{
    public class Weapon : ItemDataAbstract
    {
        public Weapon(ItemDataModel itemDataModel) : base(itemDataModel)
        {
        }
        public Weapon(int itemId, WeaponType weaponType) : base(itemId, weaponType)
        {
        }
    }
}