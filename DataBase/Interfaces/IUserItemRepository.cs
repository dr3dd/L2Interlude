using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface IUserItemRepository : IGenericRepository<UserItemEntity>
    {
        Task<UserItemEntity> CreateItemAsync(UserItemEntity userItemEntity);
        Task<UserItemEntity> UpdateItemAsync(UserItemEntity userItemEntity);

        Task<List<UserItemEntity>> GetInventoryItemsByOwnerId(int ownerId);
        Task<List<UserItemEntity>> GetInventoryItemsByOwnerIdAndLocId(int ownerId, string baseLocation, string equipLocation);
        Task<UserItemEntity> GetInventoryItemsByItemId(int charId, int itemId);
        Task UpdateItemAmount(int charId, int itemId, int amount);
    }
}