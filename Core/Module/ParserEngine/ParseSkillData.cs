using System;
using System.Globalization;
using L2Logger;

namespace Core.Module.ParserEngine
{
    public class ParseSkillData : IParse
    {
        private readonly IResult _result;

        public ParseSkillData()
        {
            _result = new Result();
        }
        public void ParseLine(string line)
        {
            try
            {
                var items = line.Split("\t");
                var skillBegin = new SkillBegin();
                foreach (var item in items)
                {
                    if (item.StartsWith("skill_name"))
                    {
                        skillBegin.SkillName = item.Split("=")[1].RemoveBrackets().Trim();
                        if (skillBegin.SkillName == " s_ice_bolt11")
                        {
                            var d = 1;
                        }
                    }
                    if (item.StartsWith("skill_id"))
                    {
                        skillBegin.SkillId = Convert.ToInt32(item.Split("=")[1]);
                        
                    }
                    if (item.StartsWith("level"))
                    {
                        skillBegin.Level = Convert.ToInt32(item.Split("=")[1]);
                    }
                    if (item.StartsWith("operate_type"))
                    {
                        skillBegin.OperateType = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("magic_level"))
                    {
                        skillBegin.MagicLevel = Convert.ToInt32(item.Split("=")[1]);
                    }
                    if (item.StartsWith("effect="))
                    {
                        skillBegin.Effect = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("operate_cond"))
                    {
                        skillBegin.OperateCond = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("is_magic"))
                    {
                        skillBegin.IsMagic = Convert.ToByte(item.Split("=")[1]);
                    }
                    if (item.StartsWith("mp_consume2"))
                    {
                        skillBegin.MpConsume2 = Convert.ToInt32(item.Split("=")[1]);
                    }
                    if (item.StartsWith("cast_range"))
                    {
                        skillBegin.CastRange = Convert.ToInt32(item.Split("=")[1]);
                    }
                    if (item.StartsWith("effective_range"))
                    {
                        skillBegin.EffectiveRange = Convert.ToInt32(item.Split("=")[1]);
                    }
                    if (item.StartsWith("skill_hit_time"))
                    {
                        skillBegin.SkillHitTime = Convert.ToSingle(item.Split("=")[1], CultureInfo.InvariantCulture);
                    }
                    if (item.StartsWith("skill_cool_time"))
                    {
                        skillBegin.SkillCoolTime = Convert.ToSingle(item.Split("=")[1], CultureInfo.InvariantCulture);
                    }
                    if (item.StartsWith("skill_hit_cancel_time"))
                    {
                        skillBegin.SkillHitCancelTime = Convert.ToSingle(item.Split("=")[1], CultureInfo.InvariantCulture);
                    }
                    if (item.StartsWith("reuse_delay"))
                    {
                        skillBegin.ReuseDelay = Convert.ToSingle(item.Split("=")[1], CultureInfo.InvariantCulture);
                    }
                    if (item.StartsWith("attribute"))
                    {
                        skillBegin.Attribute = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("effect_point"))
                    {
                        skillBegin.EffectPoint = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("abnormal_type"))
                    {
                        skillBegin.AbnormalType = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("target_type"))
                    {
                        skillBegin.TargetType = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("affect_scope"))
                    {
                        skillBegin.AffectScope = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("affect_limit"))
                    {
                        skillBegin.AffectLimit = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("next_action"))
                    {
                        skillBegin.NextAction = item.Split("=")[1].Trim();
                    }
                    if (item.StartsWith("ride_state"))
                    {
                        skillBegin.RideState = item.Split("=")[1].Trim();
                    }
                }
                if (skillBegin.SkillName is null)
                    return;
                _result.AddItem(skillBegin.SkillName, skillBegin);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}