namespace Core.Module.ParserEngine;

public class ParseFString : IParse
{
    private readonly Result _result = new ();

    public void ParseLine(string line)
    {
        var parts = line.Split('\t', 2);
        var id = parts[0];
        var text = parts[1].Trim('[', ']');
        _result.AddItem(id, text);
    }

    public IResult GetResult()
    {
        return _result;
    }
}