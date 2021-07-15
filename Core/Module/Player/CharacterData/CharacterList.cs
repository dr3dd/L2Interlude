using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player.CharacterData
{
    public class CharacterList
    {
        private readonly ICharacterRepository _characterRepository;
        public CharacterList()
        {
            _characterRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWork>().Characters;
        }
    }
}