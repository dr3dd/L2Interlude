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
    public class SkillTreeRepository : ISkillTreeRepository
    {
        
        private readonly GameConnectionFactory _connectionFactory;
        private readonly IServiceProvider _serviceProvider;
        
        public SkillTreeRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
            _serviceProvider = serviceProvider;
        }
        
        public Task<SkillTreeEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<SkillTreeEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddAsync(SkillTreeEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(SkillTreeEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SkillTreeEntity>> GetSkillTreeListByClassId(int classId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM skill_trees where class_id=@ClassId ORDER BY skill_id, level";
                    IEnumerable<SkillTreeEntity> items = await connection.QueryAsync<SkillTreeEntity>(sql, new {ClassId = classId});
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