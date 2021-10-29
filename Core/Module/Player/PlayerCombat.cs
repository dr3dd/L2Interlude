﻿using System;
using System.Linq;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.SkillData.Effects;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerCombat
    {
        private readonly ITemplateHandler _templateHandler;
        private readonly PcParameterInit _statBonusInit;
        private readonly byte _level;
        private readonly PlayerInstance _playerInstance;
        public PlayerCombat(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = playerInstance.TemplateHandler();
            _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<PcParameterInit>();
            _level = playerInstance.PlayerStatus().Level;
        }

        public ItemInstance GetWeapon()
        {
            return _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.RightHand);
        }

        public int GetPhysicalWeaponDamage()
        {
            var itemWeapon = GetWeapon();
            return itemWeapon.UserItemId == 0 ? _templateHandler.GetBasePhysicalAttack() : itemWeapon.ItemData.PhysicalDamage;
        }
        
        public int GetMagicalWeaponDamage()
        {
            var itemWeapon = GetWeapon();
            return itemWeapon.UserItemId == 0 ? _templateHandler.GetBaseMagicAttack() : itemWeapon.ItemData.MagicalDamage;
        }

        /// <summary>
        /// Str bonus = Str Bonus + 100 / 100f
        /// Weapon P.Atk * Level Bonus * Str bonus
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalAttack()
        {
            var baseAttack = GetPhysicalWeaponDamage();
            var strStat = _templateHandler.GetStr();
            float strBonus = (_statBonusInit.GetStrBonus(strStat) + 100) / 100f;
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var result = baseAttack * levelBonus * strBonus;
            return (int) result;
        }

        /// <summary>
        /// Int bonus = Int Bonus + 100 / 100f
        /// Weapon M.Atk * (Level Bonus * Level Bonus) * (Int Bonus * Int Bonus)
        /// </summary>
        /// <returns></returns>
        public int GetMagicalAttack()
        {
            var baseAttack = GetMagicalWeaponDamage();
            var intStat = _templateHandler.GetInt();
            float intBonus = (_statBonusInit.GetIntBonus(intStat) + 100) / 100f;
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var result = baseAttack * (levelBonus * levelBonus) * (intBonus * intBonus); 
            return (int) result;
        }

        public int GetPhysicalDefence()
        {
            int result = 0;
            try
            {
                float levelBonus = _statBonusInit.GetLevelBonus(_level);
                var upperBody = GetUpperBody();
                var lowerBody = GetLowerBody();
                var pitch = GetHead();
                var boots = GetBoots();
                var gloves = GetGloves();
                var underwear = GetUnderwear();
                var mantle = GetMantle();
                result = (int)((upperBody + lowerBody + pitch + boots + gloves + underwear + mantle) * levelBonus);
                
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": "  + ex.Message);
            }
            return result;
        }

        private int GetMantle()
        {
            var mantle = _templateHandler.GetBaseDefendMantle(); //TODO
            return mantle;
        }

        private int GetUnderwear()
        {
            var underwear = _templateHandler.GetBaseDefendUnderwear(); //TODO
            return underwear;
        }

        private int GetGloves()
        {
            var itemGloves = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Gloves);
            var gloves = itemGloves.UserItemId == 0
                ? _templateHandler.GetBaseDefendGloves()
                : itemGloves.ItemData.PhysicalDefense;
            return gloves;
        }

        private int GetBoots()
        {
            var boots = _templateHandler.GetBaseDefendBoots(); // TODO
            return boots;
        }

        private int GetHead()
        {
            var itemHead = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Head);
            var pitch = itemHead.UserItemId == 0 ? _templateHandler.GetBaseDefendPitch() : itemHead.ItemData.PhysicalDefense;
            return pitch;
        }

        private int GetLowerBody()
        {
            var itemLowerBody = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Legs);
            var lowerBody = itemLowerBody.UserItemId == 0
                ? _templateHandler.GetBaseDefendLowerBody()
                : itemLowerBody.ItemData.PhysicalDefense;
            return lowerBody;
        }

        private int GetUpperBody()
        {
            var itemUpperBody = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Chest);
            var upperBody = itemUpperBody.UserItemId == 0
                ? _templateHandler.GetBaseDefendUpperBody()
                : itemUpperBody.ItemData.PhysicalDefense;
            return upperBody;
        }

        public int GetMagicalDefence()
        {
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var leftEarning = _templateHandler.GetBaseMagicDefendLeftEarring();
            var rightEarning = _templateHandler.GetBaseMagicDefendRightEarring();
            var leftRing = _templateHandler.GetBaseMagicDefendLeftRing();
            var rightRing = _templateHandler.GetBaseMagicDefendRightRing();
            var necklace = _templateHandler.GetBaseMagicDefendNecklace();
            
            var menStat = _templateHandler.GetMen();
            float menBonus = (_statBonusInit.GetMenBonus(menStat) + 100) / 100f;

            var result = (leftEarning + rightEarning + leftRing + rightRing + necklace) * menBonus * levelBonus;
            return (int) result;
        }

        /// <summary>
        /// Accuracy = ((square root of DEX)*6)+Level+Weapon Accuracy+Passive+Armor Bonus+Guidance SA+M.Def. Bonus+Buffs
        /// </summary>
        /// <returns></returns>
        public int GetAccuracy()
        {
            var dexStat = _templateHandler.GetDex();
            var itemWeapon = GetWeapon();
            var weaponAccuracy = itemWeapon.UserItemId == 0 ? 0 : itemWeapon.ItemData.HitModify;
            var result = Math.Sqrt(dexStat) * 6 + _level + weaponAccuracy;
            return (int) result;
        }

        public int GetEvasion()
        {
            var dexStat = _templateHandler.GetDex();
            var result = Math.Sqrt(dexStat) * 6 + _level;
            return (int) result;
        }

        /// <summary>
        /// Base Critical = DEX Modifier*Weapon Critical Modifier
        /// Final Critical = Base Critical+Passives+Buffs+Weapon Bonus
        /// If you have a Final Critical of higher than 500, it is set as 500.
        /// </summary>
        /// <returns></returns>
        public int GetCriticalRate()
        {
            var itemWeapon = GetWeapon();
            var baseCritical = (itemWeapon.UserItemId == 0 ? _templateHandler.GetBaseCritical() : itemWeapon.ItemData.Critical) * 10;
            var dexStat = _templateHandler.GetDex();
            float dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100f;
            var result = dexBonus * baseCritical;
            return (int) result;
        }

        public int GetPhysicalAttackSpeed()
        {
            var itemWeapon = GetWeapon();
            var baseAttackSpeed = itemWeapon.UserItemId == 0 ? _templateHandler.GetBaseAttackSpeed() : itemWeapon.ItemData.AttackSpeed;
            
            var dexStat = _templateHandler.GetDex();
            float dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100f;
            var result = dexBonus * baseAttackSpeed;
            return (int) result;
        }

        public int GetGroundHighSpeed()
        {
            var baseGroundHighSpeed= _templateHandler.GetBaseGroundHighSpeed();
            var dexStat = _templateHandler.GetDex();
            float dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100f;
            var result = baseGroundHighSpeed * dexBonus;

            var effects = _playerInstance.PlayerEffect().GetEffects().Values;
            result = effects.Where(e => e.Effect is PSpeed).Select(effect => (PSpeed)effect.Effect).Aggregate(result,
                (current, effectSpeed) => current + effectSpeed.GetEffectSpeed());
            return (int) result;
        }
        
        public int GetGroundLowSpeed()
        {
            var baseGroundHighSpeed = _templateHandler.GetBaseGroundLowSpeed();
            var dexStat = _templateHandler.GetDex();
            float dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100f;
            var result = baseGroundHighSpeed * dexBonus;
            return (int) result;
        }

        public float GetMovementSpeedMultiplier()
        {
            var baseGroundHighSpeed= _templateHandler.GetBaseGroundHighSpeed();
            return GetGroundHighSpeed() / (float)baseGroundHighSpeed;
        }

        public float GetAttackSpeedMultiplier()
        {
            return (float)((1.1 * GetPhysicalAttackSpeed()) / _templateHandler.GetBaseAttackSpeed());
        }

        /// <summary>
        /// Casting Spd. = (333*WIT bonus*Fast Spell Casting)*Armor Multiplier*Armor Bonus*Weapon Bonus*M.Def. Bonus*Buffs*Weapon Penalty+Heroic Berserker
        /// </summary>
        /// <returns></returns>
        public int GetCastSpeed()
        {
            var attackSpeed = _templateHandler.GetBaseAttackSpeed() + 33;
            var witStat = _templateHandler.GetWit();
            float witBonus = (_statBonusInit.GetWitBonus(witStat) + 100) / 100f;
            var result = attackSpeed * witBonus;
            return (int) result;
        }
    }
}