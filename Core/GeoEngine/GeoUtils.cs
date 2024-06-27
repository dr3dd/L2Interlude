using System;

namespace Core.GeoEngine;

public class GeoUtils
{
    
    public static int ComputeNswe(int lastX, int lastY, int x, int y)
    {
        if (x > lastX) // east
        {
            if (y > lastY)
            {
                return Cell.NSWE_SOUTH_EAST;
            }
            if (y < lastY)
            {
                return Cell.NSWE_NORTH_EAST;
            }
            return Cell.NSWE_EAST;
        }

        if (x < lastX) // west
        {
            if (y > lastY)
            {
                return Cell.NSWE_SOUTH_WEST;
            }
            if (y < lastY)
            {
                return Cell.NSWE_NORTH_WEST;
            }
            return Cell.NSWE_WEST;
        }

        // unchanged x
        if (y > lastY)
        {
            return Cell.NSWE_SOUTH;
        }
        if (y < lastY)
        {
            return Cell.NSWE_NORTH;
        }
        throw new Exception();
    }
}