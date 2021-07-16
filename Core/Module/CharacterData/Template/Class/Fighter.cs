namespace Core.Module.CharacterData.Template.Class
{
    public class Fighter : HumanFighter, ITemplateHandler
    {
        private const int ClassId = 0;

        public int GetClassId()
        {
            return ClassId;
        }
    }
}