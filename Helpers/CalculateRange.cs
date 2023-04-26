using System;

namespace Helpers
{
    public static class CalculateRange
    {
        public static double CalculateDistance(int x1, int y1, int z1, int x2, int y2)
        {
            return CalculateDistance(x1, y1, 0, x2, y2, 0, false);
        }
        
        public static double CalculateDistance(int x1, int y1, int z1, int x2, int y2, int z2, bool includeZAxis)
        {
            double dx = (double) x1 - x2;
            double dy = (double) y1 - y2;
            if (includeZAxis)
            {
                double dz = z1 - z2;
                return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
            }
            return Math.Sqrt((dx * dx) + (dy * dy));
        }
        
        public static double ConvertHeadingToDegree(int heading)
        {
            if (heading == 0)
            {
                return 360D;
            }
            return (9.0D * heading) / 1610.0D; // = 360.0 * (heading / 64400.0)
        }
        
        public static int CalculateHeadingFrom(int obj1X, int obj1Y, int obj2X, int obj2Y)
        {
            return (int) ((Math.Atan2(obj1Y - obj2Y, obj1X - obj2X) * 10430.379999999999D) + 32768.0D);
        }
        
        public static int CalculateHeadingFrom(double dx, double dy)
        {
            double angleTarget = Math.Atan2(dy, dx).ToRadians();
            if (angleTarget < 0.0D)
            {
                angleTarget = 360.0D + angleTarget;
            }
            return (int) (angleTarget * 182.04444444399999D);
        }
        
        public static double CalculateDistanceSq2D(int x1, int y1, int x2, int y2)
        {
            return Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2);
        }
        
        /// <summary>
        /// includeZAxis = true only if GeoData works
        /// </summary>
        /// <param name="range"></param>
        /// <param name="origX"></param>
        /// <param name="origY"></param>
        /// <param name="origZ"></param>
        /// <param name="origCollisionRadius"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <param name="targetZ"></param>
        /// <param name="targetCollisionRadius"></param>
        /// <param name="includeZAxis"></param>
        /// <returns></returns>
        public static bool CheckIfInRange(int range, int origX, int origY, int origZ, float origCollisionRadius, int targetX,
            int targetY, int targetZ, float targetCollisionRadius, bool includeZAxis)
        {
            if (range == -1)
            {
                return true; // not limited
            }
	
            float rad = 0;
            rad += origCollisionRadius;
            rad += targetCollisionRadius;
		
            double dx = origX - targetX;
            double dy = origY - targetY;
            double d;
            if (includeZAxis)
            {
                double dz = origZ - targetZ;
                d = (dx * dx) + (dy * dy) + (dz * dz);
                return d <= ((range * range) + (2 * range * rad) + (rad * rad));
            }
            d = (dx * dx) + (dy * dy);
            return d <= ((range * range) + (2 * range * rad) + (rad * rad));
        }
    }
}