using System.Collections.Concurrent;
using Core.Module.WorldData;

namespace Core.Module.CharacterData
{
    public interface ICharacterKnownList
    {
        void AddToKnownList(int objectId, WorldObject worldObject);
        bool HasObjectInKnownList(int objectId);
        ConcurrentDictionary<int, WorldObject> GetKnownObjects();
        void RemoveKnownObject(WorldObject worldObject);
        void RemoveAllKnownObjects();
        void RemoveMeFromKnownObjects();
    }
}