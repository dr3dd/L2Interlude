using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerStatus
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PcParameterInit _statBonusInit;
        public float CurrentCp { get; set; }
        private double _currentHp;

        public double CurrentHp
        {
            get
            {
                if (_currentHp >= GetMaxHp())
                {
                    return GetMaxHp();
                }
                return _currentHp;
            }
            set => _currentHp = value;
        }
        public float CurrentMp { get; set; }
        public byte Level { get; set; } = 1;  
        public PlayerStatus(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<PcParameterInit>();
        }

        /// <summary>
        /// maxСp = maxCp + (maxCp * CON bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxCp()
        {
            var cpBegin = _playerInstance.TemplateHandler().GetCpBegin(Level);
            var conStat = _playerInstance.TemplateHandler().GetCon();
            return (int) (cpBegin + (cpBegin * _statBonusInit.GetConBonus(conStat) / 100));
        }

        /// <summary>
        /// MAX HP  = base * mod_con * mod_per + mod_diff
        /// maxHp = maxHp + (maxHp * CON bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxHp()
        {
            var hpBegin = _playerInstance.TemplateHandler().GetHpBegin(Level);
            var conStat =  _playerInstance.TemplateHandler().GetCon();
            var result = (hpBegin + (hpBegin * _statBonusInit.GetConBonus(conStat) / 100));
            var effects = GetPlayerEffects();
            result = CalculateStats.CalculateMaxHp(effects, result);
            return (int)result;
        }
        
        /// <summary>
        /// maxMp = maxMp + (maxMp * MEN bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxMp()
        {
            var mpBegin = _playerInstance.TemplateHandler().GetMpBegin(Level);
            var menStat =  _playerInstance.TemplateHandler().GetMen();
            return (int) (mpBegin + (mpBegin * _statBonusInit.GetMenBonus(menStat) / 100));
        }

        public void DecreaseCurrentHp(double damage)
        {
            lock (this)
            {
                if (!(damage > 0)) return;
                CurrentHp -= damage; // Get diff of Hp vs value
                if (CurrentHp <= 0)
                {
                    CurrentHp = 0;
                }
                SetCurrentHp(CurrentHp); // Set Hp
            }
        }
        
        public void IncreaseCurrentHp(double heal)
        {
            lock (this)
            {
                CurrentHp += heal; // Get diff of Hp vs value
                if (CurrentHp >= GetMaxHp())
                {
                    CurrentHp = GetMaxHp();
                }
                SetCurrentHp(CurrentHp); // Set Hp
            }
        }

        private void SetCurrentHp(double newHp)
        {
            CurrentHp = newHp;
        }
        
        private IEnumerable<EffectDuration> GetPlayerEffects()
        {
            return _playerInstance.PlayerEffect().GetEffects().Values;
        }
    }
}