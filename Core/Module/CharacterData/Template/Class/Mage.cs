using System.Collections.Generic;

namespace Core.Module.CharacterData.Template.Class
{
    public class Mage : HumanMagician, ITemplateHandler
    {
        private const byte ClassId = 10;
        private const string ClassKey = "mage";
        
        private readonly IDictionary<byte, float> _cpTable;
        private readonly IDictionary<byte, float> _hpTable;
        private readonly IDictionary<byte, float> _mpTable;

        public Mage(PcParameterInit pcParameter)
        {
            var result = pcParameter.GetResult();
            var fighterCp = result[$"{ClassKey}_cp"];
            var fighterHp = result[$"{ClassKey}_hp"];
            var fighterMp = result[$"{ClassKey}_mp"];
            _cpTable = (IDictionary<byte, float>) fighterCp;
            _hpTable = (IDictionary<byte, float>) fighterHp;
            _mpTable = (IDictionary<byte, float>) fighterMp;
        }
        
        public byte GetClassId()
        {
            return ClassId;
        }

        public string GetClassKey()
        {
            return ClassKey;
        }

        public float GetCpBegin(byte level)
        {
            return _cpTable[level];
        }
        public float GetHpBegin(byte level)
        {
            return _hpTable[level];
        }
        public float GetMpBegin(byte level)
        {
            return _mpTable[level];
        }
    }
}