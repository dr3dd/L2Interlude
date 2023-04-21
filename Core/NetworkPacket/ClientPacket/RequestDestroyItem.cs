using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket;

public class RequestDestroyItem : PacketBase
{
    private readonly PlayerInstance _playerInstance;
    private readonly PlayerInventory _playerInventory;
    private readonly int _objectId;
    private readonly int _count;
    
    public RequestDestroyItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
    {
        _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        _playerInventory = _playerInstance.PlayerInventory();
        _objectId = packet.ReadInt();
        _count = packet.ReadInt();
    }

    public override async Task Execute()
    {
        var itemInstance = _playerInventory.GetInventoryItemByObjectId(_objectId);

        await _playerInventory.AddOrUpdate().DestroyItemInInventory(_objectId, _count);
        var su = new StatusUpdate(_playerInstance.ObjectId);
        //su.AddAttribute(StatusUpdate.CurLoad, _playerInstance.GetCurrentLoad());
        await _playerInstance.SendPacketAsync(su);
    }
}