using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using L2Logger;

namespace Core.Module.ParserEngine
{
    public class ParseItemData : IParse
    {
        private readonly IResult _result;
        private IDictionary<int, ItemBegin> _itemBegins;

        public ParseItemData()
        {
            _result = new Result();
            _itemBegins = new Dictionary<int, ItemBegin>();
        }
        
        public void ParseLine(string line)
        {
            try
            {
                var split = line.Split("\t");
                ItemBegin itemData = new ItemBegin();
                for (int i = 0; i < split.Length; i++)
                {
                    var item = split[i].RemoveBrackets();
                    if (!split[0].StartsWith("item_begin")) return;
                    switch (i)
                    {
                        case 1:
                            itemData.ItemType = item;
                            break;
                        case 2:
                            itemData.Id = Convert.ToInt32(item);
                            break;
                        case 3:
                            itemData.Name = item;
                            break;
                    }

                    if (item.StartsWith("item_type"))
                    {
                        var itemType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                    }
                    if (item.StartsWith("slot_bit_type"))
                    {
                        var slotBitType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.SlotBitType = slotBitType;
                    }
                    if (item.StartsWith("armor_type"))
                    {
                        var armorType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.ArmorType = armorType;
                    }
                    if (item.StartsWith("etcitem_type"))
                    {
                        var etcItemType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.EtcItemType = etcItemType;
                    }
                    if (item.StartsWith("recipe_id"))
                    {
                        var recipeId = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.RecipeId = Convert.ToInt32(recipeId);
                    }
                    if (item.StartsWith("blessed="))
                    {
                        var blessed = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.Blessed = Convert.ToInt32(blessed);
                    }
                    if (item.StartsWith("weight"))
                    {
                        var weight = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.Weight = Convert.ToInt32(weight);
                    }
                    if (item.StartsWith("default_action"))
                    {
                        var defaultAction = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.DefaultAction = defaultAction;
                    }
                    if (item.StartsWith("consume_type"))
                    {
                        var consumeType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.ConsumeType = consumeType;
                    }
                    if (item.StartsWith("initial_count"))
                    {
                        var initialCount = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.InitialCount = Convert.ToInt32(initialCount);
                    }
                    if (item.StartsWith("maximum_count"))
                    {
                        var maximumCount = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.MaximumCount = Convert.ToInt32(maximumCount);
                    }
                    if (item.StartsWith("soulshot_count"))
                    {
                        var soulShotCount = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.SoulShotCount = Convert.ToByte(soulShotCount);
                    }
                    if (item.StartsWith("spiritshot_count"))
                    {
                        var spiritShotCount = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.SpiritShotCount = Convert.ToByte(spiritShotCount);
                    }
                    if (item.StartsWith("reduced_soulshot"))
                    {
                        var reducedSoulShot = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                    }
                    if (item.StartsWith("reduced_spiritshot"))
                    {
                        var reducedSpiritShot = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                    }
                    if (item.StartsWith("reduced_mp_consume"))
                    {
                        var reducedMpConsume = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                    }
                    if (item.StartsWith("immediate_effect"))
                    {
                        var immediateEffect = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                    }
                    if (item.StartsWith("price"))
                    {
                        var price = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.Price = Convert.ToInt32(price);
                    }
                    if (item.StartsWith("default_price"))
                    {
                        var defaultPrice = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.DefaultPrice = Convert.ToInt32(defaultPrice);
                    }
                    if (item.StartsWith("item_skill"))
                    {
                        var itemSkill = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.ItemSkill = itemSkill;
                    }
                    if (item.StartsWith("material_type"))
                    {
                        var materialType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.MaterialType = materialType;
                    }
                    if (item.StartsWith("is_trade"))
                    {
                        var isTrade = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.IsTrade = Convert.ToByte(isTrade) == 1;
                    }
                    if (item.StartsWith("is_destruct"))
                    {
                        var isDestruct = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.IsDestruct = Convert.ToByte(isDestruct) == 1;
                    }
                    if (item.StartsWith("hit_modify"))
                    {
                        var hitModify = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.HitModify = Convert.ToSingle(hitModify, CultureInfo.InvariantCulture);
                    }
                    if (item.StartsWith("magical_damage"))
                    {
                        var magicalDamage = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.MagicalDamage = Convert.ToInt32(magicalDamage);
                    }
                    if (item.StartsWith("physical_damage"))
                    {
                        var physicalDamage = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.PhysicalDamage = Convert.ToInt32(physicalDamage);
                    }
                    if (item.StartsWith("physical_defense"))
                    {
                        var physicalDefense = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.PhysicalDefense = Convert.ToInt32(physicalDefense);
                    }
                    if (item.StartsWith("magical_defense"))
                    {
                        var magicalDefense = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.MagicalDefense = Convert.ToInt32(magicalDefense);
                    }
                    if (item.StartsWith("weapon_type"))
                    {
                        var weaponType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.WeaponType = weaponType;
                    }
                    if (item.StartsWith("critical="))
                    {
                        var critical = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.Critical = Convert.ToInt32(critical);
                    }
                    if (item.StartsWith("attack_speed"))
                    {
                        var attackSpeed = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.AttackSpeed = Convert.ToInt32(attackSpeed);
                    }
                    if (item.StartsWith("reuse_delay"))
                    {
                        var reuseDelay = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.ReuseDelay = Convert.ToInt32(reuseDelay);
                    }
                    if (item.StartsWith("crystal_type"))
                    {
                        var crystalType = item.Substring(item.IndexOf("=", StringComparison.Ordinal)+1);
                        itemData.CrystalType = crystalType;
                    }
                }
                _result.AddItem(itemData.Id, itemData);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
                throw;
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}