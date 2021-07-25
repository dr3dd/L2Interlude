namespace Core.Module.Player
{
    public class PlayerStatus
    {
        private readonly PlayerInstance _playerInstance;
        public float CurrentCp { get; set; }
        public float CurrentHp { get; set; }
        public float CurrentMp { get; set; }
        public byte Level { get; set; } = 1;  
        public PlayerStatus(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        /// <summary>
        /// maxСp = maxCp + (maxCp * CON bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxCp()
        {
            var cpBegin = _playerInstance.TemplateHandler().GetCpBegin(Level);
            return (int) (cpBegin + (cpBegin * 58 / 100));
        }

        /// <summary>
        /// maxHp = maxHp + (maxHp * CON bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxHp()
        {
            var hpBegin = _playerInstance.TemplateHandler().GetHpBegin(Level);
            return (int) (hpBegin + (hpBegin * 58 / 100));
        }
        
        /// <summary>
        /// maxMp = maxMp + (maxMp * MEN bonus / 100)
        /// </summary>
        /// <returns></returns>
        public int GetMaxMp()
        {
            var mpBegin = _playerInstance.TemplateHandler().GetMpBegin(Level);
            return (int) (mpBegin + (mpBegin * 28 / 100));
        }
    }
}