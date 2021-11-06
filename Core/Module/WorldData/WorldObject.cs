using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket.CharacterPacket;

namespace Core.Module.WorldData
{
    public abstract class WorldObject
    {
        public int ObjectId { get; set; }
        private ObjectPosition _position;
        
        public int GetX()
        {
            return WorldObjectPosition().GetX();
        }
	
        public int GetY()
        {
            return WorldObjectPosition().GetY();
        }
	
        public int GetZ()
        {
            return WorldObjectPosition().GetZ();
        }
        
        public void SetXYZ(int x, int y, int z)
        {
            WorldObjectPosition().SetXYZ(x, y, z);
        }
        
        public WorldRegionData GetWorldRegion()
        {
            return WorldObjectPosition().GetWorldRegion();
        }
        
        public ObjectPosition WorldObjectPosition()
        {
            return _position ??= new ObjectPosition(this);
        }

        public void SpawnMe(int x, int y, int z)
        {
            WorldObjectPosition().SetWorldPosition(x, y, z);
            WorldObjectPosition().SetWorldRegion(GetRegionByLocation());
            StoreWorldObject();
            AddVisibleObject();
            StorePlayerObject();
        }

        private void AddVisibleObject()
        {
            WorldRegionData worldRegion = WorldObjectPosition().GetWorldRegion();
            worldRegion.AddVisibleObject(this);
        }

        private void StorePlayerObject()
        {
            if (this is PlayerInstance playerInstance)
            {
                Initializer.WorldInit().StorePlayerObject(playerInstance);
                playerInstance.SendToKnownPlayers(new CharInfo(playerInstance));
            }
        }

        private WorldRegionData GetRegionByLocation()
        {
            return Initializer.WorldInit().GetRegion(WorldObjectPosition().GetWorldPosition());
        }

        /// <summary>
        /// Store World Object in collection
        /// </summary>
        private void StoreWorldObject()
        {
            Initializer.WorldInit().StoreWorldObject(this);
        }
    }
}