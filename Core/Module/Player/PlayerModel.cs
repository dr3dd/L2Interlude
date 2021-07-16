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
            return new CharacterEntity
            {
                AccountName = _playerInstance.PlayerAppearance().AccountName,
                AccountId = 1,
                CharacterName = _playerInstance.PlayerAppearance().CharacterName,
                Face = _playerInstance.PlayerAppearance().Face,
                HairStyle = _playerInstance.PlayerAppearance().HairStyle,
                HairColor = _playerInstance.PlayerAppearance().HairColor,
                Gender = _playerInstance.PlayerAppearance().Gender,
                Race = _playerInstance.TemplateHandler().GetRaceId(),
                ClassId = _playerInstance.TemplateHandler().GetClassId(),
                Level = 1,
                Cp = 100,
                Duel = 0,
                Exp = 0,
                Hp = 100,
                Mp = 100,
                Nickname = "",
                Pk = 0,
                Sp = 0,
                MaxCp = 100,
                MaxHp = 100,
                MaxMp = 100,
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
                IsInVehicle = false,
                XLoc = 0,
                YLoc = 0,
                ZLoc = 0,
            };
        }
    }
}