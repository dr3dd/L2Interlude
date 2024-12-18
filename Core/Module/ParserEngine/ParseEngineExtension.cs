using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine
{
    public static class ParseEngineExtension
    {
        public static string RemoveBrackets(this string str)
        {
            return Regex.Replace(str, @"\{|\}|\[|\]", "");
        }

        public static string RemoveSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }
    }
}