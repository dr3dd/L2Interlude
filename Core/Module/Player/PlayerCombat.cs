using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.CharacterData.Template;
using Core.Module.SkillData;
using Core.Module.SkillData.Effect;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerCombat
    {
        private readonly ITemplateHandler _templateHandler;
        private readonly PcParameterInit _statBonusInit;
        private readonly EffectInit _effectInit;
        private readonly byte _level;
        private readonly PlayerInstance _playerInstance;
        public PlayerCombat(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = playerInstance.TemplateHandler();
            _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<PcParameterInit>();
            _effectInit = playerInstance.ServiceProvider.GetRequiredService<EffectInit>();
            _level = playerInstance.PlayerStatus().Level;
        }

        /// <summary>
        /// Str bonus = Str Bonus + 100 / 100f
        /// Weapon P.Atk * Level Bonus * Str bonus
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalAttack()
        {
            var weaponAttack = 6;
            bool weaponEquipped = true;
            var baseAttack = (weaponEquipped? weaponAttack : _templateHandler.GetBasePhysicalAttack());
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
            var weaponAttack = 6;
            bool weaponEquipped = true;
            var baseAttack = (weaponEquipped? weaponAttack : _templateHandler.GetBaseMagicAttack());
            var intStat = _templateHandler.GetInt();
            float intBonus = (_statBonusInit.GetIntBonus(intStat) + 100) / 100f;
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var result = baseAttack * (levelBonus * levelBonus) * (intBonus * intBonus); 
            return (int) result;
        }

        public int GetPhysicalDefence()
        {
            float levelBonus = _statBonusInit.GetLevelBonus(_level);
            var upperBody = _templateHandler.GetBaseDefendUpperBody();
            var lowerBody = _templateHandler.GetBaseDefendLowerBody();
            var pitch = _templateHandler.GetBaseDefendPitch();
            var boots = _templateHandler.GetBaseDefendBoots();
            var gloves = _templateHandler.GetBaseDefendGloves();
            var underwear = _templateHandler.GetBaseDefendUnderwear();
            var mantle = _templateHandler.GetBaseDefendMantle();
            
            var result = (upperBody + lowerBody + pitch + boots + gloves + underwear + mantle) * levelBonus;
            return (int) result;
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

        public int GetAccuracy()
        {
            var dexStat = _templateHandler.GetDex();
            var result = Math.Sqrt(dexStat) * 6 + _level;
            return (int) result;
        }

        public int GetEvasion()
        {
            var dexStat = _templateHandler.GetDex();
            var result = Math.Sqrt(dexStat) * 6 + _level;
            return (int) result;
        }

        public int GetCriticalRate()
        {
            var baseCritical = _templateHandler.GetBaseCritical() * 10;
            var dexStat = _templateHandler.GetDex();
            float dexBonus = (_statBonusInit.GetDexBonus(dexStat) + 100) / 100f;
            var result = dexBonus * baseCritical;
            return (int) result;
        }

        public int GetPhysicalAttackSpeed()
        {
            var baseAttackSpeed = _templateHandler.GetBaseAttackSpeed();
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

            IEnumerable<SkillDataModel> effects = _playerInstance.PlayerEffect().GetEffects();
            effects.Where(e => e.AbnormalType == AbnormalType.SpeedUp);
            
            var handler = _effectInit.GetEffectHandler("p_speed");
            handler.Calc((int)result, 30);
            var test = ((PSpeed)handler).GetEffectSpeed();
            
            return (int) test;
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

        public int GetCastSpeed()
        {
            var attackSpeed = _templateHandler.GetBaseAttackSpeed() + 33;
            var witStat = _templateHandler.GetWit();
            float witBonus = (_statBonusInit.GetWitBonus(witStat) + 100) / 100f;
            var result = witBonus * attackSpeed;
            return (int) result;
        }
    }
}