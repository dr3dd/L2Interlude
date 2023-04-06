using System.Collections.Concurrent;
using Core.Module.CharacterData;
using Core.Module.WorldData;

namespace Core.Module.DoorData
{
    public class DoorKnownList : ICharacterKnownList
    {
        private DoorInstance _doorInstance;
        private readonly ConcurrentDictionary<int, WorldObject> _doorKnownList;
        
        public DoorKnownList(DoorInstance doorInstance)
        {
            _doorKnownList = new ConcurrentDictionary<int, WorldObject>();
            _doorInstance = doorInstance;
        }
        
        public void AddToKnownList(int objectId, WorldObject worldObject)
        {
            _doorKnownList.TryAdd(objectId, worldObject);
        }
        
        public bool HasObjectInKnownList(int objectId)
        {
            return _doorKnownList.ContainsKey(objectId);
        }

        public ConcurrentDictionary<int, WorldObject> GetKnownObjects()
        {
            return _doorKnownList;
        }

        public void RemoveKnownObject(WorldObject worldObject)
        {
            if (worldObject == null)
            {
                return;
            }
            _doorKnownList.TryRemove(worldObject.ObjectId, out _);
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