using System.Collections.Generic;

namespace Core.Module.NpcData
{
    public record NpcStat
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public float AcquireExpRate { get; set; }
        public int AcquireSp { get; set; }
        public bool UnSowing { get; set; }
        public IList<string> Clan { get; set; }
        public IList<string> IgnoreClanList { get; set; }
        public int ClanHelpRange { get; set; }
        public string SlotChest { get; set; }
        public string SlotRHand { get; set; }
        public string SlotLHand { get; set; }
        public short ShieldDefenseRate { get; set; }
        public short ShieldDefense { get; set; }
        public IList<string> SkillList { get; set; }
        public NpcAiData NpcAiData { get; set; }
        public string Category { get; set; }
        public string Race { get; set; }
        public string Sex { get; set; }
        public bool UnDying { get; set; }
        public short CanBeAttacked { get; set; }
        public short CorpseTime { get; set; }
        public bool NoSleepMode { get; set; }
        public short AgroRange { get; set; }
        public IList<float> GroundHigh { get; set; }
        public IList<float> GroundLow { get; set; }
        public double Exp { get; set; }
        public float OrgHp { get; set; }
        public float OrgHpRegen { get; set; }
        public float OrgMp { get; set; }
        public float OrgMpRegen { get; set; }
        public IList<float> CollisionRadius { get; set; }
        public IList<float> CollisionHeight { get; set; }
        public byte Str { get; set; }
        public byte Int { get; set; }
        public byte Dex { get; set; }
        public byte Wit { get; set; }
        public byte Con { get; set; }
        public byte Men { get; set; }
        public short StrBonus { get; set; }
        public short IntBonus { get; set; }
        public short DexBonus { get; set; }
        public short WitBonus { get; set; }
        public short ConBonus { get; set; }
        public short MenBonus { get; set; }
        public float LevelBonus { get; set; }
        public string BaseAttackType { get; set; }
        public short BaseAttackRange { get; set; }
        public IList<float> BaseDamageRange { get; set; }
        public short BaseRandDam { get; set; }
        public float BasePhysicalAttack { get; set; }
        public short BaseCritical { get; set; }
        public double PhysicalHitModify { get; set; }
        public float BaseAttackSpeed { get; set; } = 300;
        public float BaseMagicalAttackSpeed { get; set; } = 333;
        public int BaseReuseDelay { get; set; }
        public float BaseMagicAttack { get; set; }
        public float BaseDefend { get; set; }
        public float BaseMagicDefend { get; set; }
        public float PhysicalAvoidModify { get; set; }
        public byte SoulShotCount { get; set; }
        public byte SpiritShotCount { get; set; }
        public float HitTimeFactor { get; set; }
        public string ItemMakeList { get; set; }
        public string CorpseMakeList { get; set; }
        public IList<NpcAdditionalListData> AdditionalMakeList { get; set; }
        public IList<NpcAdditionalListData> AdditionalMakeMultiList { get; set; }
        public short HpIncrease { get; set; }
        public short MpIncrease { get; set; }
        public short SafeHeight { get; set; }
    }
}