using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public interface IResult
    {
        IDictionary<string, object> GetResult();
        void AddItem(string name, object value);
    }
}