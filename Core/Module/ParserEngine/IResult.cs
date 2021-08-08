using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public interface IResult
    {
        IDictionary<object, object> GetResult();
        void AddItem(object name, object value);
    }
}