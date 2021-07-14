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
    public class ShortCutRepository : IShortCutRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public ShortCutRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<ConnectionFactory>();
        }
        
        public Task<ShortCutEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<ShortCutEntity>> GetAllAsync()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM character_shortcuts";
                    var result = await connection.QueryAsync<ShortCutEntity>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> AddAsync(ShortCutEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                        "REPLACE INTO character_shortcuts (char_obj_id,slot,page,type,shortcut_id,level,class_index) VALUES (@CharacterObjectId, @Slot, @Page, @Type, @ShortcutId, @Level, @ClassIndex)";
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

        public Task<int> UpdateAsync(ShortCutEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ShortCutEntity>> GetShortCutsByOwnerIdAsync(int ownerId, int classIndex)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM character_shortcuts WHERE char_obj_id=@CharacterObjectId AND class_index=@ClassIndex";
                    var result = await connection.QueryAsync<ShortCutEntity>(sql,
                        new {CharacterObjectId = ownerId, ClassIndex = classIndex});
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> DeleteShortCutAsync(ShortCutEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                        "DELETE FROM character_shortcuts WHERE char_obj_id=@CharacterObjectId AND slot=@Slot AND page=@Page AND class_index=@ClassIndex";
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
    }
}