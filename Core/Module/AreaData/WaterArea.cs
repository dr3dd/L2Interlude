using System.Collections.Generic;

namespace Core.Module.AreaData
{
    public class WaterArea : BaseArea
    {
        public double MinX { get; }
        public double MinY { get; }
        public double MinZ { get; }
        
        public double MaxX { get; }
        public double MaxY { get; }
        public double MaxZ { get; }
        
        public WaterArea(string name, byte mapNoX, byte mapNoY, IDictionary<string, double> waterRange) : base(name, mapNoX, mapNoY, typeof(WaterArea))
        {
            MinX = waterRange["MinX"];
            MinY = waterRange["MinY"];
            MinZ = waterRange["MinZ"];
            
            MaxX = waterRange["MaxX"];
            MaxY = waterRange["MaxY"];
            MaxZ = waterRange["MaxZ"];
        }
    }
}