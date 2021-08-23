using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.SkillData
{
    public class SkillAcquireInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, IList<SkillAcquireModel>> _skillList;
        private readonly IList<SkillAcquireModel> _tmpAcquireModels;

        public SkillAcquireInit(IServiceProvider provider) : base(provider)
        {
            _skillList = new Dictionary<string, IList<SkillAcquireModel>>();
            _tmpAcquireModels = new List<SkillAcquireModel>();
            _parse = new ParseSkillAcquire();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("SkillAcquire start...");
                IResult result = Parse("skillacquire.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    foreach (var item in (IList<SkillAcquireBegin>)value)
                    {
                        _tmpAcquireModels.Add(new SkillAcquireModel(item));
                    }
                    _skillList.Add(key.ToString(), new List<SkillAcquireModel>(_tmpAcquireModels));
                    _tmpAcquireModels.Clear();
                }
                LoggerManager.Info("Loaded SkillAcquire: " + _skillList.Count);
            }
            catch (Exception ex)
            {
                
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public List<SkillAcquireModel> GetSkillAcquireListByClassKey(string classKey)
        {
            return _skillList[classKey].ToList();
        }
    }
}