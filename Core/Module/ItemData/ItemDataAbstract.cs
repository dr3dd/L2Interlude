namespace Core.Module.ItemData
{
    public abstract class ItemDataAbstract
    {
        public int ItemId { get; }
        public string Name { get; }
        public ItemType ItemType { get; }
        public SlotBitType SlotBitType { get; }
        public WeaponType WeaponType { get; }
        public ActionType ActionType { get; }
        public int MagicalDamage { get; }
        public int PhysicalDamage { get; }
        public int PhysicalDefense { get; }
        public int MagicalDefense { get; }
        public int AttackSpeed { get; }
        public float HitModify { get; } //increase or decrease accuracy
        public int Critical { get; }
        public int ReuseDelay { get; }
        public CrystalType CrystalType { get; }
        public int MaximumCount { get; }
        public int DefaultPrice { get; }
        public int Weight { get; set; }

        public ItemDataAbstract(int itemId, WeaponType weaponType)
        {
            ItemId = itemId;
            WeaponType = weaponType;
        }
        public ItemDataAbstract(ItemDataModel itemDataModel)
        {
            ItemId = itemDataModel.ItemId;
            Name = itemDataModel.Name;
            ItemType = itemDataModel.ItemType;
            SlotBitType = itemDataModel.SlotBitType;
            WeaponType = itemDataModel.WeaponType;
            ActionType = itemDataModel.ActionType;
            MagicalDamage = itemDataModel.MagicalDamage;
            PhysicalDamage = itemDataModel.PhysicalDamage;
            PhysicalDefense = itemDataModel.PhysicalDefense;
            MagicalDefense = itemDataModel.MagicallDefense;
            AttackSpeed = itemDataModel.AttackSpeed;
            HitModify = itemDataModel.HitModify;
            Critical = itemDataModel.Critical;
            ReuseDelay = itemDataModel.ReuseDelay;
            CrystalType = itemDataModel.CrystalType;
            MaximumCount = itemDataModel.MaximumCount;
            DefaultPrice = itemDataModel.DefaultPrice;
            Weight = itemDataModel.Weight;
        }
    }
}