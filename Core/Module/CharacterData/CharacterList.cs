using System.Collections.Generic;
using DataBase.Entities;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.CharacterData
{
    public class CharacterList
    {
        private readonly ICharacterRepository _characterRepository;
        public CharacterList()
        {
            _characterRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWork>().Characters;
        }

        public List<CharacterEntity> GetCharacterList(string accountName)
        {
            var list = _characterRepository.GetCharactersByAccountNameAsync(accountName);
            return list.Result;
        }
    }
}