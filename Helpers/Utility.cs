using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

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
        
        public static float ToFloat(object obj) => Convert.ToSingle(obj, CultureInfo.InvariantCulture.NumberFormat);
        public static double ToDouble(object obj) => Convert.ToDouble(obj, CultureInfo.InvariantCulture.NumberFormat);
        public static short ToShort(object obj) => Convert.ToInt16(obj);
        public static int ToInt(object obj) => Convert.ToInt32(obj);
        public static byte ToByte(object obj) => Convert.ToByte(obj);
        
        public static double ToRadians(this double val) => (Math.PI / 180) * val;
        
        public static int Limit(int numToTest, int min, int max)
        {
            return (numToTest > max) ? max : ((numToTest < min) ? min : numToTest);
        }

        public static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
        public static bool EndsWithIgnoreCase(this string str, string stringToCompare)
        {
            return str.EndsWith(stringToCompare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}