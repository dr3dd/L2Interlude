using System;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using DataBase.Interfaces;
using Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerLoader
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly TemplateInit _templateInit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWorkGame _unitOfWork;
        public PlayerLoader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _characterRepository = serviceProvider.GetRequiredService<IUnitOfWorkGame>().Characters;
            _templateInit = serviceProvider.GetRequiredService<TemplateInit>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWorkGame>();
        }
        
        public async Task<PlayerInstance> Load(int charId)
        {
            var playerInstance = await Restore(charId);
            await playerInstance.PlayerInventory().RestoreInventory();
            await playerInstance.PlayerSkill().RestorePassiveSkills();
            await playerInstance.PlayerShortCut().RestoreShortCuts();
            await playerInstance.PlayerMacros().RestoreMacroses();
            await playerInstance.PlayerQuest().RestoreQuests();
            return playerInstance;
        }
        
        private async Task<PlayerInstance> Restore(int charId)
        {
            var characterEntity = await _characterRepository.GetCharacterByObjectIdAsync(charId);
            var template = _templateInit.GetTemplateByClassId(characterEntity.ClassId);
            PlayerAppearance playerAppearance = new PlayerAppearance(characterEntity.AccountName,
                characterEntity.CharacterName, characterEntity.FaceIndex, characterEntity.HairColorIndex,
                characterEntity.HairShapeIndex, characterEntity.Gender);
            
            PlayerInstance playerInstance = new PlayerInstance(template, playerAppearance, _serviceProvider, _unitOfWork);
            var characterInfo = playerInstance.PlayerCharacterInfo();
            characterInfo.CharacterId = characterEntity.CharacterId;
            playerInstance.WorldObjectPosition().WorldPosition().SetXYZ(characterEntity.XLoc, characterEntity.YLoc, characterEntity.ZLoc);
            playerInstance.PlayerStatus().Level = characterEntity.Level;
            playerInstance.PlayerStatus().CurrentCp = characterEntity.Cp;
            playerInstance.CharacterStatus().CurrentHp = characterEntity.Hp;
            playerInstance.CharacterStatus().CurrentMp = characterEntity.Mp;
            characterInfo.RaceId = characterEntity.Race;
            characterInfo.ClassId = characterEntity.ClassId;
            characterInfo.ClassName = template.GetClassKey();
            characterInfo.Exp = characterEntity.Exp;
            characterInfo.Sp = characterEntity.Sp;

            characterInfo.StBack = characterEntity.StBack;
            characterInfo.StChest = characterEntity.StChest;
            characterInfo.StFeet = characterEntity.StFeet;
            characterInfo.StGloves = characterEntity.StGloves;
            characterInfo.StHair = characterEntity.StHair;
            characterInfo.StHead = characterEntity.StHead;
            characterInfo.StLegs = characterEntity.StLegs;
            characterInfo.StNeck = characterEntity.StNeck;
            characterInfo.StUnderwear = characterEntity.StUnderwear;
            characterInfo.StRightEar = characterEntity.StRightEar;
            characterInfo.StLeftEar = characterEntity.StLeftEar;
            characterInfo.StRightHand = characterEntity.StRightHand;
            characterInfo.StLeftHand = characterEntity.StLeftHand;
            characterInfo.StFace = characterEntity.StFace;
            characterInfo.StBothHand = characterEntity.StBothHand;
            characterInfo.StHairAll = characterEntity.StHairAll;
            characterInfo.StLeftFinger = characterEntity.StLeftFinger;
            characterInfo.StRightFinger = characterEntity.StRightFinger;
            characterInfo.QuestFlag = new BitStorage(characterEntity.QuestFlag);

            playerInstance.PlayerStatus().Level = characterEntity.Level;
            return playerInstance;
        }
    }
}