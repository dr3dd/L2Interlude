using System;

namespace Core.Module.AreaData
{
    public class ZoneNPoly : ZoneForm
    {
        private readonly int[] _x;
        private readonly int[] _y;
        private readonly int _z1;
        private readonly int _z2;
        
        public ZoneNPoly(int[] x, int[] y, int z1, int z2)
        {
            _x = x;
            _y = y;
            _z1 = z1;
            _z2 = z2;
        }
        public override bool IsInsideZone(int x, int y, int z)
        {
            if ((z < _z1) || (z > _z2))
            {
                return false;
            }
		
            bool inside = false;
            for (int i = 0, j = _x.Length - 1; i < _x.Length; j = i++)
            {
                if ((((_y[i] <= y) && (y < _y[j])) || ((_y[j] <= y) && (y < _y[i]))) && (x < ((((_x[j] - _x[i]) * (y - _y[i])) / (_y[j] - _y[i])) + _x[i])))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        public override bool IntersectsRectangle(int ax1, int ax2, int ay1, int ay2)
        {
            int tX;
            int tY;
            int uX;
            int uY;
		
            // First check if a point of the polygon lies inside the rectangle
            if ((_x[0] > ax1) && (_x[0] < ax2) && (_y[0] > ay1) && (_y[0] < ay2))
            {
                return true;
            }
		
            // Or a point of the rectangle inside the polygon
            if (IsInsideZone(ax1, ay1, (_z2 - 1)))
            {
                return true;
            }
		
            // If the first point wasn't inside the rectangle it might still have any line crossing any side of the rectangle
            // Check every possible line of the polygon for a collision with any of the rectangles side
            for (int i = 0; i < _y.Length; i++)
            {
                tX = _x[i];
                tY = _y[i];
                uX = _x[(i + 1) % _x.Length];
                uY = _y[(i + 1) % _x.Length];
			
                // Check if this line intersects any of the four sites of the rectangle
                if (LineSegmentsIntersect(tX, tY, uX, uY, ax1, ay1, ax1, ay2))
                {
                    return true;
                }
                if (LineSegmentsIntersect(tX, tY, uX, uY, ax1, ay1, ax2, ay1))
                {
                    return true;
                }
                if (LineSegmentsIntersect(tX, tY, uX, uY, ax2, ay2, ax1, ay2))
                {
                    return true;
                }
                if (LineSegmentsIntersect(tX, tY, uX, uY, ax2, ay2, ax2, ay1))
                {
                    return true;
                }
            }
            return false;
        }

        public override double GetDistanceToZone(int x, int y)
        {
            double test;
            double shortestDist = Math.Pow(_x[0] - x, 2) + Math.Pow(_y[0] - y, 2);
            for (int i = 1; i < _y.Length; i++)
            {
                test = Math.Pow(_x[i] - x, 2) + Math.Pow(_y[i] - y, 2);
                if (test < shortestDist)
                {
                    shortestDist = test;
                }
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