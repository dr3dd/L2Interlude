using Core.Module.CharacterData.Template.Race;

namespace Core.Module.CharacterData.Template
{
    public class HumanMagician : Human
    {
        private const byte Int = 41;
        private const byte Str = 22;
        private const byte Con = 27;
        private const byte Men = 39;
        private const byte Dex = 21;
        private const byte Wit = 20;
        
        public byte GetInt()
        {
            return Int;
        }

        public byte GetStr()
        {
            return Str;
        }

        public byte GetCon()
        {
            return Con;
        }

        public byte GetMen()
        {
            return Men;
        }

        public byte GetDex()
        {
            return Dex;
        }

        public byte GetWit()
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
        
        public int GetBaseDefendUpperBody()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendLowerBody()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendPitch()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendBoots()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendGloves()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendUnderwear()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseDefendMantle()
        {
            throw new System.NotImplementedException();
        }
        
        public int GetBaseGroundHighSpeed()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseGroundLowSpeed()
        {
            throw new System.NotImplementedException();
        }
        
        public int GetBaseMagicDefendLeftEarring()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseMagicDefendRightEarring()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseMagicDefendLeftRing()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseMagicDefendRightRing()
        {
            throw new System.NotImplementedException();
        }

        public int GetBaseMagicDefendNecklace()
        {
            throw new System.NotImplementedException();
        }
    }
}