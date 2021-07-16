namespace Core.Module.CharacterData.Template.Class
{
    public class Mage : HumanMagician, ITemplateHandler
    {
        private const int ClassId = 10;
        public int GetClassId()
        {
            return ClassId;
        }
    }
}