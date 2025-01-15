using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 23:06:18

namespace DataBase.Repositories
{
    public class MacrosRepository : IMacrosRepository
    {
        private readonly GameConnectionFactory _connectionFactory;

        public MacrosRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
        }

        public async Task<int> AddAsync(UserMacrosEntity macrosEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "INSERT INTO user_macroses (user_macros_id,char_id,icon,name,description,acronym,commands) values (@UserMacrosId,@CharacterObjectId,@Icon,@Name,@Description,@Acronym,@Commands); SELECT LAST_INSERT_ID();";

                    var characterId = await connection.ExecuteScalarAsync<int>(sql, macrosEntity);
                    return characterId;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(UserMacrosEntity macrosEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                    "REPLACE INTO user_macroses(macros_id, user_macros_id, char_id, icon, name, description, acronym, commands) values(@MacrosId, @UserMacrosId, @CharacterObjectId, @Icon, @Name, @Description, @Acronym, @Commands); SELECT LAST_INSERT_ID(); ";
                    var result = await connection.ExecuteScalarAsync<int>(sql, macrosEntity);
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
            throw new NotImplementedException();
        }

        public async Task<int> DeleteMarcosAsync(UserMacrosEntity macrosEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                        "DELETE FROM user_macroses WHERE char_id=@CharacterObjectId AND user_macros_id=@UserMacrosId";
                    var result = await connection.ExecuteAsync(sql, macrosEntity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public Task<IReadOnlyList<UserMacrosEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserMacrosEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserMacrosEntity>> GetMarcosesByOwnerIdAsync(int ownerId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM user_macroses WHERE char_id=@CharacterObjectId ORDER BY `user_macros_id`";
                    var result = await connection.QueryAsync<UserMacrosEntity>(sql,
                        new { CharacterObjectId = ownerId });
                    return result.ToList();
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
