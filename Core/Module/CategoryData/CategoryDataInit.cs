using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.CategoryData
{
    public class CategoryDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, IList<string>> _categoryDataCollection;

        public CategoryDataInit(IServiceProvider provider) : base(provider)
        {
            _categoryDataCollection = new Dictionary<string, IList<string>>();
            _parse = new ParseCategoryData();
        }
        
        public override void Run()
        {
            try
            {
                LoggerManager.Info("CategoryData start...");
                IResult result = Parse("categorydata.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _categoryDataCollection.Add(key, (IList<string>) value);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
            LoggerManager.Info("Loaded CategoryData: " + _categoryDataCollection.Count);
        }
        
        public IDictionary<string, IList<string>> GetCategoryDataCollection() => _categoryDataCollection;
    }
}