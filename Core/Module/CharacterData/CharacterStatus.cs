using System.Threading;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;

namespace Core.Module.CharacterData
{
    public class CharacterStatus
    {
        private readonly Character _character;
        private double _currentHp;
        private Task _regTask;
        private CancellationTokenSource _cts;
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
                    StopHpMpRegeneration();
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
                StartHpMpRegeneration();
                SendStatusUpdate();
            }
        }

        private void SendStatusUpdate()
        {
            if (_character is PlayerInstance playerInstance)
            {
                var su = new StatusUpdate(playerInstance);
                playerInstance.SendPacketAsync(su);
            }
        }

        private void SetCurrentHp(double newHp)
        {
            CurrentHp = newHp;
        }

        public void StartHpMpRegeneration()
        {
            // Get the Regeneration period
            var period = 3000;
            if (_regTask != null) return;
            _cts = new CancellationTokenSource();
            _regTask = TaskManagerScheduler.ScheduleAtFixedRate(() =>
            {
                lock (this)
                {
                    IncreaseCurrentHp(_character.GetHpRegenRate());
                    SendStatusUpdate();
                }
            }, period, period, _cts.Token);
        }
        
        public void StopHpMpRegeneration()
        {
            if (_regTask == null) return;
            // Stop the HP/MP/CP Regeneration task
            if (!_regTask.IsCanceled)
            {
                _cts.Cancel();
                _regTask = null;
            }
        }
    }
}