using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.CategoryData
{
    public class CategoryPchInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, byte> _categoryPchCollection;
        
        public CategoryPchInit(IServiceProvider provider) : base(provider)
        {
            _categoryPchCollection = new Dictionary<string, byte>();
            _parse = new ParseCategoryPch();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("CategoryPch start...");
                IResult result = Parse("category_pch.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _categoryPchCollection.Add(key, Convert.ToByte(value));
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
            LoggerManager.Info("Loaded CategoryPch: " + _categoryPchCollection.Count);
        }

        public IDictionary<string, byte> GetCategoryPchCollection() => _categoryPchCollection;
    }
}