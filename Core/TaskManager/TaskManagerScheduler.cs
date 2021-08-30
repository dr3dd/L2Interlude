using System;
using System.Threading;
using System.Threading.Tasks;

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
    }
}