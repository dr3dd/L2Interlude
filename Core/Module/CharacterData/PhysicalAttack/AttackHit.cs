namespace Core.Module.CharacterData.PhysicalAttack
{
    public readonly struct AttackHit
    {
        public Character Character { get; }
        public Character HitTarget { get; }
        public int Damage { get; }
        public bool IsShield { get; }
        public bool IsCriticalHit { get; }
        public bool IsMissedHit { get; }
        public bool IsSoulShot { get; }
        public AttackHit(Character character, Character hitTarget, int damage, bool isShield, bool isCritical,
            bool isMissed, bool isSoulShot)
        {
            Character = character;
            HitTarget = hitTarget;
            Damage = damage;
            IsShield = isShield;
            IsCriticalHit = isCritical;
            IsMissedHit = isMissed;
            IsSoulShot = isSoulShot;
        }
    }
}