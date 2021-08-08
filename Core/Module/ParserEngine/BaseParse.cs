using System;
using System.IO;
using Config;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.ParserEngine
{
    public abstract class BaseParse
    {
        private readonly string _basePath;
        public abstract void Run();

        protected BaseParse(IServiceProvider provider)
        {
            _basePath = provider.GetRequiredService<GameConfig>().ServerConfig.StaticData;
        }

        protected string GetStaticData() => _basePath;
        
        protected IResult Parse(string file, IParse parse)
        {
            using StreamReader sr = new StreamReader(GetStaticData() + "/" + file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                parse.ParseLine(line);
            }
            return parse.GetResult();
        }
    }
}