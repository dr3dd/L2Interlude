using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.ParserEngine;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.ItemData
{
    public class ItemDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<int, ItemDataModel> _itemDataModel;
        private readonly ItemPchInit _itemPchInit;
        public ItemDataInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseItemData();
            _itemDataModel = new Dictionary<int, ItemDataModel>();
            _itemPchInit = provider.GetRequiredService<ItemPchInit>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("ItemData start...");
                IResult result = Parse("Itemdata.txt", _parse);
                foreach (var keyValuePair in result.GetResult())
                {
                    var itemModel = new ItemDataModel(keyValuePair);
                    _itemDataModel.Add(itemModel.ItemId, itemModel);
                }
                LoggerManager.Info("Loaded ItemData: " + _itemDataModel.Count);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public ItemDataModel GetItemById(int id)
        {
            try
            {
                return _itemDataModel[id];
            }
            catch (Exception)
            {
                throw new Exception($": there is no item id {id}");
            }
        }

        public ItemDataModel GetItemByName(string name)
        {
            try
            {
                int itemId = _itemPchInit.GetItemIdByName(name);
                return _itemDataModel[itemId];
            }
            catch (Exception)
            {
                throw new Exception($": there is no item name {name}");
            }
        }

        public List<ItemDataModel> GetItemsByNames(IEnumerable<string> names)
        {
            try
            {
                List<int> itemIds = GetItemIds(names);
                List<ItemDataModel> items = _itemDataModel
                    .Where(i => itemIds.Contains(i.Key))
                    .Select(i => i.Value).ToList();
                return items;
            }
            catch (Exception)
            {
                throw new Exception($": there are no item names {names}");
            }
        }

        private List<int> GetItemIds(IEnumerable<string> names)
        {
            var itemIds = _itemPchInit.GetItems()
                .Where(i => names.Contains(i.Key))
                .Select(i => i.Value).ToList();
            return itemIds;
        }
    }
}