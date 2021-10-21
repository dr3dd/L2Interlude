using System;

namespace Core.Module.AreaData
{
    public class ZoneCuboid : ZoneForm
    {
        private readonly int _x1;
        private readonly int _x2;
        private readonly int _y1;
        private readonly int _y2;
        private readonly int _z1;
        private readonly int _z2;

        public ZoneCuboid(int x1, int x2, int y1, int y2, int z1, int z2)
        {
            _x1 = x1;
            _x2 = x2;
            if (_x1 > _x2) // switch them if alignment is wrong
            {
                _x1 = x2;
                _x2 = x1;
            }
		
            _y1 = y1;
            _y2 = y2;
            if (_y1 > _y2) // switch them if alignment is wrong
            {
                _y1 = y2;
                _y2 = y1;
            }
		
            _z1 = z1;
            _z2 = z2;
            if (_z1 > _z2) // switch them if alignment is wrong
            {
                _z1 = z2;
                _z2 = z1;
            }
        }
        public override bool IsInsideZone(int x, int y, int z)
        {
            return (x >= _x1) && (x <= _x2) && (y >= _y1) && (y <= _y2) && (z >= _z1) && (z <= _z2);
        }

        public override bool IntersectsRectangle(int ax1, int ax2, int ay1, int ay2)
        {
            // Check if any point inside this rectangle
            if (IsInsideZone(ax1, ay1, (_z2 - 1)))
            {
                return true;
            }
		
            if (IsInsideZone(ax1, ay2, (_z2 - 1)))
            {
                return true;
            }
		
            if (IsInsideZone(ax2, ay1, (_z2 - 1)))
            {
                return true;
            }
		
            if (IsInsideZone(ax2, ay2, (_z2 - 1)))
            {
                return true;
            }
		
            // Check if any point from this rectangle is inside the other one
            if ((_x1 > ax1) && (_x1 < ax2) && (_y1 > ay1) && (_y1 < ay2))
            {
                return true;
            }
		
            if ((_x1 > ax1) && (_x1 < ax2) && (_y2 > ay1) && (_y2 < ay2))
            {
                return true;
            }
		
            if ((_x2 > ax1) && (_x2 < ax2) && (_y1 > ay1) && (_y1 < ay2))
            {
                return true;
            }
		
            if ((_x2 > ax1) && (_x2 < ax2) && (_y2 > ay1) && (_y2 < ay2))
            {
                return true;
            }
		
            // Horizontal lines may intersect vertical lines
            if (LineIntersectsLine(_x1, _y1, _x2, _y1, ax1, ay1, ax1, ay2))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x1, _y1, _x2, _y1, ax2, ay1, ax2, ay2))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x1, _y2, _x2, _y2, ax1, ay1, ax1, ay2))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x1, _y2, _x2, _y2, ax2, ay1, ax2, ay2))
            {
                return true;
            }
		
            // Vertical lines may intersect horizontal lines
            if (LineIntersectsLine(_x1, _y1, _x1, _y2, ax1, ay1, ax2, ay1))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x1, _y1, _x1, _y2, ax1, ay2, ax2, ay2))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x2, _y1, _x2, _y2, ax1, ay1, ax2, ay1))
            {
                return true;
            }
		
            if (LineIntersectsLine(_x2, _y1, _x2, _y2, ax1, ay2, ax2, ay2))
            {
                return true;
            }
		
            return false;
        }

        public override double GetDistanceToZone(int x, int y)
        {
            if (IsInsideZone(x, y, _z1))
            {
                return 0; // If you are inside the zone distance to zone is 0.
            }

            double shortestDist = Math.Pow(_x1 - x, 2) + Math.Pow(_y1 - y, 2);
            var test = Math.Pow(_x1 - x, 2) + Math.Pow(_y2 - y, 2);
            if (test < shortestDist)
            {
                shortestDist = test;
            }
		
            test = Math.Pow(_x2 - x, 2) + Math.Pow(_y1 - y, 2);
            if (test < shortestDist)
            {
                shortestDist = test;
            }
		
            test = Math.Pow(_x2 - x, 2) + Math.Pow(_y2 - y, 2);
            if (test < shortestDist)
            {
                shortestDist = test;
            }
		
            return Math.Sqrt(shortestDist);
        }

        public override int GetLowZ()
        {
            return _z1;
        }

        public override int GetHighZ()
        {
            return _z2;
        }
    }
}