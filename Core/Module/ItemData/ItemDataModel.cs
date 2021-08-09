using System.Collections.Generic;
using Core.Module.ParserEngine;

namespace Core.Module.ItemData
{
    public class ItemDataModel
    {
        public int ItemId { get; }
        public string Name { get; }
        public ItemType ItemType { get; }
        public SlotBitType SlotBitType { get; }
        public ItemDataModel(KeyValuePair<object, object> keyValuePair)
        {
            ItemId = (int) keyValuePair.Key;
            var itemBegin = (ItemBegin) keyValuePair.Value;
            Name = itemBegin.Name;
            SlotBitType = GetSlotBitType(itemBegin.SlotBitType);
            ItemType = GetItemType(itemBegin.ItemType);
        }

        private ItemType GetItemType(string itemType)
        {
            return itemType switch
            {
                "weapon" => ItemType.Weapon,
                "armor" => ItemType.Armor,
                "etcitem" => ItemType.EtcItem,
                "accessary" => ItemType.Accessory,
                "questitem" => ItemType.QuestItem,
                "herb_item" => ItemType.HerbItem,
                "shadow_weapon" => ItemType.ShadowWeapon,
                "asset" => ItemType.Asset,
                _ => ItemType.EtcItem
            };
        }

        private SlotBitType GetSlotBitType(string slotBitType)
        {
            return slotBitType switch
            {
                "none" => SlotBitType.None,
                "rhand" => SlotBitType.RightHand,
                "lhand" => SlotBitType.LeftHand,
                "lrhand" => SlotBitType.LeftRightHand,
                "chest" => SlotBitType.Chest,
                "legs" => SlotBitType.Legs,
                "feet" => SlotBitType.Feet,
                "head" => SlotBitType.Head,
                "gloves" => SlotBitType.Gloves,
                "onepiece" => SlotBitType.OnePiece,
                "rear;lear" => SlotBitType.RightEarning | SlotBitType.LeftEarning,
                "rfinger;lfinger" => SlotBitType.RightFinger | SlotBitType.LeftFinger,
                "necklace" => SlotBitType.Necklace,
                "back" => SlotBitType.Back,
                "underwear" => SlotBitType.UnderWear,
                "hair" => SlotBitType.Hair,
                "alldress" => SlotBitType.AllDress,
                _ => SlotBitType.None
            };
        }

        public override string ToString()
        {
            return ItemId + ": " + ItemType;
        }
    }
}