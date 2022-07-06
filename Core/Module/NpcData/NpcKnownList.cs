using System.Collections.Concurrent;
using Core.Module.CharacterData;
using Core.Module.WorldData;

namespace Core.Module.NpcData
{
    public class NpcKnownList : ICharacterKnownList
    {
        private NpcInstance _npcInstance;
        private readonly ConcurrentDictionary<int, WorldObject> _npcKnownList;
        
        public NpcKnownList(NpcInstance npcInstance)
        {
            _npcKnownList = new ConcurrentDictionary<int, WorldObject>();
            _npcInstance = npcInstance;
        }
        
        public void AddToKnownList(int objectId, WorldObject worldObject)
        {
            _npcKnownList.TryAdd(objectId, worldObject);
        }
        
        public bool HasObjectInKnownList(int objectId)
        {
            return _npcKnownList.ContainsKey(objectId);
        }

        public ConcurrentDictionary<int, WorldObject> GetKnownObjects()
        {
            return _npcKnownList;
        }

        public void RemoveKnownObject(WorldObject worldObject)
        {
            if (worldObject == null)
            {
                return;
            }
            _npcKnownList.TryRemove(worldObject.ObjectId, out _);
        }
        
        public void RemoveAllKnownObjects()
        {
            GetKnownObjects().Clear();
        }

        public void RemoveMeFromKnownObjects()
        {
            throw new System.NotImplementedException();
        }
    }
}