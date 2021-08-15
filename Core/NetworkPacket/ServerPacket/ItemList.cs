using System.Collections.Generic;
using Core.Module.ItemData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class ItemList : Network.ServerPacket
    {
        private readonly List<ItemInstance> _items;
        private readonly PlayerCharacterInfo _characterInfo;
        public ItemList(PlayerInstance player)
        {
            _items = player.PlayerInventory().GetInventoryItems();
            _characterInfo = player.PlayerCharacterInfo();
        }
        
        public override void Write()
        {
            WriteByte(0x1b);
            WriteShort(0x01); //?? show window
            WriteShort(_items.Count);
            _items.ForEach(item =>
            {
                WriteShort(0); //?? Type1
                WriteInt(item.UserItemId);
                WriteInt(item.ItemId);
                WriteInt(item.Amount);
                WriteShort((int)item.GetItemType()); //(int)temp.Item.Type2
                WriteShort(0); //CustomType1
                WriteShort(item.IsEquipped(_characterInfo) ? 0x01 : 0x00); //IsEquipped() ? 0x01 : 0x00
                WriteInt((int) item.GetSlotBitType());
                WriteShort(0); //item.EnchantLevel
                // race tickets
                WriteShort(0); //CustomType2
                WriteInt(0);//WriteInt((isAugmented()) ? getAugmentation().getAugmentationId() : 0x00);
                WriteInt(0); //Mana
            });
        }
    }
}