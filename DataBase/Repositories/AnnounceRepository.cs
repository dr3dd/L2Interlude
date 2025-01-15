using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 13.01.2025 18:00:11

namespace DataBase.Repositories
{
    public class AnnounceRepository : IAnnounceRepository
    {
        private readonly GameConnectionFactory _connectionFactory;

        public AnnounceRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
        }

        public async Task<int> AddAsync(AnnounceEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "INSERT INTO login_announce (announce_msg, `interval`) values (@AnnounceMsg, @Interval); SELECT LAST_INSERT_ID();";

                    var id = await connection.ExecuteScalarAsync<int>(sql, entity);
                    return id;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "DELETE FROM login_announce WHERE announce_id = @AnnounceId";
                    var result = await connection.ExecuteAsync(sql, new { AnnounceId = id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<IReadOnlyList<AnnounceEntity>> GetAllAsync()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM login_announce";
                    var result = await connection.QueryAsync<AnnounceEntity>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<AnnounceEntity> GetByIdAsync(int id)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM login_announce WHERE announce_id = @AnnounceId";
                    return await connection.QuerySingleOrDefaultAsync<AnnounceEntity>(sql, new { AnnounceId = id });
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<IReadOnlyList<AnnounceEntity>> GetIntervalAnnounces()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM login_announce WHERE `interval` > 0 order by `interval`, announce_id asc;";
                    var result = await connection.QueryAsync<AnnounceEntity>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<IReadOnlyList<AnnounceEntity>> GetLoginAnnounces()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM login_announce WHERE `interval` = 0 order by announce_id asc;";
                    var result = await connection.QueryAsync<AnnounceEntity>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(AnnounceEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql =
                    "REPLACE INTO login_announce (announce_msg,`interval`) VALUES (@AnnounceMsg,@Interval); SELECT LAST_INSERT_ID(); ";
                    var result = await connection.ExecuteScalarAsync<int>(sql, entity);
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
