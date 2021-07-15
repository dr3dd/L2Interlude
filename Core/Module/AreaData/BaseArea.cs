using System;

namespace Core.Module.AreaData
{
    public abstract class BaseArea
    {
        protected string Name { get; }
        protected Type Area { get; }
        protected byte MapNoX { get; }
        protected byte MapNoY { get; }

        protected BaseArea(string name, byte mapNoX, byte mapNoY, Type area)
        {
            Name = name;
            MapNoX = mapNoX;
            MapNoY = mapNoY;
            Area = area;
        }
    }
}