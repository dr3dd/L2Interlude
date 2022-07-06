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
    }
}