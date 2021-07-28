using System;

namespace Helpers
{
    public static class Utility
    {
        public static double LengthSq(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2);
        }

        public static double Hypot(double x, double y)
        {
            return Math.Sqrt(LengthSq(x, y));
        }
        
        public static double ConvertHeadingToDegree(int heading)
        {
            if (heading == 0)
            {
                return 360D;
            }
            return (9.0D * heading) / 1610.0D; // = 360.0 * (heading / 64400.0)
        }
        
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
        
        public static int Limit(int numToTest, int min, int max)
        {
            return (numToTest > max) ? max : ((numToTest < min) ? min : numToTest);
        }
    }
}