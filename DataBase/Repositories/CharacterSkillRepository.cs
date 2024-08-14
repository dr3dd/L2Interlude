using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase.Repositories
{
    public class CharacterSkillRepository : ICharacterSkillRepository
    {
        private readonly GameConnectionFactory _connectionFactory;

        public CharacterSkillRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
        }
        
        public Task<CharacterSkillEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CharacterSkillEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddAsync(CharacterSkillEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "INSERT INTO character_skills (char_obj_id,skill_id,skill_level,skill_name,class_index) VALUES (@CharacterObjectId,@SkillId,@SkillLevel,@SkillName,@ClassIndex)";

                    var result= await connection.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<int> UpdateAsync(CharacterSkillEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "UPDATE character_skills SET skill_level=@SkillLevel WHERE skill_id=@SkillId AND char_obj_id=@CharacterObjectId AND class_index=@ClassIndex";

                    var result= await connection.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CharacterSkillEntity>> GetSkillsByCharObjectAndClassIndex(int ownerId, int classId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT skill_id, skill_level FROM character_skills WHERE char_obj_id = @OwnerId AND class_index = @ClassIndex";
                    IEnumerable<CharacterSkillEntity> items = 
                        await connection.QueryAsync<CharacterSkillEntity>(sql, 
                            new {OwnerId = ownerId, ClassIndex = classId}
                        );
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
    }
}