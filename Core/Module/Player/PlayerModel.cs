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
        private readonly ITemplateHandler _template;
        
        public PlayerModel(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _template = _playerInstance.TemplateHandler();
            _itemData = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
            _itemRepository = _playerInstance.ServiceProvider.GetRequiredService<IUnitOfWork>().UserItems;
        }
        
        public async Task CreateCharacter()
        {
            try
            {
                ICharacterRepository characterRepository = Initializer.UnitOfWork().Characters;
                var characterId = await characterRepository.CreateCharacterAsync(PrepareEntity());
                AddInitialEquipment(characterId);
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
                Face = appearance.Face,
                HairStyle = appearance.HairStyle,
                HairColor = appearance.HairColor,
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

        private void AddInitialEquipment(int characterId)
        {
            var initialEquipment = _template.GetInitialEquipment();
            var items = _itemData.GetItemsByNames(initialEquipment);
            items.ForEach(item =>
            {
                _itemRepository.AddAsync(new UserItemEntity
                {
                    ItemId = item.ItemId,
                    ItemType = (int) item.ItemType,
                    Amount = 1,
                    CharacterId = characterId,
                    Enchant = 0
                });
            });
        }
    }
}