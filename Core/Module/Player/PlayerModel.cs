using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.SkillData;
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
        private readonly IUserSkillRepository _skillRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ITemplateHandler _template;
        private readonly SkillAcquireInit _acquireInit;
        private readonly SkillPchInit _skillPchInit;
        private readonly SkillDataInit _skillDataInit;
        
        public PlayerModel(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _template = _playerInstance.TemplateHandler();
            _itemData = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            _acquireInit = _playerInstance.ServiceProvider.GetRequiredService<SkillAcquireInit>();
            _skillPchInit = _playerInstance.ServiceProvider.GetRequiredService<SkillPchInit>();
            _skillDataInit = _playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
            
            var unitOfWorkService = _playerInstance.GetUnitOfWork();
            _itemRepository = unitOfWorkService.UserItems;
            _characterRepository = unitOfWorkService.Characters;
            _skillRepository = unitOfWorkService.UserSkill;
        }
        
        public async Task CreateCharacter()
        {
            try
            {
                var location = _template.GetInitialStartPoint();
                _playerInstance.SetXYZ(location.GetX(), location.GetY(), location.GetZ());
                var entity = PrepareEntity();
                var characterId = await _characterRepository.CreateCharacterAsync(entity);
                entity.CharacterId = characterId;
                await AddInitialEquipment(entity);
                AddInitialSkill(characterId);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private CharacterEntity PrepareEntity()
        {
            var appearance = _playerInstance.PlayerAppearance();
            var characterInfo = _playerInstance.PlayerCharacterInfo();
            var characterEntity = new CharacterEntity
            {
                CharacterId = characterInfo.CharacterId,
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
                Exp = characterInfo.Exp,
                Sp = characterInfo.Sp,
                Nickname = "",
                Pk = 0,
                MaxCp = _playerInstance.PlayerStatus().GetMaxCp(),
                MaxHp = _playerInstance.PlayerStatus().GetMaxHp(),
                MaxMp = _playerInstance.PlayerStatus().GetMaxMp(),
                QuestFlag = "",
                QuestMemo = "",
                StBack = characterInfo.StBack,
                StChest = characterInfo.StChest,
                StFeet = characterInfo.StFeet,
                StGloves = characterInfo.StGloves,
                StHead = characterInfo.StHead,
                StLegs = characterInfo.StLegs,
                StNeck = characterInfo.StNeck,
                StBothHand = characterInfo.StBothHand,
                StLeftEar = characterInfo.StLeftEar,
                StLeftFinger = characterInfo.StLeftFinger,
                StLeftHand = characterInfo.StLeftHand,
                StRightEar = characterInfo.StRightEar,
                StRightFinger = characterInfo.StRightFinger,
                StRightHand = characterInfo.StRightHand,
                StUnderwear = characterInfo.StUnderwear,
                StFace = characterInfo.StFace,
                StHair = characterInfo.StHair,
                StHairAll = characterInfo.StHairAll,
                IsInVehicle = false,
                XLoc = _playerInstance.GetX(),
                YLoc = _playerInstance.GetY(),
                ZLoc = _playerInstance.GetZ(),
            };
            return characterEntity;
        }

        private async Task AddInitialEquipment(CharacterEntity entity)
        {
            var initialEquipment = _template.GetInitialEquipment();
            var items = _itemData.GetItemsByNames(initialEquipment);
            items.ForEach(async item =>
            {
                int userItemId = await AddItemsToInventory(entity.CharacterId, item);
                EquipCharacter(entity, item, userItemId);
            });
            await _characterRepository.UpdateCharacterAsync(entity);
        }

        private void AddInitialSkill(int characterId)
        {
            var classKey = _template.GetClassKey();
            var skills = _acquireInit
                .GetSkillAcquireListByClassKey(classKey)
                .Where(s => s.LevelToGetSkill == 1).ToList();
            
            skills.ForEach(s =>
            {
                _skillRepository.AddAsync(new UserSkillEntity
                {
                    CharacterId = characterId,
                    SkillId = _skillDataInit.GetSkillIdByName(s.SkillName),
                    SkillLevel = s.LevelToGetSkill,
                    ToEndTime = 0
                });
            });
        }

        public async Task UpdateCharacter()
        {
            var entity = PrepareEntity();
            await _characterRepository.UpdateCharacterAsync(entity);
        }

        private void EquipCharacter(CharacterEntity entity, ItemDataModel item, int userItemId)
        {
            if (!IsEquippable(item)) return;

            switch (item.SlotBitType)
            {
                case SlotBitType.None:
                    break;
                case SlotBitType.RightHand:
                    entity.StRightHand = userItemId;
                    break;
                case SlotBitType.LeftHand:
                    entity.StLeftHand = userItemId;
                    break;
                case SlotBitType.LeftRightHand:
                    entity.StBothHand = userItemId;
                    break;
                case SlotBitType.Chest:
                    entity.StChest = userItemId;
                    break;
                case SlotBitType.Legs:
                    entity.StLegs = userItemId;
                    break;
                case SlotBitType.Feet:
                    entity.StFeet = userItemId;
                    break;
                case SlotBitType.Head:
                    entity.StHead = userItemId;
                    break;
                case SlotBitType.Gloves:
                    entity.StGloves = userItemId;
                    break;
                case SlotBitType.OnePiece:
                    break;
                case SlotBitType.RightEarning:
                    entity.StRightEar = userItemId;
                    break;
                case SlotBitType.LeftEarning:
                    entity.StLeftEar = userItemId;
                    break;
                case SlotBitType.RightFinger:
                    entity.StRightFinger = userItemId;
                    break;
                case SlotBitType.LeftFinger:
                    entity.StLeftFinger = userItemId;
                    break;
                case SlotBitType.Necklace:
                    entity.StNeck = userItemId;
                    break;
                case SlotBitType.Back:
                    entity.StBack = userItemId;
                    break;
                case SlotBitType.UnderWear:
                    entity.StUnderwear = userItemId;
                    break;
                case SlotBitType.Hair:
                    entity.StHair = userItemId;
                    break;
                case SlotBitType.HairAll:
                    entity.StHairAll = userItemId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsEquippable(ItemDataModel item)
        {
            return item.ActionType == ActionType.ActionEquip;
        }

        private async Task<int> AddItemsToInventory(int characterId, ItemDataModel item)
        {
            return await _itemRepository.AddAsync(new UserItemEntity
            {
                ItemId = item.ItemId,
                ItemType = (int) item.ItemType,
                Amount = 1,
                CharacterId = characterId,
                Enchant = 0
            });
        }
        
        public async Task CharacterStoreAsync()
        {
            var entity = PrepareEntity();
            await _characterRepository.UpdateCharacterAsync(entity);
        }
    }
}