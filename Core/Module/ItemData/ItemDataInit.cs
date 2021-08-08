using System;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.ItemData
{
    public class ItemDataInit : BaseParse
    {
        private readonly IParse _parse;
        public ItemDataInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseItemData();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("ItemData start...");
                IResult result = Parse("Itemdata.txt", _parse);
                LoggerManager.Info("Loaded ItemData: " + result.GetResult().Count);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }
    }
}