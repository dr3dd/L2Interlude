using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
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
        private readonly List<PlayerInstance> _movingObjects;

        public GameTimeController()
        {
            _movingObjects = new List<PlayerInstance>();
            MillisInTick = 1000 / TicksPerSecond;
        }

        public void Run()
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
                
                if (oldTicks != GameTicks)
                {
                    MoveObjects();
                }
        
                //LoggerManager.Info("TICK:" + GameTicks);
            }
        }
        
        private void MoveObjects()
        {
            try
            {
                PlayerInstance[] PlayerInstances = _movingObjects.ToArray();
                // Create an ArrayList to contain all Creature that are arrived to destination
                List<PlayerInstance> ended = null;

                foreach (var l2Character in PlayerInstances)
                {
                    if (l2Character is null)
                        continue;
                    // Update the position of the Creature and return True if the movement is finished
                    bool end = l2Character.PlayerMovement().UpdatePosition(GameTicks);
                    // If movement is finished, the Creature is removed from movingObjects and added to the ArrayList ended
                    if (end)
                    {
                        _movingObjects.Remove(l2Character);
                        if (ended == null)
                        {
                            ended = new List<PlayerInstance>();
                        }
                        ended.Add(l2Character);
                    }
                }

                if (ended != null)
                {
                    foreach (PlayerInstance playerInstance in ended)
                    {
                        playerInstance.UpdateKnownObjects();
                        //character.AI.NotifyEvent(CtrlEvent.EvtArrived);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(GetType().Name + ": " + ex.Message);
            }
        }
        
        public void RegisterMovingObject(PlayerInstance playerInstance)
        {
            try
            {
                if (!_movingObjects.Contains(playerInstance))
                {
                    _movingObjects.Add(playerInstance);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(ex.Message);
            }
        }
    }
}