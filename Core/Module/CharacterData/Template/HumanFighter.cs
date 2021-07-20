using System.Collections.Generic;
using Core.Module.CharacterData.Template.Race;
using Helpers;

namespace Core.Module.CharacterData.Template
{
    public abstract class HumanFighter : Human
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
    }
}