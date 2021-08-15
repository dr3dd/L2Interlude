using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestUnEquipItem : PacketBase
    {
        private readonly int _slot;
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerInventory _playerInventory;
        
        public RequestUnEquipItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) :
            base(serviceProvider)
        {
            _slot = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerInventory = _playerInstance.PlayerInventory();
        }

        public override async Task Execute()
        {
            //todo there should be added logic for validation
            //todo Prevent player from unequipping items in special conditions like Stun, Confuse, etc..
            
            await _playerInventory.UnEquipItemInBodySlot(_slot);
            var unEquippedItem = _playerInventory.GetUnEquippedBodyPartItem(_slot);
            // show the update in the inventory
            await SendInventoryUpdateAsync(unEquippedItem);
            await SendMessageAsync(unEquippedItem);
            await _playerInstance.SendUserInfoAsync();
        }

        private async Task SendMessageAsync(ItemInstance unEquippedItem)
        {
            var sm = new SystemMessage(SystemMessageId.S1Disarmed);
            sm.AddItemName(unEquippedItem.ItemId);
            await _playerInstance.SendPacketAsync(sm);
        }

        private async Task SendInventoryUpdateAsync(ItemInstance unEquippedItem)
        {
            InventoryUpdate iu = new InventoryUpdate(_playerInstance);
            iu.AddItem(unEquippedItem);
            await _playerInstance.SendPacketAsync(iu);
        }
    }
}