using System.Collections.Generic;
using Core.GeoEngine.Pathfinding;

namespace Core.Module.CharacterData
{
    public sealed class MoveData
    {
        // when we retrieve x/y/z we use GameTimeControl.getGameTicks()
        // if we are moving, but move timestamp==gameticks, we don't need to recalculate position
        public int MoveStartTime { get; set; }
        public int MoveTimestamp { get; set; }
        public int XDestination { get; set; }
        public int YDestination { get; set; }
        public int ZDestination { get; set; }
        public double XAccurate { get; set; }
        public double YAccurate { get; set; }
        public double ZAccurate { get; set; }
        public int Heading { get; set; }
        public bool DisregardingGeodata { get; set; }
        public int OnGeodataPathIndex { get; set; }
        public LinkedList<AbstractNodeLoc> GeoPath { get; set; }
        public int GeoPathAccurateTx { get; set; }
        public int GeoPathAccurateTy { get; set; }
        public int GeoPathGtx { get; set; }
        public int GeoPathGty { get; set; }
        public int LastBroadcastTime { get; set; }
    }
}