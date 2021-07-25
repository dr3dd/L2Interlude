using System;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerLoader
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly TemplateInit _templateInit;
        private readonly IServiceProvider _serviceProvider;
        public PlayerLoader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _characterRepository = serviceProvider.GetRequiredService<IUnitOfWork>().Characters;
            _templateInit = serviceProvider.GetRequiredService<TemplateInit>();
        }
        
        public async Task<PlayerInstance> Load(int charId)
        {
            return await Restore(charId);
        }
        
        private async Task<PlayerInstance> Restore(int charId)
        {
            var characterEntity = await _characterRepository.GetCharacterByObjectIdAsync(charId);
            var template = _templateInit.GetTemplateByClassId(characterEntity.ClassId);
            PlayerAppearance playerAppearance = new PlayerAppearance(characterEntity.AccountName,
                characterEntity.CharacterName, characterEntity.Face, characterEntity.HairColor,
                characterEntity.HairStyle, characterEntity.Gender);
            
            PlayerInstance playerInstance = new PlayerInstance(template, playerAppearance, _serviceProvider);
            var characterInfo = playerInstance.PlayerCharacterInfo();
            characterInfo.CharacterId = characterEntity.CharacterId;
            characterInfo.Location = new Location(characterEntity.XLoc, characterEntity.YLoc, characterEntity.ZLoc);
            characterInfo.CurrentCp = characterEntity.Cp;
            characterInfo.CurrentHp = characterEntity.Hp;
            characterInfo.CurrentMp = characterEntity.Mp;
            characterInfo.Exp = characterEntity.Exp;
            characterInfo.Sp = characterEntity.Sp;
            characterInfo.Level = characterEntity.Level;
            return playerInstance;
        }
    }
}