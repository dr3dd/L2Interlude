using System;
using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public class ParseSkillAcquire : IParse
    {
        private readonly IResult _result;
        private string _className;
        private IDictionary<string, IList<SkillAcquireBegin>> _acquireBegins;
        private readonly IList<SkillAcquireBegin> _acquireList;

        public ParseSkillAcquire()
        {
            _result = new Result();
            _acquireBegins = new Dictionary<string, IList<SkillAcquireBegin>>();
            _acquireList = new List<SkillAcquireBegin>();
        }
        public void ParseLine(string line)
        {
            if (line.EndsWith("_begin") && line != "skill_begin")
            {
                _className = line.Substring(0, line.Length - 6);
                return;
            }

            if (line.Equals(_className + "_end"))
            {
                _result.AddItem(_className, new List<SkillAcquireBegin>(_acquireList));
                _acquireList.Clear();
                return;
            }
            var items = line.Split("\t");
            SkillAcquireBegin acquireBegin = new SkillAcquireBegin();
            foreach (var item in items)
            {
                var splitItems = item.Split("=");
                var key = splitItems[0].Trim();
                if (splitItems.Length <= 1)
                {
                    continue;
                }
                var value = splitItems[1].Trim();
                switch (key)
                {
                    case "skill_name":
                        acquireBegin.SkillName = value.RemoveBrackets();
                        break;
                    case "get_lv":
                        acquireBegin.GetLevel = Convert.ToInt32(value);
                        break;
                    case "lv_up_sp":
                        acquireBegin.LevelUpSp = Convert.ToInt32(value);
                        break;
                    case "auto_get":
                        acquireBegin.AutoGet = value == "true";
                        break;
                    case "item_needed":
                        acquireBegin.ItemNeeded = value;
                        break;
                }
            }
            if (acquireBegin.SkillName is null)
                return;
            _acquireList.Add(acquireBegin);
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}