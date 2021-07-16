namespace Core.Module.CharacterData.Template.Class
{
    public class Fighter : HumanFighter, ITemplateHandler
    {
        private const byte ClassId = 0;

        public byte GetClassId()
        {
            return ClassId;
        }
    }
}