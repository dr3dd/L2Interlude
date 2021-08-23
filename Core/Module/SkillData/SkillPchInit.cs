using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.SkillData
{
    public class SkillPchInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, int> _skills;
        
        public SkillPchInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseSkillPch();
            _skills = new Dictionary<string, int>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("SkillPch start...");
                IResult result = Parse("skill_pch.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _skills.Add(key.ToString(), (int) value);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }
        
        public int GetSkillIdByName(string name)
        {
            return _skills[name];
        }

        public string GetSkillNameById(int skillId)
        {
            return _skills.FirstOrDefault(s => s.Value == skillId).Key;
        }

        public IDictionary<string, int> GetSkills()
        {
            return _skills;
        }
    }
}