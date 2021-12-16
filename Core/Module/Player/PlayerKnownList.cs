using System.Collections.Concurrent;
using Core.Module.WorldData;

namespace Core.Module.Player
{
    public class PlayerKnownList
    {
        private PlayerInstance _playerInstance;
        private readonly ConcurrentDictionary<int, WorldObject> _playerKnownList;
        
        public PlayerKnownList(PlayerInstance playerInstance)
        {
            _playerKnownList = new ConcurrentDictionary<int, WorldObject>();
            _playerInstance = playerInstance;
        }

        public void AddToKnownList(int objectId, WorldObject worldObject)
        {
            _playerKnownList.TryAdd(objectId, worldObject);
        }
        
        public bool HasObjectInKnownList(int objectId)
        {
            return _playerKnownList.ContainsKey(objectId);
        }

        public ConcurrentDictionary<int, WorldObject> GetKnownObjects()
        {
            return _playerKnownList;
        }
        
        public void RemoveKnownObject(WorldObject worldObject)
        {
            if (worldObject == null)
            {
                return;
            }
            _playerKnownList.TryRemove(worldObject.ObjectId, out _);
        }
        
        public void RemoveAllKnownObjects()
        {
            GetKnownObjects().Clear();
        }
    }
}