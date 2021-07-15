using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public class Result : IResult
    {
        private readonly IDictionary<string, object> _items;

        public Result()
        {
            _items = new Dictionary<string, object>();
        }
        
        public IDictionary<string, object> GetResult()
        {
            return _items;
        }

        public void AddItem(string name, object value)
        {
            _items.TryAdd(name, value);
        }
    }
}