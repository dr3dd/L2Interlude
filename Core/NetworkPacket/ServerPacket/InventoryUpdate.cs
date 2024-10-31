using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x27);
            await WriteShortAsync(_items.Count);

            foreach (var item in _items)
            {
                await WriteShortAsync(item.Change);
                await WriteShortAsync(0); //(int) item.Item.Type1
                await WriteIntAsync(item.ObjectId);
                await WriteIntAsync(item.ItemId);
                await WriteIntAsync(item.Amount);
                await WriteShortAsync((int)item.GetItemType());
                await WriteShortAsync(0); //item.CustomType1
                await WriteShortAsync(item.IsEquipped(_characterInfo) ? 0x01 : 0x00);
                await WriteIntAsync((int) item.GetSlotBitType());
                await WriteShortAsync(0); //item.Enchant
                await WriteShortAsync(0); //item.CustomType2
                await WriteIntAsync(0); //item.AugmentationBonus
                await WriteIntAsync(0); //item.Mana
            }
        }
    }
}