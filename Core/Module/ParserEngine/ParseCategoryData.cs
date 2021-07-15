using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public class ParseCategoryData : IParse
    {
        private bool _isCategoryBegin;
        private bool _squareBracketsOpen;
        private bool _curlyBracketsOpen;
        private bool _isFoundField;
        private string _tmpItem;
        
        private string _fieldName;
        private string _categoryName;
        private readonly IList<string> _categoryList;
        private readonly IResult _result;

        public ParseCategoryData()
        {
            _categoryList = new List<string>();
            _result = new Result();
        }
        public void ParseLine(string line)
        {
            switch (line)
            {
                case "category_define_begin":
                    _isCategoryBegin = true;
                    return;
                case "category_define_end":
                    _isCategoryBegin = false;
                    _result.AddItem(_categoryName, new List<string>(_categoryList));
                    _categoryList.Clear();
                    return;
            }

            if (!_isCategoryBegin)
                return;
            
            foreach (var item in line)
            {
                switch (item)
                {
                    case '=':
                        _fieldName = _tmpItem;
                        _tmpItem = string.Empty;
                        _isFoundField = true;
                        continue;
                    case ';' when _isFoundField && _curlyBracketsOpen:
                        _categoryList.Add(_tmpItem);
                        _tmpItem = string.Empty;
                        continue;
                    case '{':
                        _curlyBracketsOpen = true;
                        continue;
                    case '}':
                        _curlyBracketsOpen = false;
                        _isFoundField = false;
                        continue;
                    case '[':
                        _squareBracketsOpen = true;
                        continue;
                    case ']':
                        _categoryName = _tmpItem;
                        _tmpItem = string.Empty;
                        _isFoundField = false;
                        _squareBracketsOpen = false;
                        continue;
                    case '\t':
                        continue;
                }
                _tmpItem += item;
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}