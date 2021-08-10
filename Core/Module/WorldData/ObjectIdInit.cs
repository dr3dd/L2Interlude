using System.Threading;

namespace Core.Module.WorldData
{
    public class ObjectIdInit
    {
        private int _currentObjectId = 1;

        public int NextObjectId()
        {
            return Interlocked.Increment(ref _currentObjectId);
        }
    }
}