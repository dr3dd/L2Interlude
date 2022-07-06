using System.Collections.Concurrent;
using Core.Module.CharacterData;
using Core.Module.NpcData;
using Core.Module.WorldData;

namespace Core.Module.Player
{
    public class PlayerKnownList : ICharacterKnownList
    {
        private readonly PlayerInstance _playerInstance;
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

        public void RemoveMeFromKnownObjects()
        {
            foreach (var worldObject in _playerKnownList.Values)
            {
                switch (worldObject)
                {
                    case PlayerInstance playerInstance:
                        playerInstance.CharacterKnownList().RemoveKnownObject(_playerInstance);
                        break;
                    case NpcInstance npcInstance:
                        npcInstance.CharacterKnownList().RemoveKnownObject(_playerInstance);
                        break;
                }
            }
        }
    }
}