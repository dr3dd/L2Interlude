namespace NpcAi
{
    public class HandleNpcDelete<T>
    {
        private readonly T _handleNpc;
        public HandleNpcDelete(string className, string npcType)
        {
            var namespaceName = "NpcAi.Ai.Npc" + char.ToUpper(npcType[0]) + npcType.Substring(1);
            var classAiName = namespaceName + "." + className;
            var objectType = Type.GetType(classAiName);
            var defaultNpc = (T)Activator.CreateInstance(objectType!)!;

            _handleNpc = defaultNpc;
        }

        public T GetNpcHandler()
        {
            return _handleNpc;
        }
    }
}