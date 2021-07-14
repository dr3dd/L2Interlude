namespace Config
{
    public class ServerConfig
    {
        public const string GameServiceSection = "GameServiceSection";

        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public string LoginServiceHost { get; set; }
        public int LoginServicePort { get; set; }
        public string AuthKey { get; set; }
        public string StaticData { get; set; }
        public string RedisConnection { get; set; } 
    }
}