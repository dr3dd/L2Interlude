using System;
using System.Collections.Generic;

namespace Core.Module.AreaData
{
    public class AreaId
    {
        public AreaIds Id { get; }

        public AreaId(AreaIds areaIds)
        {
            Id = areaIds;
        }
        
        public static int GetAreaCount()
        {
            return Enum.GetNames(typeof(AreaIds)).Length;
        }
        
        public static IEnumerable<AreaId> Values
        {
            get
            {
                yield return Water;
                yield return MotherTree;
                yield return PeaceZone;
            }
        }
        
        public static readonly AreaId Water = new AreaId(AreaIds.Water);
        public static readonly AreaId MotherTree = new AreaId(AreaIds.MotherTree);
        public static readonly AreaId PeaceZone = new AreaId(AreaIds.PeaceZone);
    }
}