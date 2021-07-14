using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface ISkillTreeRepository : IGenericRepository<SkillTreeEntity>
    {
        public Task<List<SkillTreeEntity>> GetSkillTreeListByClassId(int classId);
    }
}