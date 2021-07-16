using Core.Module.CharacterData.Template.Race;

namespace Core.Module.CharacterData.Template
{
    public class HumanFighter : Human
    {
        public const int BasePhysicalAttack = 4;
        public const int BaseCritical = 4;
        public const string BaseAttackType = "fist";
        public const int BaseAttackSpeed = 300;
        public const int BaseDefend = 0;
        public const int BaseMagicAttack = 6;
        public const int BaseMagicDefend = 0;
        public const int BaseAttackRange = 20;
        public const int BaseDamageRange = 0;
        public const int BaseRandDam = 10;

        private const int Int = 21;
        private const int Str = 40;
        private const int Con = 43;
        private const int Men = 25;
        private const int Dex = 30;
        private const int Wit = 11;

        public int GetInt()
        {
            return Int;
        }
        public int GetStr()
        {
            return Str;
        }
        public int GetCon()
        {
            return Con;
        }
        public int GetMen()
        {
            return Men;
        }
        public int GetDex()
        {
            return Dex;
        }
        public int GetWit()
        {
            return Wit;
        }
    }
}