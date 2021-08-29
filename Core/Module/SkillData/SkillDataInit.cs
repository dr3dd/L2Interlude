using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.SkillData
{
    public class SkillDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, SkillDataModel> _skillDataModel;
        private readonly SkillPchInit _skillPchInit;
        private readonly EffectInit _effectInit;
        public SkillDataInit(IServiceProvider provider) : base(provider)
        {
            _skillDataModel = new Dictionary<string, SkillDataModel>();
            _parse = new ParseSkillData();
            _skillPchInit = provider.GetRequiredService<SkillPchInit>();
            _effectInit = provider.GetRequiredService<EffectInit>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("SkillData start...");
                IResult result = Parse("skilldata.txt", _parse);
                foreach (var parseItem in result)
                {
                    var skillModel = new SkillDataModel((SkillBegin) parseItem, _effectInit);
                    _skillDataModel.Add(skillModel.SkillName, skillModel);
                }
                LoggerManager.Info("Loaded SkillData: " + _skillDataModel.Count);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }
        
        public int GetSkillIdByName(string name)
        {
            return _skillDataModel[name].SkillId;
        }

        public SkillDataModel GetSkillByName(string name)
        {
            return _skillDataModel[name];
        }
    }
}