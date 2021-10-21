using System;
using Core.Module.CharacterData;
using L2Logger;

namespace Core.Module.WorldData
{
    public class ObjectPosition
    {
        private readonly WorldObject _activeObject;
        private Location _worldPosition;

        private WorldRegionData _worldRegion; // Object localization : Used for items/chars that are seen in the world
        public int Heading { get; set; } 
        public ObjectPosition(WorldObject activeObject)
        {
            _activeObject = activeObject;
        }
        
        public int GetX()
        {
            return GetWorldPosition().GetX();
        }
        
        public int GetY()
        {
            return GetWorldPosition().GetY();
        }
        
        public int GetZ()
        {
            return GetWorldPosition().GetZ();
        }

        public void SetXYZ(int x, int y, int z)
        {
            SetWorldPosition(x, y, z);

            try
            {
                if (Initializer.WorldInit().GetRegion(GetWorldPosition()) != GetWorldRegion())
                {
                    UpdateWorldRegion();
                }
            }
            catch (Exception)
            {
                LoggerManager.Info("Object Id at bad coords: (x: " + GetWorldPosition().GetX() + ", y: " + GetWorldPosition().GetY() + ", z: " + GetWorldPosition().GetZ() + ").");
            }
        }

        private void UpdateWorldRegion()
        {
            WorldRegionData newRegionData = Initializer.WorldInit().GetRegion(GetWorldPosition());
            if (newRegionData != GetWorldRegion())
            {
                SetWorldRegion(newRegionData);
            }
        }

        public Location GetWorldPosition()
        {
            if (_worldPosition == null)
            {
                _worldPosition = new Location(0, 0, 0);
            }
            return _worldPosition;
        }
        
        public void SetWorldRegion(WorldRegionData value)
        {
            _worldRegion = value;
        }
        
        public void SetWorldPosition(int x, int y, int z)
        {
            WorldPosition().SetXYZ(x, y, z);
        }
        
        public WorldRegionData GetWorldRegion()
        {
            return _worldRegion;
        }
        
        public Location WorldPosition()
        {
            if (_worldPosition == null)
            {
                _worldPosition = new Location(0, 0, 0);
            }
            return _worldPosition;
        }
    }
}