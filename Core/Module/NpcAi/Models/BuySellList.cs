namespace Core.Module.NpcAi.Models;

public struct BuySellList
{
    public int ItemId { get; }
    public string ItemName { get; }
    public short Param1 { get; }
    public double Param2 { get; }
    public short Param3 { get; }

    public BuySellList(string itemName, short param1, double param2, short param3)
    {
        ItemName = itemName;
        ItemId = Initializer.ItemDataInit().GetItemByName(itemName).ItemId;
        Param1 = param1;
        Param2 = param2;
        Param3 = param3;
    }

    public BuySellList(int itemId, short param1, double param2, short param3)
    {
        ItemName = Initializer.ItemDataInit().GetItemById(itemId).Name;
        ItemId = itemId;
        Param1 = param1;
        Param2 = param2;
        Param3 = param3;
    }
}