namespace Core.Module.CharacterData.Template.Class
{
    public class Mage : HumanMagician, ITemplateHandler
    {
        private const byte ClassId = 10;
        public byte GetClassId()
        {
            return ClassId;
        }

        public float GetCpBegin(int level)
        {
            throw new System.NotImplementedException();
        }

        public float GetHpBegin(int level)
        {
            throw new System.NotImplementedException();
        }

        public float GetMpBegin(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}