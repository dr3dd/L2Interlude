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
        private readonly IDictionary<SlotBitType, int> _bodyParts;
        private readonly PlayerCharacterInfo _characterInfo;

        public PlayerInventory(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _itemRepository = playerInstance.ServiceProvider.GetRequiredService<IUnitOfWork>().UserItems;
            _objectIdInit = playerInstance.ServiceProvider.GetRequiredService<ObjectIdInit>();
            _itemDataInit = playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            _bodyParts = new Dictionary<SlotBitType, int>();
            _items = new Dictionary<int, ItemInstance>();
            _characterInfo = _playerInstance.PlayerCharacterInfo();
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
                    ItemInstance itemInstance = new ItemInstance(objectId)
                    {
                        UserItemId = item.UserItemId,
                        ItemId = itemData.ItemId,
                        ItemData = itemData,
                        Amount = item.Amount
                    };
                    _items.Add(item.UserItemId, itemInstance);
                });
                InitBodyParts();
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        private void InitBodyParts()
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
                    break;
                case SlotBitType.LeftEarning:
                    break;
                case SlotBitType.Necklace:
                    break;
                case SlotBitType.RightFinger:
                    break;
                case SlotBitType.LeftFinger:
                    break;
                case SlotBitType.Head:
                    break;
                case SlotBitType.RightHand:
                    _characterInfo.StRightHand = 0;
                    _bodyParts[SlotBitType.RightHand] = 0;
                    break;
                case SlotBitType.LeftHand:
                    break;
                case SlotBitType.Gloves:
                    break;
                case SlotBitType.Legs:
                    _characterInfo.StLegs = 0;
                    _bodyParts[SlotBitType.Legs] = 0;
                    break;
                case SlotBitType.Feet:
                    break;
                case SlotBitType.LeftRightHand:
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

        public async Task EquipItemInBodySlot(ItemInstance itemInstance)
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
                    break;
                case SlotBitType.RightEarning:
                    break;
                case SlotBitType.LeftEarning:
                    break;
                case SlotBitType.Necklace:
                    break;
                case SlotBitType.RightFinger:
                    break;
                case SlotBitType.LeftFinger:
                    break;
                case SlotBitType.Head:
                    break;
                case SlotBitType.RightHand:
                    _characterInfo.StRightHand = itemInstance.UserItemId;
                    _bodyParts[SlotBitType.RightHand] = itemInstance.UserItemId;
                    break;
                case SlotBitType.LeftHand:
                    break;
                case SlotBitType.Gloves:
                    break;
                case SlotBitType.Legs:
                    _characterInfo.StLegs = itemInstance.UserItemId;
                    _bodyParts[SlotBitType.Legs] = itemInstance.UserItemId;
                    break;
                case SlotBitType.Feet:
                    break;
                case SlotBitType.LeftRightHand:
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

        public SlotBitType GetSlotBitByItem(ItemInstance itemInstance) =>
            GetBodyParts().SingleOrDefault(p => p.Value == itemInstance.UserItemId).Key;

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