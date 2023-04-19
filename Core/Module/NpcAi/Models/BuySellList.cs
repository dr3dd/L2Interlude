namespace Core.Module.NpcAi.Models;

public struct BuySellList
{
    public int ItemId { get; }
    public short Param1 { get; }
    public double Param2 { get; }
    public short Param3 { get; }

    public BuySellList(int itemId, short param1, double param2, short param3)
    {
        ItemId = itemId;
        Param1 = param1;
        Param2 = param2;
        Param3 = param3;
    }
}