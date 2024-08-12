using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Security;

namespace DataBase.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        public AccountRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<ConnectionFactory>();
        }
        
        public async Task<UserAuthEntity> GetByIdAsync(int id)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM user_auth WHERE account_id = @AccountId";
                    return await connection.QuerySingleOrDefaultAsync<UserAuthEntity>(sql, new { AccountId = id });
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<IReadOnlyList<UserAuthEntity>> GetAllAsync()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM user_auth";
                    var result = await connection.QueryAsync<UserAuthEntity>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<int> AddAsync(UserAuthEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "INSERT INTO user_auth (account_name, password) VALUES (@account_name, @password)";
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

        public async Task<int> UpdateAsync(UserAuthEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "UPDATE user_auth SET account_name = @account_name, password = @password WHERE account_id = @account_id";
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

        public async Task<int> UpdateLastUseAsync(UserAuthEntity entity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "UPDATE user_auth SET last_world = @LastWorld, last_login = @LastLogin WHERE account_id = @AccountId";
                    var result = await connection.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"{ex.Message} {ex.StackTrace}");
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
                    var sql = "DELETE FROM user_auth WHERE account_id = @AccountId";
                    var result = await connection.ExecuteAsync(sql, new {AccountId = id});
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<UserAuthEntity> GetAccountByLoginAsync(string login)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "SELECT * FROM user_auth WHERE account_name = @AccountName";
                    return await connection.QuerySingleOrDefaultAsync<UserAuthEntity>(sql, new { AccountName = login });
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<UserAuthEntity> CreateAccountAsync(string accountName, string password)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    var sql = "INSERT INTO user_auth (account_name, password, email) VALUES (@AccountName, @Password, @Email)";
                    
                    UserAuthEntity userAuthEntity = new UserAuthEntity
                    {
                        AccountName = accountName,
                        Password = L2Security.HashPassword(password),
                    };
                    //userAuthEntity.AccountId = await connection.InsertAsync(userAuthEntity);
                    userAuthEntity.AccountId = await connection.ExecuteAsync(sql, userAuthEntity);
                    return userAuthEntity;
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