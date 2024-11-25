using Core.Module.ItemData;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using DataBase.Interfaces;
using System.Threading.Tasks;

namespace Core.Module.Player.PlayerInventoryModel;

public class AddOrUpdate
{
    private readonly PlayerInventory _playerInventory;
    private readonly ItemDataInit _itemDataInit;
    private readonly PlayerInstance _playerInstance;
    private readonly IUserItemRepository _itemRepository;
    public AddOrUpdate(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
        _itemDataInit = playerInventory.ItemDataInit();
        _playerInstance = playerInventory.PlayerInstance();
        _itemRepository = playerInventory.UserItemRepository();
    }
    
    public async Task AddOrUpdateItemToInventory(int itemId, int quantity)
    {
        var item = _itemDataInit.GetItemById(itemId);

        switch (item.ItemType)
        {
            case ItemType.EtcItem:
            case ItemType.Asset:
            case ItemType.QuestItem:
                await UpdateItemInInventory(itemId, quantity);
                break;
            default:
                await AddItemToInventory(item, quantity);
                break;
        }
        RefreshWeight();
    }

    private async Task UpdateItemInInventory(int itemId, int quantity)
    {
        var characterId = _playerInventory.PlayerCharacterInfo().CharacterId;
        var inventoryItem = await _itemRepository.GetInventoryItemsByItemId(characterId, itemId);
        if (inventoryItem == null)
        {
            await AddItemToInventory(_itemDataInit.GetItemById(itemId), quantity);
            return;
        }

        var newQuantity = inventoryItem.Amount + quantity;
        await _itemRepository.UpdateItemAmount(characterId, itemId, newQuantity);

        var itemInstance = _playerInventory.GetItemInstance(inventoryItem.UserItemId);
        itemInstance.Amount = newQuantity;
        await SendInventoryUpdate(itemInstance);
    }

    private async Task AddItemToInventory(ItemDataAbstract item, int quantity)
    {
        var characterId = _playerInventory.PlayerCharacterInfo().CharacterId;
        var userItemId = await _itemRepository.AddAsync(new UserItemEntity
        {
            ItemId = item.ItemId,
            ItemType = (int) item.ItemType,
            Amount = quantity,
            CharacterId = characterId,
            Enchant = 0
        });

        var objectId = _playerInventory.ObjectIdInit().NextObjectId();
        var itemInstance = new ItemInstance(objectId, _playerInstance.ServiceProvider)
        {
            UserItemId = userItemId,
            ItemId = item.ItemId,
            ItemData = item,
            Amount = quantity
        };
        _playerInventory.AddItemToInventoryCollection(itemInstance);
        await SendInventoryUpdate(itemInstance);
    }

    public async Task DestroyItemInInventory(int objectId, int quantity)
    {
        var itemInstance = _playerInventory.GetInventoryItemByObjectId(objectId);
        await RemoveUpdateItemInDb(itemInstance, quantity);
        RefreshWeight();
        await SendInventoryUpdate(itemInstance);
    }

    public async Task DestroyItemInInventoryById(int item_id, int quantity)
    {
        var itemInstance = _playerInventory.GetInventoryItemByItemId(item_id);
        await RemoveUpdateItemInDb(itemInstance, quantity);
        RefreshWeight();
        await SendInventoryUpdate(itemInstance);
    }

    private async Task RemoveUpdateItemInDb(ItemInstance itemInstance, int quantity)
    {
        var characterId = _playerInventory.PlayerCharacterInfo().CharacterId;
        
        if (quantity >= itemInstance.Amount)
        {
            await _itemRepository.DeleteAsync(itemInstance.UserItemId);
            _playerInventory.DeleteItemInInventoryCollection(itemInstance);
            itemInstance.Amount = 0;
            return;
        }
        itemInstance.Amount -= quantity;
        await _itemRepository.UpdateItemAmount(characterId, itemInstance.ItemId, itemInstance.Amount);
    }

    private async Task SendInventoryUpdate(ItemInstance itemInstance)
    {
        var inventoryUpdate = new InventoryUpdate(_playerInstance);
        if (itemInstance.Amount == 0)
        {
            inventoryUpdate.AddRemovedItem(itemInstance);
        }
        else
        {
            inventoryUpdate.AddModifiedItem(itemInstance);
        }
        await _playerInstance.SendPacketAsync(inventoryUpdate);
    }

    private void RefreshWeight()
    {
        _playerInventory.RefreshWeight();
    }
}