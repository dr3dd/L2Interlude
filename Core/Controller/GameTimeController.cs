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
        public bool IsNight { get; set; }
        private readonly List<Character> _movingObjects;

        public GameTimeController()
        {
            _movingObjects = new List<Character>();
            MillisInTick = 1000 / TicksPerSecond;
        }

        public void Run()
        {
            GameStartTime = DateTimeHelper.GetCurrentUnixTimeMillis() - 3600000; // offset so that the server starts a day begin
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
                long runtime = DateTimeHelper.GetCurrentUnixTimeMillis() - GameStartTime; // from server boot to now
        
                GameTicks = (int) (runtime / MillisInTick); // new ticks value (ticks now)
                
                if (oldTicks != GameTicks)
                {
                    await MoveObjects();
                }
        
                //LoggerManager.Info("TICK:" + GameTicks);
            }
        }
        
        private async Task MoveObjects()
        {
            List<Character> ended = new List<Character>();
            try
            {
                // Create an ArrayList to contain all Characters that are arrived to destination
                foreach (var character in _movingObjects.ToArray())
                {
                    if (character is null)
                        continue;
                    // Update the position of the Character and return True if the movement is finished
                    var end = await character.CharacterMovement().UpdatePosition();
                    // If movement is finished, the Character is removed from movingObjects and added to the ArrayList ended
                    if (end)
                    {
                        ended.Add(character);
                    }
                    await character.UpdateKnownObjects();
                    if (character is PlayerInstance playerInstance)
                    {
                        await playerInstance.FindCloseNpc();
                        await playerInstance.FindCloseDoor();
                    }
                    await character.RemoveKnownObjects();
                }

                _movingObjects.RemoveAll(c => ended.Contains(c));
                foreach (var character in ended)
                {
                    character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtArrived);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(GetType().Name + ": " + ex.Message);
                throw;
            }
        }
        
        public void RegisterMovingObject(Character character)
        {
            try
            {
                if (!_movingObjects.Contains(character))
                {
                    _movingObjects.Add(character);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(ex.Message);
            }
        }
    }
}