using System;
using System.Collections.Generic;

namespace Core.Module.DoorData;

public class DoorTemplateInit
{
    private readonly DoorStat _stat;
    
    public DoorStat GetStat() => _stat;
    public DoorTemplateInit(IDictionary<string, object> setStats)
    {
        _stat = new DoorStat();
        _stat.Name = (string) setStats["door_name"];
        _stat.Type = (string) setStats["type"];
        _stat.Hp = ToInt(setStats["hp"]);
        if (setStats.ContainsKey("editor_id"))
        {
            _stat.EditorId = ToInt(setStats["editor_id"]);
        }
        if (setStats.ContainsKey("open_method"))
        {
            _stat.OpenMethod = (string) setStats["open_method"];
        }
        if (setStats.ContainsKey("height"))
        {
            _stat.Height = ToInt(setStats["height"]);
        }
        if (setStats.ContainsKey("physical_defence"))
        {
            _stat.PhysicalDefence = ToInt(setStats["physical_defence"]);
        }
        if (setStats.ContainsKey("magical_defence"))
        {
            _stat.MagicalDefence = ToInt(setStats["magical_defence"]);
        }
        if (setStats.ContainsKey("pos"))
        {
            _stat.Pos = (int[]) setStats["pos"];
        }
    }
    
    private int ToInt(object obj) => Convert.ToInt32(obj);
}
