using System.Threading;
using System.Threading.Tasks;

namespace Core.Module.CharacterData
{
    public class CharacterStatus
    {
        private readonly Character _character;
        private CancellationTokenSource _cancellationTokenSource;
        private Timer _regenerationTimer;
        private readonly SemaphoreSlim _semaphore;
        public CharacterStatus(Character character)
        {
            _character = character;
            _semaphore = new SemaphoreSlim(1);
        }
        
        public float CurrentMp { get; set; }
        
        public double CurrentHp { get; set; }
        
        /// <summary>
        /// Increase Current Hp of Character
        /// </summary>
        /// <param name="heal"></param>
        public void IncreaseCurrentHp(double heal)
        {
            CurrentHp += heal;
            if (CurrentHp >= _character.GetMaxHp())
            {
                CurrentHp = _character.GetMaxHp();
                StopHpMpRegeneration();
            }
        }
        
        /// <summary>
        /// Decrease Current Hp of Character
        /// </summary>
        /// <param name="damage"></param>
        public void DecreaseCurrentHp(double damage)
        {
            if (damage <= 0) return;
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
            }
            StartHpMpRegeneration();
        }

        private void StartHpMpRegeneration()
        {
            // Get the Regeneration period
            var period = 3300;
            var start = 0;
            if (_regenerationTimer != null) return;
            _cancellationTokenSource = new CancellationTokenSource();
            _regenerationTimer = new Timer( _ => HpMpRegenerationCallback(), null, start,
                period);
        }
        

        /// <summary>
        /// 
        /// </summary>
        private void HpMpRegenerationCallback()
        {
            _semaphore.Wait();
            try
            {
                CurrentHp += _character.GetHpRegenRate();
                if (CurrentHp >= _character.GetMaxHp())
                {
                    CurrentHp = _character.GetMaxHp();
                    StopHpMpRegeneration();
                }
                Task.Run(() => _character.SendStatusUpdate());
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private void StopHpMpRegeneration()
        {
            if (_regenerationTimer == null) return;
            _regenerationTimer.Dispose();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _regenerationTimer = null;
        }
    }
}