using System.Collections.Generic;

namespace Core.Module.CharacterData.Template.Class
{
    public class Fighter : HumanFighter, ITemplateHandler
    {
        private const byte ClassId = 0;

        private readonly IDictionary<int, float> _cpTable;
        private readonly IDictionary<int, float> _hpTable;
        private readonly IDictionary<int, float> _mpTable;

        public Fighter()
        {
            _cpTable = new Dictionary<int, float>();
            _hpTable = new Dictionary<int, float>();
            _mpTable = new Dictionary<int, float>();
            InitCpTable();
            InitHpTable();
            InitMpTable();
        }

        private void InitMpTable()
        {
            _mpTable.Add(1, 30);
            _mpTable.Add(2, 35.46f);
            _mpTable.Add(3, 40.98f);
            _mpTable.Add(4, 46.56f);
            _mpTable.Add(5, 52.2f);
            _mpTable.Add(6, 57.9f);
            _mpTable.Add(7, 63.66f);
        }

        private void InitCpTable()
        {
            _cpTable.Add(1, 32);
            _cpTable.Add(2, 36.732f);
            _cpTable.Add(3, 41.516f);
            _cpTable.Add(4, 46.352f);
            _cpTable.Add(5, 51.24f);
            _cpTable.Add(6, 56.18f);
            _cpTable.Add(7, 61.172f);
        }

        private void InitHpTable()
        {
            _hpTable.Add(1, 80);
            _hpTable.Add(2, 91.83f);
            _hpTable.Add(3, 103.79f);
            _hpTable.Add(4, 115.88f);
            _hpTable.Add(5, 128.1f);
            _hpTable.Add(6, 140.45f);
            _hpTable.Add(7, 152.93f);
        }

        public byte GetClassId()
        {
            return ClassId;
        }

        public float GetCpBegin(int level)
        {
            return _cpTable[level];
        }
        public float GetHpBegin(int level)
        {
            return _hpTable[level];
        }
        public float GetMpBegin(int level)
        {
            return _mpTable[level];
        }
    }
}