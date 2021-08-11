using System;
using System.Threading.Tasks;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerModel
    {
        private readonly PlayerInstance _playerInstance;
        private readonly ItemDataInit _itemData;
        private readonly IUserItemRepository _itemRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ITemplateHandler _template;
        
        public PlayerModel(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _template = _playerInstance.TemplateHandler();
            _itemData = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            
            var unitOfWorkService = _playerInstance.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _itemRepository = unitOfWorkService.UserItems;
            _characterRepository = unitOfWorkService.Characters;
        }
        
        public async Task CreateCharacter()
        {
            try
            {
                var entity = PrepareEntity();
                var characterId = await _characterRepository.CreateCharacterAsync(entity);
                entity.CharacterId = characterId;
                await AddInitialEquipment(entity);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private CharacterEntity PrepareEntity()
        {
            var location = _template.GetInitialStartPoint();
            var appearance = _playerInstance.PlayerAppearance();
            var characterEntity = new CharacterEntity
            {
                AccountName = appearance.AccountName,
                AccountId = 1,
                CharacterName = appearance.CharacterName,
                FaceIndex = appearance.Face,
                HairShapeIndex = appearance.HairStyle,
                HairColorIndex = appearance.HairColor,
                Gender = appearance.Gender,
                Race = _template.GetRaceId(),
                ClassId = _template.GetClassId(),
                Level = _playerInstance.PlayerStatus().Level,
                Cp = _playerInstance.PlayerStatus().GetMaxCp(),
                Hp = _playerInstance.PlayerStatus().GetMaxHp(),
                Mp = _playerInstance.PlayerStatus().GetMaxMp(),
                Duel = 0,
                Exp = 0,
                Sp = 0,
                Nickname = "",
                Pk = 0,
                MaxCp = _playerInstance.PlayerStatus().GetMaxCp(),
                MaxHp = _playerInstance.PlayerStatus().GetMaxHp(),
                MaxMp = _playerInstance.PlayerStatus().GetMaxMp(),
                QuestFlag = "",
                QuestMemo = "",
                StBack = 0,
                StChest = 0,
                StFeet = 0,
                StGloves = 0,
                StHead = 0,
                StLegs = 0,
                StNeck = 0,
                StBothHand = 0,
                StLeftEar = 0,
                StLeftFinger = 0,
                StLeftHand = 0,
                StRightEar = 0,
                StRightFinger = 0,
                StRightHand = 0,
                StUnderwear = 0,
                StFace = 0,
                StHair = 0,
                StHairAll = 0,
                IsInVehicle = false,
                XLoc = location.GetX(),
                YLoc = location.GetY(),
                ZLoc = location.GetZ(),
            };
            return characterEntity;
        }

        private async Task AddInitialEquipment(CharacterEntity entity)
        {
            var initialEquipment = _template.GetInitialEquipment();
            var items = _itemData.GetItemsByNames(initialEquipment);
            items.ForEach(item =>
            {
                AddItemsToInventory(entity.CharacterId, item);
                EquipCharacter(entity, item);
            });
            await _characterRepository.UpdateCharacterAsync(entity);
        }

        private void EquipCharacter(CharacterEntity entity, ItemDataModel item)
        {
            if (!IsEquippable(item)) return;

            switch (item.SlotBitType)
            {
                case SlotBitType.None:
                    break;
                case SlotBitType.RightHand:
                    entity.StRightHand = item.ItemId;
                    break;
                case SlotBitType.LeftHand:
                    entity.StLeftHand = item.ItemId;
                    break;
                case SlotBitType.LeftRightHand:
                    break;
                case SlotBitType.Chest:
                    entity.StChest = item.ItemId;
                    break;
                case SlotBitType.Legs:
                    entity.StLegs = item.ItemId;
                    break;
                case SlotBitType.Feet:
                    entity.StFeet = item.ItemId;
                    break;
                case SlotBitType.Head:
                    entity.StHead = item.ItemId;
                    break;
                case SlotBitType.Gloves:
                    entity.StGloves = item.ItemId;
                    break;
                case SlotBitType.OnePiece:
                    break;
                case SlotBitType.RightEarning:
                    entity.StRightEar = item.ItemId;
                    break;
                case SlotBitType.LeftEarning:
                    entity.StLeftEar = item.ItemId;
                    break;
                case SlotBitType.RightFinger:
                    entity.StRightFinger = item.ItemId;
                    break;
                case SlotBitType.LeftFinger:
                    entity.StLeftFinger = item.ItemId;
                    break;
                case SlotBitType.Necklace:
                    entity.StNeck = item.ItemId;
                    break;
                case SlotBitType.Back:
                    entity.StBack = item.ItemId;
                    break;
                case SlotBitType.UnderWear:
                    entity.StUnderwear = item.ItemId;
                    break;
                case SlotBitType.Hair:
                    entity.StHair = item.ItemId;
                    break;
                case SlotBitType.AllDress:
                    entity.StHairAll = item.ItemId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsEquippable(ItemDataModel item)
        {
            return item.ActionType == ActionType.ActionEquip;
        }

        private void AddItemsToInventory(int characterId, ItemDataModel item)
        {
            _itemRepository.AddAsync(new UserItemEntity
            {
                ItemId = item.ItemId,
                ItemType = (int) item.ItemType,
                Amount = 1,
                CharacterId = characterId,
                Enchant = 0
            });
        }
    }
}