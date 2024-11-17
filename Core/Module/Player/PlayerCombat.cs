using System;
using System.Collections.Generic;
using Core.Module.AreaData;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerCombat : ICharacterCombat
    {
        private readonly ITemplateHandler _templateHandler;
        private readonly PcParameterInit _statBonusInit;
        private readonly byte _level;
        private readonly PlayerInstance _playerInstance;
        private readonly CharacterMovement _characterMovement;
        public PlayerCombat(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = playerInstance.TemplateHandler();
            _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<PcParameterInit>();
            _level = playerInstance.PlayerStatus().Level;
            _characterMovement = _playerInstance.CharacterMovement();
        }

        public ItemInstance GetWeapon()
        {
            var inventory = _playerInstance.PlayerInventory();
            if (inventory.IsBodyPartInSlotId(SlotBitType.LeftRightHand))
            {
                return inventory.GetBodyPartBySlotId((int)SlotBitType.LeftRightHand);
            }
            if (inventory.IsBodyPartInSlotId(SlotBitType.LeftHand))
            {
                return inventory.GetBodyPartBySlotId((int)SlotBitType.LeftHand);
            }
            return inventory.GetBodyPartBySlotId((int)SlotBitType.RightHand);
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
        /// pAtk = weaponPAtk * mod_lvl * mod_str * mod_per  + mod_diff
        /// Str bonus = Str Bonus + 100 / 100f
        /// Weapon P.Atk * Level Bonus * Str bonus
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalAttack()
        {
            var weaponPAtk = GetPhysicalWeaponDamage();
            var strStat = _templateHandler.GetStr();
            float strBonus = (_statBonusInit.GetStrBonus(strStat) + 100) / 100f;
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var result = weaponPAtk * levelBonus * strBonus;

            var effects = GetPlayerEffects();
            result = CalculateStats.CalculatePhysicalAttack(effects, result);
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

        /// <summary>
        /// pDef = (4 + ArmorPDef) * mod_lvl * mod_per + mod_diff
        /// ArmorPDef - total sum physical defence of all equipped armors
        /// mod_per - skill affects on physical defence in per
        /// mod_diff - kill affects on physical defence in diff
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalDefence()
        {
            float result = 0;
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

                var armorPDef = (upperBody + lowerBody + pitch + boots + gloves + underwear + mantle);
                result = (4 + armorPDef) * levelBonus;

                var effects = GetPlayerEffects();
                result = CalculateStats.CalculatePhysicalDefence(effects, result);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": "  + ex.Message);
            }
            return (int)result;
        }

        private int GetMantle()
        {
            var mantle = _templateHandler.GetBaseDefendMantle(); //TODO
            return mantle;
        }

        private int GetUnderwear()
        {
            var itemUnderwear = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.UnderWear);
            var underwear = itemUnderwear.UserItemId == 0
                ? _templateHandler.GetBaseDefendUnderwear()
                : itemUnderwear.ItemData.PhysicalDefense;
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
            var itemBoots = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Feet);
            var boots = itemBoots.UserItemId == 0
                ? _templateHandler.GetBaseDefendBoots()
                : itemBoots.ItemData.PhysicalDefense;
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

        /// <summary>
        /// Formula magical defence
        /// mDef =  accMDef * mod_lvl * mod_men * mod_per  + mod_diff
        /// accMDef - total magical defence of Jewels
        /// mod_per - skills which are affected on magic defence in per
        /// mod_diff - skills which are affected on magic defence in diff
        /// </summary>
        /// <returns></returns>
        public int GetMagicalDefence()
        {
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var leftEarning = GetLeftEarning();
            var rightEarning = GetRightEarning();
            var leftRing = GetLeftRing();
            var rightRing = GetRightRing();
            var necklace = GetNecklace();
            
            var menStat = _templateHandler.GetMen();
            float menBonus = (_statBonusInit.GetMenBonus(menStat) + 100) / 100f;

            var result = (leftEarning + rightEarning + leftRing + rightRing + necklace) * menBonus * levelBonus;
            
            var effects = GetPlayerEffects();
            result = CalculateStats.CalculateMagicalDefence(effects, result);
            return (int) result;
        }

        private int GetNecklace()
        {
            var itemNecklace = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.Necklace);
            var necklace = itemNecklace.UserItemId == 0
                ? _templateHandler.GetBaseMagicDefendNecklace()
                : itemNecklace.ItemData.MagicalDefense;
            return necklace;
        }

        private int GetRightRing()
        {
            var itemRightRing = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.RightFinger);
            var rightRing = itemRightRing.UserItemId == 0
                ? _templateHandler.GetBaseMagicDefendRightRing()
                : itemRightRing.ItemData.MagicalDefense;
            return rightRing;
        }

        private int GetLeftRing()
        {
            var itemLeftRing = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.LeftFinger);
            var leftRing = itemLeftRing.UserItemId == 0
                ? _templateHandler.GetBaseMagicDefendLeftRing()
                : itemLeftRing.ItemData.MagicalDefense;
            return leftRing;
        }

        private int GetRightEarning()
        {
            var itemRightEarning = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.RightEarning);
            var rightEarring = itemRightEarning.UserItemId == 0
                ? _templateHandler.GetBaseMagicDefendRightEarring()
                : itemRightEarning.ItemData.MagicalDefense;
            return rightEarring;
        }

        private int GetLeftEarning()
        {
            var itemLeftEarning = _playerInstance.PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.LeftEarning);
            var leftEarring = itemLeftEarning.UserItemId == 0
                ? _templateHandler.GetBaseMagicDefendLeftEarring()
                : itemLeftEarning.ItemData.MagicalDefense;
            return leftEarring;
        }

        private IEnumerable<EffectDuration> GetPlayerEffects()
        {
            return _playerInstance.CharacterEffect().GetEffects().Values;
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
            var effects = GetPlayerEffects();
            //result = CalculateStats.CalculateAccuracy(effects, result);
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

        public int GetMagicalAttackSpeed()
        {
            throw new NotImplementedException();
        }

        public float GetCollisionRadius()
        {
            return _templateHandler.GetCollisionRadius();
        }

        public float GetCollisionHeight()
        {
            return _templateHandler.GetCollisionHeight();
        }

        public double GetCharacterSpeed()
        {
            return _characterMovement.CharacterMovementStatus().IsGroundHigh() ? GetHighSpeed() : GetLowSpeed();
        }

        public double GetHighSpeed()
        {
            var baseGroundHighSpeed= _templateHandler.GetBaseGroundHighSpeed();
            var baseUnderWaterHighSpeed= _templateHandler.GetBaseUnderWaterHighSpeed();
            var baseHighSpeed = _playerInstance.PlayerZone().IsInsideZone(AreaId.Water)
                ? baseUnderWaterHighSpeed
                : baseGroundHighSpeed;
            
            var dexStat = _templateHandler.GetDex();
            var dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100d;
            var result = baseHighSpeed * dexBonus;

            var effects = GetPlayerEffects();
            result = CalculateStats.CalculateSpeed(effects, result);
            return result;
        }

        public double GetLowSpeed()
        {
            return GetGroundLowSpeed();
        }

        public double GetGroundLowSpeed()
        {
            var baseGroundLowSpeed = _templateHandler.GetBaseGroundLowSpeed();
            var dexStat = _templateHandler.GetDex();
            var dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100d;
            var result = baseGroundLowSpeed * dexBonus;
            return result;
        }

        public float GetUnderWaterHighSpeed()
        {
            var baseGroundLowSpeed = _templateHandler.GetBaseUnderWaterHighSpeed();
            return baseGroundLowSpeed;
        }
        
        public float GetUnderWaterLowSpeed()
        {
            var baseGroundLowSpeed = _templateHandler.GetBaseUnderWaterLowSpeed();
            return baseGroundLowSpeed;
        }

        public double GetMovementSpeedMultiplier()
        {
            var baseGroundHighSpeed= _templateHandler.GetBaseGroundHighSpeed();
            return GetHighSpeed() / baseGroundHighSpeed;
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

        /// <summary>
        /// GetBaseAttackRange
        /// </summary>
        /// <returns></returns>
        public int GetBaseAttackRange()
        {
            return _templateHandler.GetBaseAttackRange();
        }

        public int GetPhysicalAttackRange()
        {
            var weapon = _playerInstance.GetActiveWeaponItem();
            int attackRange = weapon?.AttackRange ?? GetBaseAttackRange();
            return attackRange;
        }
    }
}