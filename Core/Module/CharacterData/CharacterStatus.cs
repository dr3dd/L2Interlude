namespace Core.Module.CharacterData
{
    public class CharacterStatus
    {
        private readonly Character _character;
        private double _currentHp;
        public CharacterStatus(Character character)
        {
            _character = character;
        }
        
        public float CurrentMp { get; set; }
        
        public double CurrentHp
        {
            get
            {
                lock (this)
                {
                    if (_currentHp >= _character.GetMaxHp())
                    {
                        return _character.GetMaxHp();
                    }
                }
                return _currentHp;
            }
            set => _currentHp = value;
        }
        
        /// <summary>
        /// Increase Current Hp of Character
        /// </summary>
        /// <param name="heal"></param>
        public void IncreaseCurrentHp(double heal)
        {
            lock (this)
            {
                CurrentHp += heal; // Get diff of Hp vs value
                if (CurrentHp >= _character.GetMaxHp())
                {
                    CurrentHp = _character.GetMaxHp();
                }
                SetCurrentHp(CurrentHp); // Set Hp
            }
        }
        
        /// <summary>
        /// Decrease Current Hp of Character
        /// </summary>
        /// <param name="damage"></param>
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
        
        private void SetCurrentHp(double newHp)
        {
            CurrentHp = newHp;
        }
    }
}