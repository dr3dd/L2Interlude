using System;
using System.Collections.Concurrent;
using Core.Module.Player;

namespace Core.Module.AreaData
{
    public abstract class BaseArea
    {
        protected string Name { get; }
        protected Type Area { get; }
        protected byte MapNoX { get; }
        protected byte MapNoY { get; }
        
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MinZ { get; set; }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MaxZ { get; set; }

        public int[] X { get; set; }
        public int[] Y { get; set; }

        public ZoneForm Zone { get; set; }
        private readonly ConcurrentDictionary<int, PlayerInstance> _characterList;

        protected BaseArea(string name, Type area)
        {
            Name = name;
            Area = area;
            _characterList = new ConcurrentDictionary<int, PlayerInstance>();
        }
        
        public bool IsInsideZone(int x, int y, int z)
        {
            return Zone.IsInsideZone(x, y, z);
        }
        
        public double GetDistanceToZone(int x, int y)
        {
            return Zone.GetDistanceToZone(x, y);
        }
        
        protected abstract void OnEnter(PlayerInstance character);
        protected abstract void OnExit(PlayerInstance character);

        public void RevalidateInZone(PlayerInstance playerInstance)
        {
            // If the object is inside the zone...
            if (Zone.IsInsideZone(playerInstance.GetX(), playerInstance.GetY(), playerInstance.GetZ()))
            {
                if (!_characterList.ContainsKey(playerInstance.ObjectId))
                {
                    // Was the character not yet inside this zone?
                    _characterList.TryAdd(playerInstance.ObjectId, playerInstance);
                    OnEnter(playerInstance);
                }
            }
            // Was the character inside this zone?
            else if (_characterList.ContainsKey(playerInstance.ObjectId))
            {
                _characterList.TryRemove(playerInstance.ObjectId, out _);
                OnExit(playerInstance);
            }
        }
    }
}