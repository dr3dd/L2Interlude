using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Module.NpcData
{
    public class NpcTemplateInit
    {
        private readonly NpcStat _stat;
        public NpcTemplateInit(IDictionary<string, object> setStats)
        {
            _stat = new NpcStat();
            _stat.Id = ToInt(setStats["npc_id"]);
            _stat.Type = (string) setStats["npc_type"];
            _stat.Name = (string) setStats["npc_name"];
            _stat.Level = ToByte(setStats["level"]);
            _stat.AcquireExpRate = ToFloat(setStats["acquire_exp_rate"]);
            _stat.AcquireSp = ToInt(setStats["acquire_sp"]);
            _stat.UnSowing = ToBool(setStats["unsowing"]);
            _stat.Clan = ParseClanList((string) setStats["clan"]);
            _stat.IgnoreClanList = ParseClanList((string) setStats["ignore_clan_list"]);
            _stat.ClanHelpRange = ToInt(setStats["clan_help_range"]);
            _stat.SlotChest = (string) setStats["slot_chest"]; //TODO
            _stat.SlotRHand = (string) setStats["slot_rhand"];
            _stat.SlotLHand = (string) setStats["slot_lhand"];
            _stat.ShieldDefenseRate = ToShort(setStats["shield_defense_rate"]);
            _stat.ShieldDefense = ToShort(setStats["shield_defense"]);
            _stat.SkillList = ParseSkill(setStats["skill_list"]);
            _stat.NpcAiData = ParseNpcAi(setStats["npc_ai"]);
            _stat.Category = (string) setStats["category"]; //TODO
            _stat.Race = (string) setStats["race"];
            _stat.Sex = (string) setStats["sex"];
            _stat.UnDying = ToBool(setStats["undying"]);
            _stat.CanBeAttacked = ToShort(setStats["can_be_attacked"]);
            _stat.CorpseTime = ToShort(setStats["corpse_time"]);
            _stat.NoSleepMode = ToBool(setStats["no_sleep_mode"]);
            _stat.AgroRange = ToShort(setStats["agro_range"]);
            _stat.GroundHigh = ToFloatList(setStats["ground_high"]);
            _stat.GroundLow = ToFloatList(setStats["ground_low"]);
            _stat.Exp = ToDouble(setStats["exp"]);
            _stat.OrgHp = ToFloat(setStats["org_hp"]);
            _stat.OrgHpRegen = ToFloat(setStats["org_hp_regen"]);
            _stat.OrgMp = (setStats.ContainsKey("org_mp")? ToFloat(setStats["org_mp"]) : 0);
            _stat.OrgMpRegen = ToFloat(setStats["org_mp_regen"]);
            _stat.CollisionRadius = ToFloatList(setStats["collision_radius"]);
            _stat.CollisionHeight = ToFloatList(setStats["collision_height"]);
            _stat.Str = ToByte(setStats["str"]);
            _stat.Int = ToByte(setStats["int"]);
            _stat.Dex = ToByte(setStats["dex"]);
            _stat.Wit = ToByte(setStats["wit"]);
            _stat.Con = ToByte(setStats["con"]);
            _stat.Men = ToByte(setStats["men"]);
            _stat.BaseAttackType = (string) setStats["base_attack_type"];
            _stat.BaseAttackRange = ToShort(setStats["base_attack_range"]);
            _stat.BaseDamageRange = ToFloatList(setStats["base_damage_range"]);
            _stat.BaseRandDam = ToShort(setStats["base_rand_dam"]);
            _stat.BasePhysicalAttack = ToFloat(setStats["base_physical_attack"]);
            _stat.BaseCritical = ToShort(setStats["base_critical"]);
            _stat.PhysicalHitModify = ToFloat(setStats["physical_hit_modify"]);
            _stat.BaseAttackSpeed = ToFloat(setStats["base_attack_speed"]);
            _stat.BaseReuseDelay = ToShort(setStats["base_reuse_delay"]);
            _stat.BaseMagicAttack = ToFloat(setStats["base_magic_attack"]);
            _stat.BaseDefend = ToFloat(setStats["base_defend"]);
            _stat.BaseMagicDefend = ToFloat(setStats["base_magic_defend"]);
            _stat.PhysicalAvoidModify = ToFloat(setStats["physical_avoid_modify"]);
            _stat.SoulShotCount = ToByte(setStats["soulshot_count"]);
            _stat.SpiritShotCount = ToByte(setStats["spiritshot_count"]);
            _stat.HitTimeFactor = ToFloat(setStats["hit_time_factor"]);
            _stat.ItemMakeList = (string) setStats["item_make_list"]; //TODO
            _stat.CorpseMakeList = (string) setStats["corpse_make_list"]; //TODO
            _stat.AdditionalMakeList = ParseAdditionalList((string) setStats["additional_make_list"]);
            _stat.AdditionalMakeMultiList = ParseAdditionalList((string) setStats["additional_make_multi_list"]);
            _stat.HpIncrease = ToShort(setStats["hp_increase"]);
            _stat.MpIncrease = ToShort(setStats["mp_increase"]);
            _stat.SafeHeight = ToShort(setStats["safe_height"]);
        }

        /// <summary>
        /// Parse Drop List
        /// </summary>
        /// <param name="additionalMakeMultiList"></param>
        /// <returns></returns>
        private IList<NpcAdditionalListData> ParseAdditionalList(string additionalMakeMultiList)
        {
            var pattern = @"\{(\[([^\]]+)\]);(\d+);(\d+);(\d+\.?\d*)}";
            var rgx = new Regex(pattern);
            var itemList = new List<NpcAdditionalListData>();
            var matches = rgx.Matches(additionalMakeMultiList);
            foreach (Match match in matches)
            {
                NpcAdditionalListData item = new NpcAdditionalListData();
                item.Name = match.Groups[2].Value;
                item.Min = ToInt(match.Groups[3].Value);
                item.Max = ToInt(match.Groups[4].Value);
                item.Chance = ToFloat(match.Groups[5].Value);

                itemList.Add(item);
            }

            return itemList;
        }

        private IList<float> ToFloatList(object setStat)
        {
            Regex regex = new Regex(@"{(.+?)}");
            string[] items = regex.Match(setStat.ToString()).Groups[1].Value.Split(';');
            return items.Select(ToFloat).ToList();
        }

        private IList<string> ParseSkill(object setStat)
        {
            return ((string) setStat).Split(";").ToList();
        }

        private NpcAiData ParseNpcAi(object setStat)
        {
            var npcAiName = new NpcAiData();
            var npcAiMatch = Regex.Match(setStat.ToString(), @"{*\[(\w+)\];*");
            npcAiName.NpcAiName = npcAiMatch.Groups[1].Value;
            var matches = Regex.Matches(setStat.ToString(), @"{*\[(\w+)]=\[([\w\.]+)\]};*");
            foreach (Match match in matches)
            {
                if (match.Groups[1].Value == "fnHi")
                {
                    npcAiName.FnHi = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnSell")
                {
                    npcAiName.FnSell = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnBuy")
                {
                    npcAiName.FnBuy = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnUnableItemSell")
                {
                    npcAiName.FnUnableItemSell = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnYouAreChaotic")
                {
                    npcAiName.FnYouAreChaotic = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnNowSiege")
                {
                    npcAiName.FnNowSiege = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "FnNotMyLord")
                {
                    npcAiName.FnNotMyLord = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnUnderSiege")
                {
                    npcAiName.FnUnderSiege = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnNobless")
                {
                    npcAiName.FnNobless = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnNoNobless")
                {
                    npcAiName.FnNoNobless = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "fnNoNoblessItem")
                {
                    npcAiName.FnNoNoblessItem = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "MoveAroundSocial")
                {
                    npcAiName.MoveAroundSocial = short.Parse(match.Groups[2].Value);
                }
                if (match.Groups[1].Value == "MoveAroundSocial1")
                {
                    npcAiName.MoveAroundSocial1 = short.Parse(match.Groups[2].Value);
                }
                if (match.Groups[1].Value == "MoveAroundSocial2")
                {
                    npcAiName.MoveAroundSocial2 = short.Parse(match.Groups[2].Value);
                }
                if (match.Groups[1].Value == "DoorName1")
                {
                    npcAiName.DoorName1 = match.Groups[2].Value;
                }
                if (match.Groups[1].Value == "DoorName2")
                {
                    npcAiName.DoorName2 = match.Groups[2].Value;
                }
            }

            return npcAiName;
            /*
            
            var npcAi = new Dictionary<string, string>();
            if (setStat is List<object> list)
            {
                list.ForEach(i =>
                {
                    var splited = ((string)i).Split("=");
                    var key = (splited.Length > 1)? splited[0] : "npcAi";
                    var value = (splited.Length > 1)? splited[1] : splited[0];
                    npcAi.TryAdd(key, value);
                });
                return npcAi;
            }
            npcAi.TryAdd("npcAi", setStat.ToString());
            return npcAi;
            */
        }

        /// <summary>
        /// Parse Clan List
        /// </summary>
        /// <param name="setStat"></param>
        /// <returns></returns>
        private IList<string> ParseClanList(string setStat)
        {
            var clanRegex = new Regex(@"{(.+?)}");
            var value = clanRegex.Match(setStat).Groups[1].Value;
            var clanList = value.Replace("@", "").Split(';');
            return clanList[0] != "" ? clanList.ToList() : new List<string>();
        }

        public NpcStat GetStat() => _stat;
        private bool ToBool(object obj) => ToShort(obj) == 1;
        private float ToFloat(object obj) => Convert.ToSingle(obj, CultureInfo.InvariantCulture.NumberFormat);
        private double ToDouble(object obj) => Convert.ToDouble(obj, CultureInfo.InvariantCulture.NumberFormat);
        private short ToShort(object obj) => Convert.ToInt16(obj);
        private int ToInt(object obj) => Convert.ToInt32(obj);
        private byte ToByte(object obj) => Convert.ToByte(obj);

        public override string ToString()
        {
            return _stat.Id + ": " + _stat.Name;
        }
    }
}

