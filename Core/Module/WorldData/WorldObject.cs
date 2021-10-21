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
            if (_position == null)
            {
                _position = new ObjectPosition(this);
            }
            return _position;
        }

        public void SpawnMe(int x, int y, int z)
        {
            WorldObjectPosition().SetWorldPosition(x, y, z);
            var worldRegionData = Initializer.WorldInit().GetRegion(WorldObjectPosition().GetWorldPosition());
            WorldObjectPosition().SetWorldRegion(worldRegionData);
        }
        
    }
}