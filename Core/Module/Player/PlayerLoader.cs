using System.Threading.Tasks;
using Core.Module.CharacterData.Template;
using DataBase.Interfaces;

namespace Core.Module.Player
{
    public class PlayerLoader
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly TemplateInit _templateInit;
        public PlayerLoader()
        {
            _characterRepository = Initializer.UnitOfWork().Characters;
            _templateInit = Initializer.TemplateInit();
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
            
            PlayerInstance playerInstance = new PlayerInstance(template, playerAppearance);
            return playerInstance;
        }
    }
}