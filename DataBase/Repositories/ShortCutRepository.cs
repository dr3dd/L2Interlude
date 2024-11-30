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
        private readonly GameConnectionFactory _connectionFactory;

        public ShortCutRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
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
                    var sql = "SELECT * FROM shortcut_data";
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
                        "REPLACE INTO shortcut_data (char_id,slotnum,shortcut_type,shortcut_id,shortcut_macro,subjob_id) VALUES (@CharacterId, @SlotNum, @ShortcutType, @ShortcutId, @ShortcutMacro, @SubjobId)";
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

        public async Task<List<ShortCutEntity>> GetShortCutsByOwnerIdAsync(int ownerId, int subjobId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM shortcut_data WHERE char_id=@CharacterId AND subjob_id=@SubjobId";
                    var result = await connection.QueryAsync<ShortCutEntity>(sql,
                        new {CharacterId = ownerId, SubjobId = subjobId });
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
                        "DELETE FROM shortcut_data WHERE char_id=@CharacterId AND slotnum=@SlotNum AND subjob_id=@SubjobId";
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