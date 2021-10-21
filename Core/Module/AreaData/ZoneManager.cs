using System.Collections.Generic;
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
        
        public void RevalidateZones(PlayerInstance character)
        {
            _zones.ForEach(e =>
            {
                e?.RevalidateInZone(character);
            });
        }
    }
}