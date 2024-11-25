using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.Player.PlayerInventoryModel;
using Core.Module.WorldData;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player;

public class PlayerInventory
{
    private readonly PlayerInstance _playerInstance;
    private readonly IUserItemRepository _itemRepository;
    private readonly ObjectIdInit _objectIdInit;
    private readonly ItemDataInit _itemDataInit;
    private readonly IDictionary<int, ItemInstance> _items;
    private readonly IDictionary<SlotBitType, int> _bodyParts;
    private readonly PlayerCharacterInfo _characterInfo;
    private readonly AddOrUpdate _addOrUpdate;
    private int _totalWeight;
    public AddOrUpdate AddOrUpdate() => _addOrUpdate;
    public ItemDataInit ItemDataInit() => _itemDataInit;
    public PlayerInstance PlayerInstance()  => _playerInstance;
    public IUserItemRepository UserItemRepository() => _itemRepository;
    public ObjectIdInit ObjectIdInit() => _objectIdInit;
    public PlayerCharacterInfo PlayerCharacterInfo() => _characterInfo;

    public PlayerInventory(PlayerInstance playerInstance)
    {
        _playerInstance = playerInstance;
        _itemRepository = playerInstance.GetUnitOfWork().UserItems;
        _objectIdInit = playerInstance.ServiceProvider.GetRequiredService<ObjectIdInit>();
        _itemDataInit = playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
        _bodyParts = new Dictionary<SlotBitType, int>();
        _items = new Dictionary<int, ItemInstance>();
        _characterInfo = _playerInstance.PlayerCharacterInfo();
        _addOrUpdate = new AddOrUpdate(this);
    }

    public async Task RestoreInventory()
    {
        try
        {
            int charId = _characterInfo.CharacterId;
            var items = await _itemRepository.GetInventoryItemsByOwnerId(charId);
            items.ForEach(item =>
            {
                var itemData = _itemDataInit.GetItemById(item.ItemId);
                int objectId = _objectIdInit.NextObjectId();
                ItemInstance itemInstance = new ItemInstance(objectId, _playerInstance.ServiceProvider)
                {
                    UserItemId = item.UserItemId,
                    ItemId = itemData.ItemId,
                    ItemData = itemData,
                    Amount = item.Amount
                };
                _items.Add(item.UserItemId, itemInstance);
            });
            InitBodyParts();
            RefreshWeight();
        }
        catch (Exception ex)
        {
            LoggerManager.Error(ex.Message);
            throw;
        }
    }

    public void InitBodyParts()
    {
        _items.TryGetValue(_characterInfo.StUnderwear, out var underWear);
        _bodyParts.Add(SlotBitType.UnderWear, underWear?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StRightEar, out var rightEarning);
        _bodyParts.Add(SlotBitType.RightEarning, rightEarning?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StLeftEar, out var leftEarning);
        _bodyParts.Add(SlotBitType.LeftEarning, leftEarning?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StNeck, out var necklace);
        _bodyParts.Add(SlotBitType.Necklace, necklace?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StRightFinger, out var rightFinger);
        _bodyParts.Add(SlotBitType.RightFinger, rightFinger?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StLeftFinger, out var leftFinger);
        _bodyParts.Add(SlotBitType.LeftFinger, leftFinger?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StHead, out var head);
        _bodyParts.Add(SlotBitType.Head, head?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StRightHand, out var rightHand);
        _bodyParts.Add(SlotBitType.RightHand, rightHand?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StLeftHand, out var leftHand);
        _bodyParts.Add(SlotBitType.LeftHand, leftHand?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StGloves, out var gloves);
        _bodyParts.Add(SlotBitType.Gloves, gloves?.UserItemId ?? 0);
           
        _items.TryGetValue(_characterInfo.StChest, out var chest);
        _bodyParts.Add(SlotBitType.Chest, chest?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StLegs, out var legs);
        _bodyParts.Add(SlotBitType.Legs, legs?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StFeet, out var feet);
        _bodyParts.Add(SlotBitType.Feet, feet?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StBack, out var back);
        _bodyParts.Add(SlotBitType.Back, back?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StBothHand, out var leftRightHand);
        _bodyParts.Add(SlotBitType.LeftRightHand, leftRightHand?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StHair, out var hair);
        _bodyParts.Add(SlotBitType.Hair, hair?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StFace, out var face);
        _bodyParts.Add(SlotBitType.Face, face?.UserItemId ?? 0);
            
        _items.TryGetValue(_characterInfo.StHairAll, out var hairAll);
        _bodyParts.Add(SlotBitType.HairAll, hairAll?.UserItemId ?? 0);
    }

    public IDictionary<SlotBitType, int> GetBodyParts()
    {
        return _bodyParts;
    }

    public async Task UnEquipItemInBodySlot(int slot)
    {
        switch ((SlotBitType) slot)
        {
            case SlotBitType.Back:
                break;
            case SlotBitType.Chest:
                _characterInfo.StChest = 0;
                _bodyParts[SlotBitType.Chest] = 0;
                break;
            case SlotBitType.None:
                break;
            case SlotBitType.UnderWear:
                break;
            case SlotBitType.RightEarning:
                _characterInfo.StRightEar = 0;
                _bodyParts[SlotBitType.RightEarning] = 0;
                break;
            case SlotBitType.LeftEarning:
                _characterInfo.StLeftEar = 0;
                _bodyParts[SlotBitType.LeftEarning] = 0;
                break;
            case SlotBitType.Necklace:
                _characterInfo.StNeck = 0;
                _bodyParts[SlotBitType.Necklace] = 0;
                break;
            case SlotBitType.RightFinger:
                _characterInfo.StRightFinger = 0;
                _bodyParts[SlotBitType.RightFinger] = 0;
                break;
            case SlotBitType.LeftFinger:
                _characterInfo.StLeftFinger = 0;
                _bodyParts[SlotBitType.LeftFinger] = 0;
                break;
            case SlotBitType.Head:
                _characterInfo.StHead = 0;
                _bodyParts[SlotBitType.Head] = 0;
                break;
            case SlotBitType.RightHand:
                _characterInfo.StRightHand = 0;
                _bodyParts[SlotBitType.RightHand] = 0;
                break;
            case SlotBitType.LeftHand:
                _characterInfo.StLeftHand = 0;
                _bodyParts[SlotBitType.LeftHand] = 0;
                break;
            case SlotBitType.Gloves:
                _characterInfo.StGloves = 0;
                _bodyParts[SlotBitType.Gloves] = 0;
                break;
            case SlotBitType.Legs:
                _characterInfo.StLegs = 0;
                _bodyParts[SlotBitType.Legs] = 0;
                break;
            case SlotBitType.Feet:
                break;
            case SlotBitType.LeftRightHand:
                _characterInfo.StBothHand = 0;
                _bodyParts[SlotBitType.LeftRightHand] = 0;
                break;
            case SlotBitType.OnePiece:
                break;
            case SlotBitType.Hair:
                break;
            case SlotBitType.Face:
                break;
            case SlotBitType.HairAll:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
        await _playerInstance.PlayerModel().UpdateCharacter();
    }

    public void EquipItemInBodySlot(ItemInstance itemInstance)
    {
        SlotBitType slot = itemInstance.ItemData.SlotBitType;
        switch (slot)
        {
            case SlotBitType.Back:
                break;
            case SlotBitType.Chest:
                _characterInfo.StChest = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Chest] = itemInstance.UserItemId;
                break;
            case SlotBitType.None:
                break;
            case SlotBitType.UnderWear:
                _characterInfo.StUnderwear = itemInstance.UserItemId;
                _bodyParts[SlotBitType.UnderWear] = itemInstance.UserItemId;
                break;
            case SlotBitType.RightEarning:
                break;
            case SlotBitType.LeftEarning:
                break;
            case SlotBitType.Necklace:
                _characterInfo.StNeck = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Necklace] = itemInstance.UserItemId;
                break;
            case SlotBitType.RightFinger:
                break;
            case SlotBitType.LeftFinger:
                break;
            case SlotBitType.Head:
                _characterInfo.StHead = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Head] = itemInstance.UserItemId;
                break;
            case SlotBitType.RightHand:
                _characterInfo.StRightHand = itemInstance.UserItemId;
                _bodyParts[SlotBitType.RightHand] = itemInstance.UserItemId;
                break;
            case SlotBitType.LeftHand:
                _characterInfo.StLeftHand = itemInstance.UserItemId;
                _bodyParts[SlotBitType.LeftHand] = itemInstance.UserItemId;
                break;
            case SlotBitType.Gloves:
                _characterInfo.StGloves = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Gloves] = itemInstance.UserItemId;
                break;
            case SlotBitType.Legs:
                _characterInfo.StLegs = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Legs] = itemInstance.UserItemId;
                break;
            case SlotBitType.Feet:
                _characterInfo.StFeet = itemInstance.UserItemId;
                _bodyParts[SlotBitType.Feet] = itemInstance.UserItemId;
                break;
            case SlotBitType.LeftRightHand:
                _characterInfo.StBothHand = itemInstance.UserItemId;
                _bodyParts[SlotBitType.LeftRightHand] = itemInstance.UserItemId;
                break;
            case SlotBitType.OnePiece:
                break;
            case SlotBitType.Hair:
                break;
            case SlotBitType.Face:
                break;
            case SlotBitType.HairAll:
                break;
            case SlotBitType.LeftEarning | SlotBitType.RightEarning:
                if (_characterInfo.StLeftEar == 0)
                {
                    _characterInfo.StLeftEar = itemInstance.UserItemId;
                    _bodyParts[SlotBitType.LeftEarning] = itemInstance.UserItemId;
                    break;
                }
                _characterInfo.StRightEar = itemInstance.UserItemId;
                _bodyParts[SlotBitType.RightEarning] = itemInstance.UserItemId;
                break;
            case SlotBitType.LeftFinger | SlotBitType.RightFinger:
                if (_characterInfo.StLeftFinger == 0)
                {
                    _characterInfo.StLeftFinger = itemInstance.UserItemId;
                    _bodyParts[SlotBitType.LeftFinger] = itemInstance.UserItemId;
                    break;
                }
                _characterInfo.StRightFinger = itemInstance.UserItemId;
                _bodyParts[SlotBitType.RightFinger] = itemInstance.UserItemId;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }

    public ItemInstance GetUnEquippedBodyPartItem(int slot)
    {
        var itemInstance = GetBodyPartBySlotId(slot);
        itemInstance.Change = ItemInstance.Modified;
        return itemInstance;
    }

    public ItemInstance GetBodyPartBySlotId(int slotId)
    {
        var userItemId = GetBodyParts()[(SlotBitType)slotId];
        return GetItemInstance(userItemId);
    }

    public bool IsBodyPartInSlotId(SlotBitType slotBitType)
    {
        var userItemId = GetBodyParts()[slotBitType];
        return userItemId != 0;
    }

    public SlotBitType GetSlotBitByItem(ItemInstance itemInstance) =>
        GetBodyParts().SingleOrDefault(p => p.Value == itemInstance.UserItemId).Key;

    public ItemInstance GetItemInstance(int userItemId)
    {
        return _items.TryGetValue(userItemId, out var item)
            ? item
            : new ItemInstance(0, _playerInstance.ServiceProvider);
    }

    public List<ItemInstance> GetInventoryItems()
    {
        return _items.Values.ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemInstance"></param>
    public void AddItemToInventoryCollection(ItemInstance itemInstance)
    {
        _items.Add(itemInstance.UserItemId, itemInstance);
    }

    public void DeleteItemInInventoryCollection(ItemInstance itemInstance)
    {
        _items.Remove(itemInstance.UserItemId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public ItemInstance GetInventoryItemByObjectId(int objectId)
    {
        return _items.Values.SingleOrDefault(obj => obj.ObjectId == objectId);
    }
    public ItemInstance GetInventoryItemByItemId(int itemId)
    {
        return _items.Values.SingleOrDefault(obj => obj.ItemId == itemId);
    }

    /// <summary>
    /// Refresh Inventory Weight
    /// </summary>
    public void RefreshWeight()
    {
        var weight = _items.Values.Sum(itemInstance => itemInstance.ItemData.Weight * itemInstance.Amount);
        _totalWeight = weight;
    }

    /// <summary>
    /// Get Total Weight of Inventory
    /// </summary>
    /// <returns></returns>
    public int GetTotalWeight()
    {
        return _totalWeight;
    }
}