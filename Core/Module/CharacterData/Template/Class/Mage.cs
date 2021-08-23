using System.Collections.Generic;

namespace Core.Module.CharacterData.Template.Class
{
    public class Mage : HumanMagician, ITemplateHandler
    {
        private const byte ClassId = 10;
        private const string ClassKey = "mage";

        public Mage(PcParameterInit pcParameterInit)
        {
            
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
            throw new System.NotImplementedException();
        }

        public float GetHpBegin(byte level)
        {
            throw new System.NotImplementedException();
        }

        public float GetMpBegin(byte level)
        {
            throw new System.NotImplementedException();
        }
    }
}