using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket;

public class RequestBuyItem : PacketBase
{
    private int _merchantId;
    private int _buyItemsCount;
    private readonly IList<MyItem> _myItems;
    private readonly PlayerInstance _playerInstance;

    private class MyItem
    {
        public int ItemId { get; }
        public int Qty { get; }

        public MyItem(int itemId, int qty)
        {
            ItemId = itemId;
            Qty = qty;
        }
    }
    
    public RequestBuyItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
    {
        _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        _merchantId = packet.ReadInt();
        _buyItemsCount = packet.ReadInt();
        _myItems = new List<MyItem>();
        for (var i = 0; i < _buyItemsCount; i++)
        {
            var itemId = packet.ReadInt();
            var count = packet.ReadInt();
            _myItems.Add(new MyItem(itemId, count));
        }
    }

    public override async Task Execute()
    {
        foreach (var myItem in _myItems)
        {
            await _playerInstance.PlayerInventory().AddUpdateItemToInventory(myItem.ItemId, myItem.Qty);
        }

        var su = new StatusUpdate(_playerInstance.ObjectId);
        //su.AddAttribute(StatusUpdate.CurLoad, _playerInstance.GetCurrentLoad());
        await _playerInstance.SendPacketAsync(su);
        await _playerInstance.SendPacketAsync(new ItemList(_playerInstance));
    }
}