using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase.Repositories
{
    public class SpawnListRepository : ISpawnListRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public SpawnListRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<ConnectionFactory>();
        }
        
        public Task<SpawnListEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<SpawnListEntity>> GetAllAsync()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM spawnlist";
                    return (await connection.QueryAsync<SpawnListEntity>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public Task<int> AddAsync(SpawnListEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(SpawnListEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}