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
    public class UserUserItemRepository : IUserItemRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IServiceProvider _serviceProvider;
        public UserUserItemRepository(IServiceProvider serviceProvider)
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

        public Task<int> AddAsync(UserItemEntity entity)
        {
            throw new System.NotImplementedException();
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
                    string sql = "INSERT INTO items (owner_id,item_id,count,loc,loc_data,enchant_level,price_sell,price_buy,object_id,custom_type1,custom_type2,mana_left) values (@OwnerId,@ItemId,@Count,@Loc,@LocData,@EnchantLevel,@PriceSell,@PriceBuy,@ObjectId,@CustomType1,@CustomType2,@ManaLeft);";
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

        public async Task<List<UserItemEntity>> GetInventoryItemsByOwnerId(int ownerId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_item WHERE char_id = @OwnerId";
                    IEnumerable<UserItemEntity> items = await connection.QueryAsync<UserItemEntity>(sql, new {OwnerId = ownerId});
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
        
        public async Task<List<UserItemEntity>> GetInventoryItemsByOwnerIdAndLocId(int ownerId, string baseLocation, string equipLocation)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_item WHERE char_id = @OwnerId";
                    IEnumerable<UserItemEntity> items = 
                        await connection.QueryAsync<UserItemEntity>(sql, 
                            new {OwnerId = ownerId, BaseLocation = baseLocation, EquipLocation = equipLocation}
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