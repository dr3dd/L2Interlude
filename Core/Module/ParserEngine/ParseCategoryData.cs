using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine
{
    public class ParseCategoryData : IParse
    {
        private readonly IResult _result;
        private bool isCategoryDefineBegin;
        private string newString;

        public ParseCategoryData()
        {
            _result = new Result();
        }
        public void ParseLine(string line)
        {
            if (isCategoryDefineBegin)
            {
                newString += line;
            }
            
            if (line.StartsWith("category_define_begin"))
            {
                isCategoryDefineBegin = true;
            }

            if (line.StartsWith("category_define_end"))
            {
                isCategoryDefineBegin = false;
                var pattern = @"name=\[(?<name>[\w\s]+)\]category=\{(?<categories>.*?)\}";
                var categoryPattern = @"@(?<category>[\w\s]+)";
                
                var match = Regex.Match(newString, pattern);
                var groupName = match.Groups["name"].Value;
                var categories = match.Groups["categories"].Value;
                
                var categoryMatches = Regex.Matches(categories, categoryPattern);
                var categoryList = new List<string>();
                foreach (Match categoryMatch in categoryMatches)
                {
                    var category = categoryMatch.Groups["category"].Value;
                    categoryList.Add(category);
                }
                newString = string.Empty;
                _result.AddItem(groupName, categoryList);
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}