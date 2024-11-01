using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class ItemList : Network.ServerPacket
    {
        private readonly List<ItemInstance> _items;
        private readonly PlayerCharacterInfo _characterInfo;
        private readonly bool _showWindow;
        public ItemList(PlayerInstance player, bool showWindow = true)
        {
            _items = player.PlayerInventory().GetInventoryItems();
            _characterInfo = player.PlayerCharacterInfo();
            _showWindow = showWindow;
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x1b);
            await WriteShortAsync(_showWindow); //?? show window
            await WriteShortAsync(_items.Count);
            _items.ForEach(item =>
            {
                WriteShortAsync(0); //?? Type1
                WriteIntAsync(item.ObjectId);
                WriteIntAsync(item.ItemId);
                WriteIntAsync(item.Amount);
                WriteShortAsync((int)item.GetItemType()); //(int)temp.Item.Type2
                WriteShortAsync(0); //CustomType1
                WriteShortAsync(item.IsEquipped(_characterInfo) ? 0x01 : 0x00); //IsEquipped() ? 0x01 : 0x00
                WriteIntAsync((int) item.GetSlotBitType());
                WriteShortAsync(0); //item.EnchantLevel
                // race tickets
                WriteShortAsync(0); //CustomType2
                WriteIntAsync(0);//WriteIntAsync((isAugmented()) ? getAugmentation().getAugmentationId() : 0x00);
                WriteIntAsync(0); //Mana
            });
        }
    }
}