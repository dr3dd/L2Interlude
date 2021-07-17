using System.Threading.Tasks;
using Helpers;
using L2Logger;

namespace Core.Controller
{
    public class GameTimeController
    {
       
        public int TicksPerSecond { get; } = 10;
        public int MillisInTick { get; }

        public int GameTicks { get; private set; }
        private long GameStartTime { get; set; }
        public bool IsNight { get; set; } = false;


        public GameTimeController()
        {
            MillisInTick = 1000 / TicksPerSecond;
        }

        public void Init()
        {
            GameStartTime = DateTimeHelper.CurrentUnixTimeMillis() - 3600000; // offset so that the server starts a day begin
            GameTicks = 3600000 / MillisInTick;

            Task.Run(async () =>
            {
                await RunTimer();
                await BroadcastSunState();
            });
        }
        
        private async Task BroadcastSunState()
        {
            using var timer = new TaskTimer(600000).Start();
            foreach (var task in timer)
            {
                await task;
                int h = (GetGameTime() / 60) % 24; // Time in hour
                bool tempIsNight = h < 6;
                
                // If diff day/night state
                if (tempIsNight == IsNight) return;
                // Set current day/night variable to value of temp variable
                IsNight = tempIsNight;

                LoggerManager.Info("Notify Change day/night ");
                //DayNightSpawnManager.getInstance().notifyChangeMode(); TODO
            }
        }
        
        public int GetGameTime()
        {
            return GameTicks / (TicksPerSecond * 10);
        }

        public int GetGameTicks()
        {
            return GameTicks;
        }
        
        private async Task RunTimer()
        {
            using var timer = new TaskTimer(100).Start();
            foreach (var task in timer)
            {
                await task;

                int oldTicks = GameTicks; // save old ticks value to avoid moving objects 2x in same tick
                long runtime = DateTimeHelper.CurrentUnixTimeMillis() - GameStartTime; // from server boot to now
        
                GameTicks = (int) (runtime / MillisInTick); // new ticks value (ticks now)
        
                //LoggerManager.Info("TICK:" + GameTicks);
            }
        }
    }
}