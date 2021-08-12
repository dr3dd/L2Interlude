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
    public class UserItemRepository : IUserItemRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceProvider _serviceProvider;
        public UserItemRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<ConnectionFactory>();
            _serviceProvider = serviceProvider;
        }

        public Task<UserItemEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<UserItemEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddAsync(UserItemEntity userItemEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO user_item (char_id, item_id, item_type, amount, enchant) values (@CharacterId, @ItemId, @ItemType, @Amount, @Enchant); SELECT LAST_INSERT_ID();";
                    int userItemId = await connection.ExecuteScalarAsync<int>(sql, userItemEntity);
                    return userItemId;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public Task<int> UpdateAsync(UserItemEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(int objectId)
        {
            try
            {
                using var connection = _connectionFactory.GetDbConnection();
                string sql = "DELETE FROM items WHERE object_id=@ObjectId";
                return await connection.ExecuteAsync(sql, new {ObjectId = objectId});
            } 
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<UserItemEntity> CreateItemAsync(UserItemEntity userItemEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO user_item (char_id, item_id, item_type, amount, enchant) values (@CharacterId, @ItemId, @ItemType, @Amount, @Enchant);";
                    await connection.ExecuteAsync(sql, userItemEntity);
                    return userItemEntity;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<UserItemEntity> UpdateItemAsync(UserItemEntity userItemEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    string sql = "UPDATE items SET owner_id=@OwnerId,count=@Count,loc=@Loc,loc_data=@LocData WHERE object_id=@ObjectId;";
                    await connection.ExecuteAsync(sql, userItemEntity);
                    return userItemEntity;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<List<UserItemEntity>> GetInventoryItemsByOwnerId(int charId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_item WHERE char_id = @CharId";
                    IEnumerable<UserItemEntity> items = await connection.QueryAsync<UserItemEntity>(sql, new {CharId = charId});
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
        
        public async Task<List<UserItemEntity>> GetInventoryItemsByOwnerIdAndLocId(int charId, string baseLocation, string equipLocation)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_item WHERE char_id = @CharacterId";
                    IEnumerable<UserItemEntity> items = await connection.QueryAsync<UserItemEntity>(sql, new {CharacterId = charId});
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