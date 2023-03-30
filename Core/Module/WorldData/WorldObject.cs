using System;
using System.Threading.Tasks;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.WorldData
{
    public abstract class WorldObject
    {
        public int ObjectId { get; set; }
        private ObjectPosition _position;
        public IServiceProvider ServiceProvider { get; }

        protected WorldObject(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        public abstract Task RequestActionAsync(PlayerInstance playerInstance);

        public virtual async Task RequestForcedAttack(PlayerInstance playerInstance)
        {
            await playerInstance.SendActionFailedPacketAsync();
        }
        
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
        
        /// <summary>
        /// Store World Object in collection
        /// </summary>
        private void StoreWorldObject()
        {
            ServiceProvider.GetRequiredService<WorldInit>().StoreWorldObject(this);
        }

        public virtual void SpawnMe(int x, int y, int z)
        {
            WorldObjectPosition().SetWorldPosition(x, y, z);
            WorldObjectPosition().SetWorldRegion(GetRegionByLocation());
            StoreWorldObject();
            AddVisibleObject();
        }

        private void AddVisibleObject()
        {
            WorldRegionData worldRegion = WorldObjectPosition().GetWorldRegion();
            worldRegion.AddVisibleObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private WorldRegionData GetRegionByLocation()
        {
            return ServiceProvider.GetRequiredService<WorldInit>().GetRegion(WorldObjectPosition().GetWorldPosition());
        }
    }
}