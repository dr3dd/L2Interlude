namespace Core.Module.CharacterData.Template.Class
{
    public class Mage : HumanMagician, ITemplateHandler
    {
        private const byte ClassId = 10;
        public byte GetClassId()
        {
            return ClassId;
        }
    }
}