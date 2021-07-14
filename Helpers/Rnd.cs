using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public static class Rnd
    {
        private static readonly Random _global = new Random();
        
        [ThreadStatic] 
        private static Random _local;

        public static int Next()
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next();
        }

        public static int Next(int maxInt)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next(maxInt);
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next(maxInt);
        }
        
        public static int Next(int origin, int bound)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next(origin);
                        _local = new Random(seed);
                    }
                }
            }

            return origin + (int) (((bound - origin) + 1) * _local.Next(origin));
        }

        public static void NextBytes(byte[] buffer)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        _global.NextBytes(buffer);
                        _local = new Random();
                    }
                }
            }
            _local.NextBytes(buffer);
        }
        
        public static double NextDouble()
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        _global.NextDouble();
                        _local = new Random();
                    }
                }
            }
            return _local.NextDouble();
        }

        public static IEnumerable<T> Get<T>(List<T> list)
        {
            if ((list == null) || (list.Count == 0))
                return null;

            return list.Shuffle().Take(list.Count);
        }
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}
