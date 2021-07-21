using System.Collections.Generic;
using Core.Module.CharacterData.Template.Race;
using Helpers;

namespace Core.Module.CharacterData.Template
{
    public abstract class HumanFighter : Human
    {
        private const int BasePhysicalAttack = 4;
        private const int BaseCritical = 4;
        private const string BaseAttackType = "fist";
        private const int BaseAttackSpeed = 300;
        private const int BaseDefend = 80; //{Upper body; Lower body; Pitch; Boots; Gloves; Underwear; Mantle} {31;18;12;7;8;3;1}
        private const int BaseMagicAttack = 6;
        private const int BaseMagicDefend = 41; //{The earring which rises; Left earring; The ring which rises; Left ring; Neck} {9;9;5;5;13}
        private const int BaseAttackRange = 20;
        private const int BaseDamageRange = 0;
        private const int BaseRandDam = 10;

        private const int Int = 21;
        private const int Str = 40;
        private const int Con = 43;
        private const int Men = 25;
        private const int Dex = 30;
        private const int Wit = 11;

        private IList<string> _initialEquipment;
        private IList<Location> _initialStartPoint;

        protected HumanFighter()
        {
            InitialEquipment();
            InitialStartPoint();
        }

        private void InitialStartPoint()
        {
            _initialStartPoint = new List<Location>
            {
                new Location(-71338, 258271, -3104),
                new Location(-71417, 258270, -3104),
                new Location(-71453, 258305, -3104),
                new Location(-71467, 258378, -3104),
            };
        }

        private void InitialEquipment()
        {
            _initialEquipment = new List<string>
            {
                "squire_s_sword",
                "dagger",
                "squire_s_shirt",
                "squire_s_pants",
                "tutorial_guide"
            };
        }

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

        public IList<string> GetInitialEquipment()
        {
            return _initialEquipment;
        }

        public Location GetInitialStartPoint()
        {
            var rndItem = Rnd.Next(3);
            return _initialStartPoint[rndItem];
        }

        public int GetBasePhysicalAttack()
        {
            return BasePhysicalAttack;
        }

        public int GetBaseAttackSpeed()
        {
            return BaseAttackSpeed;
        }

        public int GetBaseCritical()
        {
            return BaseCritical;
        }

        public int GetBaseDefend()
        {
            return BaseDefend;
        }

        public int GetBaseMagicAttack()
        {
            return BaseMagicAttack;
        }

        public int GetBaseMagicDefend()
        {
            return BaseMagicDefend;
        }

        public int GetBaseAttackRange()
        {
            return BaseAttackRange;
        }
    }
}