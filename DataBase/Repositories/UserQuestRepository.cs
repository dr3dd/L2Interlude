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
//DATE: 26.11.2024 0:08:25

namespace DataBase.Repositories
{
    internal class UserQuestRepository : IUserQuestRepository
    {
        private readonly GameConnectionFactory _connectionFactory;
        private readonly IServiceProvider _serviceProvider;
        public UserQuestRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
            _serviceProvider = serviceProvider;
        }
        public async Task<int> AddAsync(UserQuestEntity userQuestEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO user_quest (char_id, quest_no, journal, state1, state2, state3, state4, type) values (@CharacterId, @QuestNo, @Journal, @State1, @State2, @State3, @State4, @Type);";
                    return await connection.ExecuteScalarAsync<int>(sql, userQuestEntity);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(UserQuestEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                        "REPLACE INTO user_quest (char_id, quest_no, journal, state1, state2, state3, state4, type) values (@CharacterId, @QuestNo, @Journal, @State1, @State2, @State3, @State4, @Type);";
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

        public async Task<int> DeleteAsync(UserQuestEntity userQuestEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                        "DELETE FROM user_quest WHERE char_id=@CharacterId AND quest_no=@QuestNo;";
                    var result = await connection.ExecuteAsync(sql, userQuestEntity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<UserQuestEntity>> GetAllAsync(int charId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM user_quest WHERE char_id=@CharacterId ORDER BY `quest_no`";
                    var result = await connection.QueryAsync<UserQuestEntity>(sql,
                        new { CharacterId = charId });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public Task<UserQuestEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<UserQuestEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
