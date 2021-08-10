using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface ICharacterRepository : IGenericRepository<CharacterEntity>
    {
        Task<int> CreateCharacterAsync(CharacterEntity characterEntity);
        Task<CharacterEntity> UpdateCharacterAsync(CharacterEntity characterEntity);

        Task<List<CharacterEntity>> GetCharactersByAccountNameAsync(string accountName);
        Task<CharacterEntity> GetCharacterByObjectIdAsync(int objectId);
        bool IsCharacterExist(string characterName);
        int GetMaxObjectId();
    }
}