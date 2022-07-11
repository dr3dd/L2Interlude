namespace Core.NetworkPacket.ServerPacket
{
    public class Hit
    {
        private const int HitFlagUsess = 0x10;
        private const int HitFlagCritical = 0x20;
        private const int HitFlagShield = 0x40;
        private const int HitFlagMiss = 0x80;

        public int TargetId { get; }
        public int Damage { get; }
        public int Flags { get; }

        public bool IsMiss { get; }
        public bool IsCritical { get; }
        public bool IsSoulshotUsed { get; }
        public int SoulshotGrade { get; }
        public bool Shield { get; }

        public Hit(int targetId, int damage, bool isMiss, bool isCritical, bool shield, bool isSoulshotUsed, int soulshotGrade)
        {
            TargetId = targetId;
            Damage = damage;
            IsMiss = isMiss;
            IsCritical = isCritical;
            Shield = shield;
            IsSoulshotUsed = isSoulshotUsed;
            SoulshotGrade = soulshotGrade;

            if (isMiss)
            {
                Flags = HitFlagMiss;
                return;
            }

            if (isSoulshotUsed)
            {
                Flags |= HitFlagUsess | soulshotGrade;
            }

            if (isCritical)
            {
                Flags |= HitFlagCritical;
            }

            if (shield)
            {
                Flags |= HitFlagShield;
            }
        }
    }
}