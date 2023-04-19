using System.Collections.Generic;
using System.Linq;
using Core.Module.ItemData;

namespace Core.NetworkPacket.ServerPacket;

public class SellList : Network.ServerPacket
{
    private readonly int _currentMoney;
    private readonly int _npcId;
    private readonly List<ItemDataAbstract> _sellLists;
    public SellList(List<ItemDataAbstract> sellLists, int npcId, int currentMoney)
    {
        _currentMoney = currentMoney;
        _npcId = npcId;
        _sellLists = sellLists;
    }
    
    public override void Write()
    {
        WriteByte(0x11);
        WriteInt(_currentMoney);
        WriteInt(_npcId); //Trader Id
        WriteShort(_sellLists.Count);
        foreach (var itemData in _sellLists.Where(itemData => itemData.MaximumCount > 0))
        {
            WriteShort(0); //type1
            WriteInt(0); //ObjectId
            WriteInt(itemData.ItemId);
            WriteInt(itemData.MaximumCount);
            WriteShort((short)itemData.ItemType); //type2
            WriteShort(0); //??
            WriteInt(0); //??
            WriteShort(0); //enchant level
            WriteShort(0); //?
            WriteShort(0); //?
            WriteInt(itemData.DefaultPrice);
        }
    }
}