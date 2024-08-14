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
    public class RaidBossSpawnListRepository : IRaidBossSpawnListRepository
    {
        private readonly GameConnectionFactory _connectionFactory;

        public RaidBossSpawnListRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
        }
        
        public Task<RaidBossSpawnListEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<RaidBossSpawnListEntity>> GetAllAsync()
        {
            try
            {
                using var connection = _connectionFactory.GetDbConnection();
                connection.Open();
                var sql = "SELECT * FROM raidboss_spawnlist ORDER BY boss_id";
                var result = await connection.QueryAsync<RaidBossSpawnListEntity>(sql);
                return result.ToList();
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public Task<int> AddAsync(RaidBossSpawnListEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(RaidBossSpawnListEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}