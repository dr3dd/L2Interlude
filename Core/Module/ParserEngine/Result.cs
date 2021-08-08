using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public class Result : IResult
    {
        private readonly IDictionary<object, object> _items;

        public Result()
        {
            _items = new Dictionary<object, object>();
        }
        
        public IDictionary<object, object> GetResult()
        {
            return _items;
        }

        public void AddItem(object name, object value)
        {
            _items.TryAdd(name, value);
        }
    }
}