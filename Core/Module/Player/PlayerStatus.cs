using Core.Module.CharacterData.Template;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerStatus
    {
        private readonly PlayerInstance _playerInstance;
        private readonly BasicStatBonusInit _statBonusInit;
        public float CurrentCp { get; set; }
        public float CurrentHp { get; set; }
        public float CurrentMp { get; set; }
        public byte Level { get; set; } = 1;  
        public PlayerStatus(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<BasicStatBonusInit>();
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
        /// maxHp = maxHp + (maxHp * CON bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxHp()
        {
            var hpBegin = _playerInstance.TemplateHandler().GetHpBegin(Level);
            var conStat =  _playerInstance.TemplateHandler().GetCon();
            return (int) (hpBegin + (hpBegin * _statBonusInit.GetConBonus(conStat) / 100));
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
    }
}