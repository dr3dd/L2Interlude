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
        public WeaponType WeaponType { get; }
        public ActionType ActionType { get; }
        public int MagicalDamage { get; }
        public int PhysicalDamage { get; }
        public int PhysicalDefense { get; }
        public int AttackSpeed { get; }
        public float HitModify { get; } //increase or decrease accuracy
        public int Critical { get; }
        
        public ItemDataModel(KeyValuePair<object, object> keyValuePair)
        {
            ItemId = (int) keyValuePair.Key;
            var itemBegin = (ItemBegin) keyValuePair.Value;
            Name = itemBegin.Name;
            SlotBitType = GetSlotBitType(itemBegin.SlotBitType);
            ItemType = GetItemType(itemBegin.ItemType);
            WeaponType = GetWeaponType(itemBegin.WeaponType);
            ActionType = GetActionType(itemBegin.DefaultAction);
            MagicalDamage = itemBegin.MagicalDamage;
            PhysicalDamage = itemBegin.PhysicalDamage;
            PhysicalDefense = itemBegin.PhysicalDefense;
            AttackSpeed = itemBegin.AttackSpeed;
            HitModify = itemBegin.HitModify;
            Critical = itemBegin.Critical;
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
                "alldress" => SlotBitType.HairAll,
                _ => SlotBitType.None
            };
        }

        private WeaponType GetWeaponType(string weaponType)
        {
            return weaponType switch
            {
                "none" => WeaponType.None,
                "sword" => WeaponType.Sword,
                "dagger" => WeaponType.Dagger,
                "blunt" => WeaponType.Blunt,
                "pole" => WeaponType.Pole,
                "bow" => WeaponType.Bow,
                "dualfist" => WeaponType.DualFist,
                "etc" => WeaponType.Etc,
                _ => WeaponType.None
            };
        }

        private ActionType GetActionType(string actionType)
        {
            return actionType switch
            {
                "action_none" => ActionType.ActionNone,
                "action_equip" => ActionType.ActionEquip,
                "action_skill_reduce" => ActionType.ActionSkillReduce,
                "action_soulshot" => ActionType.ActionSoulShot,
                "action_recipe" => ActionType.ActionRecipe,
                "action_seed" => ActionType.ActionSeed,
                "action_capsule" => ActionType.ActionCapsule,
                "action_fishingshot" => ActionType.ActionFishingShot,
                "action_skill_maintain" => ActionType.ActionSkillMaintain,
                _ => ActionType.ActionNone
            };
        }

        public override string ToString()
        {
            return ItemId + ": " + ItemType;
        }
    }
}