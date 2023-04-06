namespace Core.Module.DoorData;

public class DoorStat
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int EditorId { get; set; }
    public string OpenMethod { get; set; }
    public int Height { get; set; }
    public int Hp { get; set; }
    public int PhysicalDefence { get; set; }
    public int MagicalDefence { get; set; }
    public int[] Pos { get; set; }
}