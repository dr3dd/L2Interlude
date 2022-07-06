using System;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using L2Logger;

namespace Core.TaskManager
{
    public static class TaskManagerScheduler
    {
        public static Task ScheduleAtFixed(Action action, int delay, CancellationToken token)
        {
            return Task.Run( async () =>
            {
                await Task.Delay(delay, token);
                action.Invoke();
            }, token);
        }
        
        public static Task ScheduleAtFixedRate(Action action, int delay, int period, CancellationToken token)
        {
            try
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        using var timer = new TaskTimer(period).CancelWith(token).Start(delay);
                        foreach (var task in timer)
                        {
                            await task;
                            action.Invoke();
                        }
                    }
                    catch (TaskCanceledException)
                    {

                    }
                }, token);
            }
            catch (Exception ex)
            {
                LoggerManager.Error("ScheduleAtFixedRate: " + ex.Message);
                throw;
            }
        }
        
        public static void Schedule(Action action, int delay)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                action.Invoke();
            });
        }
    }
}