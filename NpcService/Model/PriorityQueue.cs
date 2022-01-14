using System.Collections.Generic;
using System.Linq;

namespace NpcService.Model
{
    public sealed class PriorityQueue<TEntry> where TEntry : IPrioritizable
    {
        private LinkedList<TEntry> Entries { get; } = new();

        public int Count()
        {
            return Entries.Count;
        }

        public TEntry Dequeue()
        {
            if (Entries.Any())
            {
                var itemTobeRemoved = Entries.First.Value;
                Entries.RemoveFirst();
                return itemTobeRemoved;
            }

            return default(TEntry);
        }

        public void Enqueue(TEntry entry)
        {
            var value = new LinkedListNode<TEntry>(entry);
            if (Entries.First == null)
            {
                Entries.AddFirst(value);
                return;
            }
            var ptr = Entries.First;
            while (ptr.Next != null && ptr.Value.Priority > entry.Priority)
            {
                ptr = ptr.Next;
            }

            if (ptr.Value.Priority >= entry.Priority)
            {
                Entries.AddAfter(ptr, value);
                return;
            }
            Entries.AddBefore(ptr, value);
        }
    }
}