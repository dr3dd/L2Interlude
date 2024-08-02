namespace Core.Module.ParserEngine
{
    public struct ItemBegin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public string SlotBitType { get; set; }
        public string ArmorType { get; set; }
        public string EtcItemType { get; set; }
        public int RecipeId { get; set; }
        public int Blessed { get; set; }
        public int Weight { get; set; }
        public string DefaultAction { get; set; }
        public string ConsumeType { get; set; }
        public int InitialCount { get; set; }
        public int MaximumCount { get; set; }
        public byte SoulShotCount { get; set; }
        public byte SpiritShotCount { get; set; }
        public int Price { get; set; }
        public int DefaultPrice { get; set; }
        public string ItemSkill { get; set; }
        public string MaterialType { get; set; }
        public bool IsTrade { get; set; }
        public bool IsDestruct { get; set; }
        public int MagicalDamage { get; set; }
        public int PhysicalDamage { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicalDefense { get; set; }
        public float HitModify { get; set; } //increase or decrease accuracy
        public string WeaponType { get; set; }
        public int Critical { get; set; }
        public int AttackSpeed { get; set; }
        public int AttackRange { get; set; }
        public int ReuseDelay { get; set; }
        public string CrystalType { get; set; }
    }
}