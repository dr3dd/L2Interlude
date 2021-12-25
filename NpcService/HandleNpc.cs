using System;
using System.Collections.Concurrent;

namespace NpcService
{
    public class HandleNpc<T>
    {
        private readonly ConcurrentDictionary<string, T> _concurrentDictionary;
        private readonly IServiceProvider _serviceProvider;

        public HandleNpc(IServiceProvider serviceProvider)
        {
            _concurrentDictionary = new ConcurrentDictionary<string, T>();
            _serviceProvider = serviceProvider;
        }

        public T TestHandleNpc(string npcKeyId, string className, string npcName, string npcType)
        {
            if (_concurrentDictionary.ContainsKey(npcKeyId))
            {
                return _concurrentDictionary[npcKeyId];
            }

            var namespaceName = "NpcService.Ai.Npc" + char.ToUpper(npcType[0]) + npcType.Substring(1);
            var objectType = Type.GetType(namespaceName  + "." + className);
            var defaultNpc = (T)Activator.CreateInstance(
                objectType ?? throw new InvalidOperationException(npcName));
            _concurrentDictionary.TryAdd(npcKeyId, defaultNpc);
            return defaultNpc;
        }
    }
}