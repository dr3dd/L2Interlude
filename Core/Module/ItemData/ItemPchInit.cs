using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.ItemData
{
    public class ItemPchInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, int> _items;
        
        public ItemPchInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseItemPch();
            _items = new Dictionary<string, int>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("ItemPch start...");
                IResult result = Parse("item_pch.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _items.Add(key.ToString(), (int) value);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public int GetItemIdByName(string name)
        {
            return _items[name];
        }

        public IDictionary<string, int> GetItems()
        {
            return _items;
        }
    }
}