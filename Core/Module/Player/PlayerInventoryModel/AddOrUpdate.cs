using System.Threading.Tasks;
using Core.Module.ItemData;
using DataBase.Entities;
using DataBase.Interfaces;

namespace Core.Module.Player.PlayerInventoryModel;

public class AddOrUpdate
{
    private readonly PlayerInventory _playerInventory;
    private readonly ItemDataInit _itemDataInit;
    private readonly PlayerInstance _playerInstance;
    private readonly IUserItemRepository _itemRepository;
    private readonly int _characterId;
    public AddOrUpdate(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
        _itemDataInit = playerInventory.ItemDataInit();
        _playerInstance = playerInventory.PlayerInstance();
        _itemRepository = playerInventory.UserItemRepository();
        _characterId = _playerInstance.PlayerCharacterInfo().CharacterId;
    }
    
    /*
    public async Task AddUpdateItemToInventory(int myItemItemId, int myItemQty)
    {
        var itemData = _itemDataInit.GetItemById(myItemItemId);
        
        if (itemData.ItemType is ItemType.EtcItem or ItemType.Asset or ItemType.QuestItem)
        {
            var userItemEntity = await _itemRepository.GetInventoryItemsByItemId(myItemItemId, _characterId);
            if (userItemEntity != null)
            {
                myItemQty = userItemEntity.Amount + myItemQty;
                await _itemRepository.UpdateItemAmount(_characterId, myItemItemId, myItemQty);
                var itemInventory = _playerInventory.GetItemInstance(userItemEntity.UserItemId);
                itemInventory.Amount = myItemQty;
                return;
            }
        }
        await AddItemToInventory(itemData, myItemQty);
        
        var objectId = _playerInventory.ObjectIdInit().NextObjectId();
        var itemInstance = new ItemInstance(objectId, _playerInstance.ServiceProvider)
        {
            ItemId = itemData.ItemId,
            ItemData = itemData,
            Amount = 1
        };
        _playerInventory.AddItemToInventoryCollection(itemInstance);
    }

    public async Task AddItemToInventory(ItemDataAbstract item, int qty)
    {
        await _itemRepository.AddAsync(new UserItemEntity
        {
            ItemId = item.ItemId,
            ItemType = (int) item.ItemType,
            Amount = qty,
            CharacterId = _characterId,
            Enchant = 0
        });
    }
    
    */
    
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
    }

    private async Task UpdateItemInInventory(int itemId, int quantity)
    {
        var inventoryItem = await _itemRepository.GetInventoryItemsByItemId(itemId, _characterId);
        if (inventoryItem == null)
        {
            await AddItemToInventory(_itemDataInit.GetItemById(itemId), quantity);
            return;
        }

        var newQuantity = inventoryItem.Amount + quantity;
        await _itemRepository.UpdateItemAmount(_characterId, itemId, newQuantity);

        var itemInstance = _playerInventory.GetItemInstance(inventoryItem.UserItemId);
        itemInstance.Amount = newQuantity;
    }

    private async Task AddItemToInventory(ItemDataAbstract item, int quantity)
    {
        await _itemRepository.AddAsync(new UserItemEntity
        {
            ItemId = item.ItemId,
            ItemType = (int) item.ItemType,
            Amount = quantity,
            CharacterId = _characterId,
            Enchant = 0
        });

        var objectId = _playerInventory.ObjectIdInit().NextObjectId();
        var itemInstance = new ItemInstance(objectId, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            Amount = quantity
        };
        _playerInventory.AddItemToInventoryCollection(itemInstance);
    }
}