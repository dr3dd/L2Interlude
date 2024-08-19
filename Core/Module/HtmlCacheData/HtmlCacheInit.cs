using System;
using System.IO;
using Config;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.HtmlCacheData
{
    public class HtmlCacheInit
    {
        private readonly string _basePath;
        
        public HtmlCacheInit(IServiceProvider provider)
        {
            _basePath = provider.GetRequiredService<GameConfig>().ServerConfig.StaticData + "/html";
        }

        public void Run()
        {
            LoggerManager.Info("[TODO] Html cache start...");
        }
        
        private string LoadFile(FileInfo fileInfo)
        {
            var content = "";
            try
            {
                using var sr = new StreamReader(fileInfo.FullName);
                content = new string(sr.ReadToEnd());
                return content;
            }
            catch (FileNotFoundException e)
            {
                LoggerManager.Error(e.Message);
            }

            return content;
        }

        public string GetHtmlText(string fnHi)
        {
            var path = _basePath + "/" + fnHi;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                return LoadFile(new FileInfo(path));
            }
            return $"<html>NOT FOUND: {fnHi}</html>";
        }
    }
}