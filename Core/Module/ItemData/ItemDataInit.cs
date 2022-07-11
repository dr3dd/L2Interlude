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
        private readonly IDictionary<int, ItemDataAbstract> _itemDataModel;
        private readonly IDictionary<string, Type> _itemDataHandler;
        private readonly ItemPchInit _itemPchInit;

        /*
        public delegate ItemDataAbstract Weapon();
        public delegate ItemDataAbstract Armor();
        public delegate ItemDataAbstract EtcItem();
        public delegate ItemDataAbstract Accessory();
        public delegate ItemDataAbstract QuestItem();
        public delegate ItemDataAbstract Asset();
        public delegate ItemDataAbstract HerbItem();
        public delegate ItemDataAbstract ShadowWeapon();
        */
        public ItemDataInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseItemData();
            _itemDataModel = new Dictionary<int, ItemDataAbstract>();
            _itemPchInit = provider.GetRequiredService<ItemPchInit>();
            _itemDataHandler = new Dictionary<string, Type>();
            _itemDataHandler.Add("weapon", typeof(Weapon));
            _itemDataHandler.Add("armor", typeof(Armor));
            _itemDataHandler.Add("etcitem", typeof(EtcItem));
            _itemDataHandler.Add("accessary", typeof(Accessory));
            _itemDataHandler.Add("questitem", typeof(QuestItem));
            _itemDataHandler.Add("asset", typeof(Asset));
            _itemDataHandler.Add("herb_item", typeof(HerbItem));
            _itemDataHandler.Add("shadow_weapon", typeof(ShadowWeapon));
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
                    var itemBegin = (ItemBegin) keyValuePair.Value;
                    var itemData = (ItemDataAbstract)Activator.CreateInstance(_itemDataHandler[itemBegin.ItemType], itemModel);
                    /* test delegate
                    if (itemBegin.ItemType == "weapon")
                    {
                        var itemData = new Weapon(() => new ItemDataAbstract(itemModel));
                        var dd = itemData.Invoke();
                    }
                    */
                    
                    _itemDataModel.Add(itemModel.ItemId, itemData);
                }
                LoggerManager.Info("Loaded ItemData: " + _itemDataModel.Count);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public ItemDataAbstract GetItemById(int id)
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

        public ItemDataAbstract GetItemByName(string name)
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

        public List<ItemDataAbstract> GetItemsByNames(IEnumerable<string> names)
        {
            try
            {
                List<int> itemIds = GetItemIds(names);
                List<ItemDataAbstract> items = _itemDataModel
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