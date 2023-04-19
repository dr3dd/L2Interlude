using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.NpcAi.Models;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData;

public class NpcAiSell
{
    private readonly NpcInstance _npcInstance;
    private readonly ItemDataInit _itemDataInit;
    public NpcAiSell(NpcAi npcAi)
    {
        _npcInstance = npcAi.NpcInstance();
        _itemDataInit = _npcInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
    }

    public async Task ShowSellDialog(PlayerInstance playerInstance, IEnumerable<BuySellList> sellList)
    {
        var items = sellList.Select(buySellList => _itemDataInit.GetItemById(buySellList.ItemId)).ToList();
        await playerInstance.SendPacketAsync(new SellList(items, _npcInstance.NpcId, 0));
    }
}