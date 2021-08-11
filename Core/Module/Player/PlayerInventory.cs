using System;
using System.Collections.Generic;
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
        private readonly List<ItemInstance> _items;
        private readonly PlayerCharacterInfo _characterInfo;
        
        public PlayerInventory(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _itemRepository = playerInstance.ServiceProvider.GetRequiredService<IUnitOfWork>().UserItems;
            _objectIdInit = playerInstance.ServiceProvider.GetRequiredService<ObjectIdInit>();
            _itemDataInit = playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            _characterInfo = playerInstance.PlayerCharacterInfo();
            _items = new List<ItemInstance>();
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
                    ItemInstance itemInstance = new ItemInstance(objectId, itemData);
                    _items.Add(itemInstance);
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public void GetStUnderwear()
        {
            int itemId = _characterInfo.StUnderwear;
        }

        public List<ItemInstance> GetInventory()
        {
            return _items;
        }
    }
}