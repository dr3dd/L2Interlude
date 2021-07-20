using System;
using System.Threading.Tasks;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerModel
    {
        private readonly PlayerInstance _playerInstance;
        public PlayerModel(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        public async Task CreateCharacter()
        {
            try
            {
                ICharacterRepository characterRepository = Initializer.UnitOfWork().Characters;
                await characterRepository.CreateCharacterAsync(PrepareEntity());
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private CharacterEntity PrepareEntity()
        {
            var location = _playerInstance.TemplateHandler().GetInitialStartPoint();
            var template = _playerInstance.TemplateHandler();
            var appearance = _playerInstance.PlayerAppearance();
            return new CharacterEntity
            {
                AccountName = appearance.AccountName,
                AccountId = 1,
                CharacterName = appearance.CharacterName,
                Face = appearance.Face,
                HairStyle = appearance.HairStyle,
                HairColor = appearance.HairColor,
                Gender = appearance.Gender,
                Race = template.GetRaceId(),
                ClassId = template.GetClassId(),
                Level = 1,
                Cp = template.GetCpBegin(1),
                Hp = template.GetHpBegin(1),
                Mp = template.GetMpBegin(1),
                Duel = 0,
                Exp = 0,
                Sp = 0,
                Nickname = "",
                Pk = 0,
                MaxCp = (int) template.GetCpBegin(1),
                MaxHp = (int) template.GetHpBegin(1),
                MaxMp = (int) template.GetMpBegin(1),
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
        }
    }
}