using System.Collections.Generic;
using Core.Module.ItemData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class InventoryUpdate : Network.ServerPacket
    {
        private readonly IList<ItemInstance> _items;
        private readonly PlayerCharacterInfo _characterInfo;
        
        public InventoryUpdate(PlayerInstance playerInstance)
        {
            _characterInfo = playerInstance.PlayerCharacterInfo();
            _items = new List<ItemInstance>();
        }
        
        public void AddItem(ItemInstance item)
        {
            item.Change = ItemInstance.Added;
            _items.Add(item);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddRemovedItem(ItemInstance item)
        {
            item.Change = ItemInstance.Removed;
            _items.Add(item);
        }
        
        public void AddModifiedItem(ItemInstance item)
        {
            item.Change = ItemInstance.Modified;
            _items.Add(item);
        }
        
        public override void Write()
        {
            WriteByte(0x27);
            WriteShort(_items.Count);

            foreach (var item in _items)
            {
                WriteShort(item.Change);
                WriteShort(0); //(int) item.Item.Type1
                WriteInt(item.ObjectId);
                WriteInt(item.ItemId);
                WriteInt(item.Amount);
                WriteShort((int)item.GetItemType());
                WriteShort(0); //item.CustomType1
                WriteShort(item.IsEquipped(_characterInfo) ? 0x01 : 0x00);
                WriteInt((int) item.GetSlotBitType());
                WriteShort(0); //item.Enchant
                WriteShort(0); //item.CustomType2
                WriteInt(0); //item.AugmentationBonus
                WriteInt(0); //item.Mana
            }
        }
    }
}