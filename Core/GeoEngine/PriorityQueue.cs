using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GeoEngine
{
    public class PriorityQueueMy <T> where T : IComparable <T>
    {
        private readonly Queue <T> data;

        public PriorityQueueMy()
        {
            this.data = new Queue <T>();
        }
        
        public int Count()
        {
            return data.Count;
        }

        public bool Contains(T obj)
        {
            return data.Contains(obj);
        }

        public void Add(T item)
        {
            data.Enqueue(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public T Poll()
        {
            return data.Dequeue();
        }
    }
}