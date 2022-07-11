using Core.Module.AreaData;
using Core.Module.Player;
using Core.Module.WorldData;
using Helpers;

namespace Core.Module.CharacterData
{
    public class CharacterZone
    {
        private readonly Character _character;
        
        private readonly byte[] _areas;
        public CharacterZone(Character character)
        {
            _character = character;
            _areas = new byte[AreaId.GetAreaCount()];
            
        }

        public bool IsInsideZone(AreaId area)
        {
            return _areas[(int)area.Id] > 0;
        }
        
        public bool IsInsideRadius2D(WorldObject worldObject, int radius)
        {
            return IsInsideRadius2D(worldObject.GetX(), worldObject.GetY(), radius);
        }
        
        public bool IsInsideRadius2D(int x, int y, int radius)
        {
            return CalculateRange.CalculateDistanceSq2D(x, y, _character.GetX(), _character.GetY()) < (radius * radius);
        }

        public void RevalidateZone()
        {
            _character.GetWorldRegion().RevalidateZones(_character);
        }

        public bool IsInsideRadius(WorldObject worldObject, int radius, bool checkZ, bool strictCheck)
        {
            if (worldObject == null)
            {
                return false;
            }
            return IsInsideRadius(worldObject.GetX(), worldObject.GetY(), worldObject.GetZ(), radius, checkZ, strictCheck);
        }
        
        private bool IsInsideRadius(int x, int y, int z, int radius, bool checkZ, bool strictCheck)
        {
            double dx = x - _character.GetX();
            double dy = y - _character.GetY();
            double dz = z - _character.GetZ();
            if (strictCheck)
            {
                if (checkZ)
                {
                    return ((dx * dx) + (dy * dy) + (dz * dz)) < (radius * radius);
                }
                return ((dx * dx) + (dy * dy)) < (radius * radius);
            }
            if (checkZ)
            {
                return ((dx * dx) + (dy * dy) + (dz * dz)) <= (radius * radius);
            }
            return ((dx * dx) + (dy * dy)) <= (radius * radius);
        }
    }
}