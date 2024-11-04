using System;

namespace Core.Module.NpcAi;

public static class Gg
{
    /// <summary>
    /// gg.GetDateTime(0, 0) return current Year 
    /// gg.GetDateTime(0, 1) return current Month 
    /// gg.GetDateTime(0, 2) return current day of Month
    /// gg.GetDateTime(0, 3) return current Hour 
    /// gg.GetDateTime(0, 4) return current Minute 
    /// gg.GetDateTime(0, 5) return current Second
    /// gg.GetDateTime(0, 6) return day of week (1 for Monday, 5 for Friday)
    /// </summary>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int GetDateTime(int param1, int param2)
    {
        var now = DateTime.Now;

        return param2 switch
        {
            0 => now.Year,            // return Year
            1 => now.Month,           // return Month
            2 => now.Day,             // return day of Month
            3 => now.Hour,            // return Hour
            4 => now.Minute,          // return Minute
            5 => now.Second,          // return Second
            6 => GetDayOfWeek(now),   // return day of week (1 for Monday, 5 for Friday)
            _ => throw new ArgumentOutOfRangeException(nameof(param2), "Wrong Parameter")
        };
    }
    
    /// <summary>
    /// Returns the current time in milliseconds
    /// </summary>
    /// <returns></returns>
    public static int GetTimeOfDay()
    {
        var now = DateTime.Now;
        return (int)now.TimeOfDay.TotalMilliseconds;
    }

    /// <summary>
    /// Returns the in-game time of day. If Tag == 0, the function returns hours, if Tag == 1, minutes
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static int GetL2Time(int tag)
    {
        if (tag == 0)
        {
            return (Initializer.TimeController().GetGameTime() / 60) % 24;
        }
        return Initializer.TimeController().GetGameTime() % 60;
    }
    
    /// <summary>
    /// Return day of week
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private static int GetDayOfWeek(DateTime date)
    {
        var dayOfWeek = (int)date.DayOfWeek;
        return dayOfWeek == 0 ? 7 : dayOfWeek;
    }

    internal static int Rand(int v)
    {
        return new Random().Next(v);
    }
}