using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.WorldData;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerInventory
    {
        private readonly PlayerInstance _playerInstance;
        private readonly IUserItemRepository _itemRepository;
        private readonly ObjectIdInit _objectIdInit;
        private readonly ItemDataInit _itemDataInit;
        private readonly IDictionary<int, ItemInstance> _items;
        
        public PlayerInventory(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _itemRepository = playerInstance.ServiceProvider.GetRequiredService<IUnitOfWork>().UserItems;
            _objectIdInit = playerInstance.ServiceProvider.GetRequiredService<ObjectIdInit>();
            _itemDataInit = playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            _items = new Dictionary<int, ItemInstance>();
        }

        public async Task RestoreInventory()
        {
            try
            {
                int charId = _playerInstance.PlayerCharacterInfo().CharacterId;
                var items = await _itemRepository.GetInventoryItemsByOwnerId(charId);
                items.ForEach(item =>
                {
                    var itemData = _itemDataInit.GetItemById(item.ItemId);
                    int objectId = _objectIdInit.NextObjectId();
                    ItemInstance itemInstance = new ItemInstance(objectId)
                    {
                        UserItemId = item.UserItemId,
                        ItemId = itemData.ItemId,
                        ItemData = itemData,
                        Amount = item.Amount
                    };
                    _items.Add(item.UserItemId, itemInstance);
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public int GetItem(int userItemId)
        {
            return _items.ContainsKey(userItemId)? _items[userItemId].ItemId : 0;
        }
        
        public ItemInstance GetItemInstance(int userItemId)
        {
            return _items.ContainsKey(userItemId)
                ? _items[userItemId]
                : new ItemInstance(0);
        }

        public List<ItemInstance> GetInventoryItems()
        {
            return _items.Values.ToList();
        }
    }
}