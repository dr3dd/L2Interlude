namespace Core.Module.ParserEngine
{
    public interface IParse
    {
        void ParseLine(string line);
        IResult GetResult();
    }
}