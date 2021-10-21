using Helpers;

namespace Core.Module.AreaData
{
    public abstract class ZoneForm
    {
        protected const int Step = 50;

        public abstract bool IsInsideZone(int x, int y, int z);

        public abstract bool IntersectsRectangle(int ax1, int ax2, int ay1, int ay2);

        public abstract double GetDistanceToZone(int x, int y);

        public abstract int GetLowZ();

        public abstract int GetHighZ();

        protected bool LineSegmentsIntersect(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
        {
            return Line2D.LinesIntersect(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2);
        }

        protected bool LineIntersectsLine(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
        {
            int s1 = SameSide(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2);
            int s2 = SameSide(bx1, by1, bx2, by2, ax1, ay1, ax2, ay1);
            return (s1 <= 0) && (s2 <= 0);
        }
        
        protected bool IsBetween(double a, double b, double c)
        {
            return b > a ? (c >= a) && (c <= b) : (c >= b) && (c <= a);
        }
        
        protected int SameSide(double x0, double y0, double x1, double y1, double px0, double py0, double px1, double py1)
        {
            int sameSide = 0;
		
            double dx = x1 - x0;
            double dy = y1 - y0;
            double dx1 = px0 - x0;
            double dy1 = py0 - y0;
            double dx2 = px1 - x1;
            double dy2 = py1 - y1;
		
            // Cross product of the vector from the endpoint of the line to the point
            double c1 = (dx * dy1) - (dy * dx1);
            double c2 = (dx * dy2) - (dy * dx2);
            if ((c1 != 0) && (c2 != 0))
            {
                sameSide = (c1 < 0) != (c2 < 0) ? -1 : 1;
            }
            else if ((dx == 0) && (dx1 == 0) && (dx2 == 0))
            {
                sameSide = !IsBetween(y0, y1, py0) && !IsBetween(y0, y1, py1) ? 1 : 0;
            }
            else if ((dy == 0) && (dy1 == 0) && (dy2 == 0))
            {
                sameSide = !IsBetween(x0, x1, px0) && !IsBetween(x0, x1, px1) ? 1 : 0;
            }
            return sameSide;
        }
    }
}