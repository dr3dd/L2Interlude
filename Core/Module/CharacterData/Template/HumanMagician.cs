using Core.Module.CharacterData.Template.Race;

namespace Core.Module.CharacterData.Template
{
    public class HumanMagician : Human
    {
        private const int Int = 41;
        private const int Str = 22;
        private const int Con = 27;
        private const int Men = 39;
        private const int Dex = 21;
        private const int Wit = 20;
        
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
        
        public Location GetInitialStartPoint()
        {
            throw new System.NotImplementedException();
        }

        public int GetBasePhysicalAttack()
        {
            return 1;
        }
        public int GetBaseAttackSpeed()
        {
            return 1;
        }

        public int GetBaseCritical()
        {
            return 1;
        }

        public int GetBaseDefend()
        {
            return 1;
        }

        public int GetBaseMagicAttack()
        {
            return 1;
        }

        public int GetBaseMagicDefend()
        {
            return 1;
        }

        public int GetBaseAttackRange()
        {
            return 1;
        }
    }
}