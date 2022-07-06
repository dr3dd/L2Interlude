using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.AreaData
{
    internal class ZoneManager
    {
        private readonly List<BaseArea> _zones;
        
        public ZoneManager()
        {
            _zones = new List<BaseArea>();
        }
        
        public void RegisterNewZone(BaseArea zone)
        {
            _zones.Add(zone);
        }
        
        public void RevalidateZones(Character character)
        {
            _zones.ForEach(e =>
            {
                e?.RevalidateInZone(character);
            });
        }
        
        public void RemoveCharacter(Character character)
        {
            _zones.ForEach(e =>
            {
                e?.RemoveCharacter(character);
            });
        }
        
    }
}