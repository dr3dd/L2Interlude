using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Core.Module.CharacterData.Template.Class
{
    public class ElvenFighter : ElfFighter, ITemplateHandler
    {
        private const byte ClassId = 18;
        private const string ClassKey = "elven_fighter";
        
        private readonly IDictionary<byte, float> _cpTable;
        private readonly IDictionary<byte, float> _hpTable;
        private readonly IDictionary<byte, float> _mpTable;
        private readonly IList<float> _collision; //collision Radius, Height

        public ElvenFighter(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var pcParameter = serviceProvider.GetRequiredService<PcParameterInit>();
            var result = pcParameter.GetResult();
            var fighterCp = result[$"{ClassKey}_cp"];
            var fighterHp = result[$"{ClassKey}_hp"];
            var fighterMp = result[$"{ClassKey}_mp"];
            _cpTable = (IDictionary<byte, float>) fighterCp;
            _hpTable = (IDictionary<byte, float>) fighterHp;
            _mpTable = (IDictionary<byte, float>) fighterMp;
            _collision = new List<float>{ 7.5f, 24f };
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
        public float GetCollisionRadius()
        {
            return _collision[0];
        }
        public float GetCollisionHeight()
        {
            return _collision[1];
        }
    }
}