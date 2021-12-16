using System.Collections.Concurrent;
using Core.Module.Player;
using Core.Module.WorldData;

namespace Core.Module.NpcData
{
    public class NpcKnownList
    {
        private NpcInstance _npcInstance;
        private readonly ConcurrentDictionary<int, PlayerInstance> _npcKnownList;
        
        public NpcKnownList(NpcInstance npcInstance)
        {
            _npcKnownList = new ConcurrentDictionary<int, PlayerInstance>();
            _npcInstance = npcInstance;
        }
        
        public void AddToKnownList(int objectId, PlayerInstance playerInstance)
        {
            _npcKnownList.TryAdd(objectId, playerInstance);
        }
        
        public bool HasObjectInKnownList(int objectId)
        {
            return _npcKnownList.ContainsKey(objectId);
        }

        public ConcurrentDictionary<int, PlayerInstance> GetKnownPlayers()
        {
            return _npcKnownList;
        }
        
        public void RemoveKnownObject(PlayerInstance playerInstance)
        {
            if (playerInstance == null)
            {
                return;
            }
            _npcKnownList.TryRemove(playerInstance.ObjectId, out _);
        }
        
        public void RemoveAllKnownObjects()
        {
            GetKnownPlayers().Clear();
        }
    }
}