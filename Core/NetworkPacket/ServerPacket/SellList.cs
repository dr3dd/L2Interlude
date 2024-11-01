using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    public override async Task WriteAsync()
    {
        await WriteByteAsync(0x11);
        await WriteIntAsync(_currentMoney);
        await WriteIntAsync(_npcId); //Trader Id
        await WriteShortAsync(_sellLists.Count);
        foreach (var itemData in _sellLists.Where(itemData => itemData.MaximumCount > 0))
        {
            await WriteShortAsync(0); //type1
            await WriteIntAsync(0); //ObjectId
            await WriteIntAsync(itemData.ItemId);
            await WriteIntAsync(itemData.MaximumCount);
            await WriteShortAsync((short)itemData.ItemType); //type2
            await WriteShortAsync(0); //??
            await WriteIntAsync(0); //??
            await WriteShortAsync(0); //enchant level
            await WriteShortAsync(0); //?
            await WriteShortAsync(0); //?
            await WriteIntAsync(itemData.DefaultPrice);
        }
    }
}