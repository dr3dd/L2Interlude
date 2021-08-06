using System.Collections.Generic;

namespace Core.Module.CharacterData.Template
{
    public class BasicStatBonusInit
    {
        private readonly IDictionary<byte, float> _levelBonus;
        private readonly IDictionary<int, short> _strBonus;
        private readonly IDictionary<int, short> _intBonus;
        private readonly IDictionary<int, short> _conBonus;
        private readonly IDictionary<int, short> _menBonus;
        private readonly IDictionary<int, short> _dexBonus;
        private readonly IDictionary<int, short> _witBonus;

        public BasicStatBonusInit()
        {
            _levelBonus = new Dictionary<byte, float>();
            _strBonus = new Dictionary<int, short>();
            _intBonus = new Dictionary<int, short>();
            _conBonus = new Dictionary<int, short>();
            _menBonus = new Dictionary<int, short>();
            _dexBonus = new Dictionary<int, short>();
            _witBonus = new Dictionary<int, short>();
            LevelBonusInit();
            StrBonusInit();
            IntBonusInit();
            ConBonusInit();
            MenBonusInit();
            DexBonusInit();
            WitBonusInit();
        }
        private void LevelBonusInit()
        {
            _levelBonus.Add(1, 0.9f);
            _levelBonus.Add(2, 0.91f);
            _levelBonus.Add(3, 0.92f);
            _levelBonus.Add(4, 0.93f); 
            _levelBonus.Add(5, 0.94f); 
            _levelBonus.Add(7, 0.96f);
        }

        private void StrBonusInit()
        {
            _strBonus.Add(35, 1);
            _strBonus.Add(36, 4);
            _strBonus.Add(37, 8);
            _strBonus.Add(38, 12);
            _strBonus.Add(39, 16);
            _strBonus.Add(40, 20);
            _strBonus.Add(41, 24);
            _strBonus.Add(42, 29);
            _strBonus.Add(43, 33);
            _strBonus.Add(44, 38);
            _strBonus.Add(45, 43);
            _strBonus.Add(46, 48);
            _strBonus.Add(47, 54);
            _strBonus.Add(48, 59);
            _strBonus.Add(49, 65);
            _strBonus.Add(50, 71);
        }

        private void IntBonusInit()
        {
            _intBonus.Add(20, -20);
            _intBonus.Add(21, -19);
            _intBonus.Add(22, -17);
            _intBonus.Add(23, -15);
            _intBonus.Add(24, -14);
            _intBonus.Add(25, -12);
            _intBonus.Add(26, -10);
            _intBonus.Add(27, -8);
            _intBonus.Add(28, -6);
            _intBonus.Add(29, -5);
            _intBonus.Add(30, -3);
            _intBonus.Add(31, -1);
            _intBonus.Add(32, 1);
            _intBonus.Add(33, 3);
            _intBonus.Add(34, 5);
            _intBonus.Add(35, 7);
            _intBonus.Add(36, 10);
            _intBonus.Add(37, 12);
            _intBonus.Add(38, 14);
            _intBonus.Add(39, 16);
            _intBonus.Add(40, 19);
            _intBonus.Add(41, 21);
            _intBonus.Add(42, 23);
            _intBonus.Add(43, 26);
            _intBonus.Add(44, 28);
            _intBonus.Add(45, 31);
            _intBonus.Add(46, 34);
            _intBonus.Add(47, 36);
            _intBonus.Add(48, 39);
            _intBonus.Add(49, 42);
            _intBonus.Add(50, 45);
        }

        private void ConBonusInit()
        {
            _conBonus.Add(28, 1);
            _conBonus.Add(29, 4);
            _conBonus.Add(30, 7);
            _conBonus.Add(31, 10);
            _conBonus.Add(32, 14);
            _conBonus.Add(33, 17);
            _conBonus.Add(34, 21);
            _conBonus.Add(35, 24);
            _conBonus.Add(36, 28);
            _conBonus.Add(37, 32);
            _conBonus.Add(38, 36);
            _conBonus.Add(39, 40);
            _conBonus.Add(40, 44);
            _conBonus.Add(41, 48);
            _conBonus.Add(42, 53);
            _conBonus.Add(43, 58);
            _conBonus.Add(44, 62);
            _conBonus.Add(45, 67);
            _conBonus.Add(46, 72);
            _conBonus.Add(47, 77);
            _conBonus.Add(48, 83);
            _conBonus.Add(49, 88);
            _conBonus.Add(50, 94);
        }

        private void MenBonusInit()
        {
            _menBonus.Add(20, 22);
            _menBonus.Add(21, 23);
            _menBonus.Add(22, 25);
            _menBonus.Add(23, 26);
            _menBonus.Add(24, 27);
            _menBonus.Add(25, 28);
            _menBonus.Add(26, 30);
            _menBonus.Add(27, 31);
            _menBonus.Add(28, 32);
            _menBonus.Add(29, 34);
            _menBonus.Add(30, 35);
            _menBonus.Add(31, 36);
            _menBonus.Add(32, 38);
            _menBonus.Add(33, 39);
            _menBonus.Add(34, 40);
            _menBonus.Add(35, 42);
            _menBonus.Add(36, 43);
            _menBonus.Add(37, 45);
            _menBonus.Add(38, 46);
            _menBonus.Add(39, 48);
            _menBonus.Add(40, 49);
        }
        
        private void DexBonusInit()
        {
            _dexBonus.Add(25, 5);
            _dexBonus.Add(26, 6);
            _dexBonus.Add(27, 7);
            _dexBonus.Add(28, 8);
            _dexBonus.Add(29, 9);
            _dexBonus.Add(30, 10);
            _dexBonus.Add(31, 11);
            _dexBonus.Add(32, 12);
            _dexBonus.Add(33, 13);
            _dexBonus.Add(34, 14);
            _dexBonus.Add(35, 15);
            _dexBonus.Add(36, 16);
            _dexBonus.Add(37, 17);
            _dexBonus.Add(38, 18);
            _dexBonus.Add(39, 19);
            _dexBonus.Add(40, 20);
            _dexBonus.Add(41, 21);
            _dexBonus.Add(42, 22);
            _dexBonus.Add(43, 24);
            _dexBonus.Add(44, 25);
            _dexBonus.Add(45, 26);
        }
        
        private void WitBonusInit()
        {
            _witBonus.Add(8, -44);
            _witBonus.Add(9, -42);
            _witBonus.Add(10, -39);
            _witBonus.Add(11, -36);
            _witBonus.Add(12, -32);
            _witBonus.Add(13, -29);
            _witBonus.Add(14, -25);
            _witBonus.Add(15, -22);
            _witBonus.Add(16, -18);
            _witBonus.Add(17, -14);
            _witBonus.Add(18, -9);
            _witBonus.Add(19, -5);
            _witBonus.Add(20, -0);
            _witBonus.Add(21, 5);
            _witBonus.Add(22, 10);
            _witBonus.Add(23, 16);
            _witBonus.Add(24, 22);
            _witBonus.Add(25, 28);
            _witBonus.Add(26, 34);
        }

        public float GetLevelBonus(byte level) => _levelBonus[level];
        public short GetStrBonus(int str) => _strBonus[str];
        public short GetIntBonus(int intelligence) => _intBonus[intelligence];
        public short GetConBonus(int con) => _conBonus[con];
        public short GetMenBonus(int men) => _menBonus[men];
        public short GetDexBonus(int dex) => _dexBonus[dex];
        public short GetWitBonus(int wit) => _witBonus[wit];
    }
}