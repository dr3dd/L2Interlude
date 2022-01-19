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
    public class UserSkillRepository : IUserSkillRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceProvider _serviceProvider;

        public UserSkillRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<ConnectionFactory>();
            _serviceProvider = serviceProvider;
        }
        
        public Task<UserSkillEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<UserSkillEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddAsync(UserSkillEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO user_skill (char_id, skill_id, skill_level, to_end_time) values (@CharacterId, @SkillId, @SkillLevel, @ToEndTime);";
                    var result = await connection.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(UserSkillEntity entity)
        {
            try
            {
                using var connection = _connectionFactory.GetDbConnection();
                connection.Open();
                var sql = "UPDATE user_skill SET skill_level = @SkillLevel WHERE char_id = @CharacterId AND skill_id = @SkillId;";
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
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

        public async Task<List<UserSkillEntity>> GetSkillsByCharId(int charId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT skill_id, skill_level FROM user_skill WHERE char_id = @CharacterId";
                    IEnumerable<UserSkillEntity> items = 
                        await connection.QueryAsync<UserSkillEntity>(sql, 
                            new {CharacterId = charId}
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