using System.Linq;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player
{
    public sealed class PlayerUseItem
    {
        private readonly PlayerInstance _playerInstance;
        
        public PlayerUseItem(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public async Task UseItemAsync(ItemInstance itemInstance)
        {
            if (itemInstance.IsEquippable())
            {
                await UseEquippedItemAsync(itemInstance);
                return;
            }
        }

        private async Task UseEquippedItemAsync(ItemInstance itemInstance)
        {
            await EquipUnEquipItemAsync(itemInstance);
            await SendStatusUpdateAsync();
        }
        
        private async Task SendStatusUpdateAsync()
        {
            await _playerInstance.SendPacketAsync(new UserInfo(_playerInstance));
            await _playerInstance.SendPacketAsync(new ItemList(_playerInstance));
            await _playerInstance.SendToKnownPlayers(new UserInfo(_playerInstance));
        }
        
        private async Task EquipUnEquipItemAsync(ItemInstance itemInstance)
        {
            var characterInfo = _playerInstance.PlayerCharacterInfo();
            if (itemInstance.IsEquipped(characterInfo))
            {
                await SendMessageUnEquipAsync(itemInstance);
                SlotBitType bodyPart = _playerInstance.PlayerInventory().GetSlotBitByItem(itemInstance);
                await _playerInstance.PlayerInventory().UnEquipItemInBodySlot((int) bodyPart);
                return;
            }
            _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
            await _playerInstance.PlayerModel().UpdateCharacter();
            await SendMessageEquipAsync(itemInstance);
        }
        
        private async Task SendMessageUnEquipAsync(ItemInstance itemInstance)
        {
            var sm = new SystemMessage(SystemMessageId.S1Disarmed);
            sm.AddItemName(itemInstance.ItemId);
            await _playerInstance.SendPacketAsync(sm);
        }
        
        private async Task SendMessageEquipAsync(ItemInstance itemInstance)
        {
            var sm = new SystemMessage(SystemMessageId.S1Equipped);
            sm.AddItemName(itemInstance.ItemId);
            await _playerInstance.SendPacketAsync(sm);
        }
    }
}