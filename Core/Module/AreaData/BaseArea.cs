using System;
using System.Collections.Concurrent;
using Core.Module.CharacterData;
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
        private readonly ConcurrentDictionary<int, Character> _characterList;

        protected BaseArea(string name, Type area)
        {
            Name = name;
            Area = area;
            _characterList = new ConcurrentDictionary<int, Character>();
        }
        
        public bool IsInsideZone(int x, int y, int z)
        {
            return Zone.IsInsideZone(x, y, z);
        }
        
        public double GetDistanceToZone(int x, int y)
        {
            return Zone.GetDistanceToZone(x, y);
        }
        
        protected abstract void OnEnter(Character character);
        protected abstract void OnExit(Character character);

        public void RevalidateInZone(Character character)
        {
            // If the object is inside the zone...
            if (Zone.IsInsideZone(character.GetX(), character.GetY(), character.GetZ()))
            {
                if (!_characterList.ContainsKey(character.ObjectId))
                {
                    // Was the character not yet inside this zone?
                    _characterList.TryAdd(character.ObjectId, character);
                    OnEnter(character);
                }
            }
            // Was the character inside this zone?
            else if (_characterList.ContainsKey(character.ObjectId))
            {
                _characterList.TryRemove(character.ObjectId, out _);
                OnExit(character);
            }
        }
        
        public void RemoveCharacter(Character character)
        {
            if (_characterList.TryRemove(character.ObjectId, out _))
            {
                OnExit(character);
            }
        }
    }
}