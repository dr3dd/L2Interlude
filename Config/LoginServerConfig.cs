namespace Config
{
    public class LoginServerConfig
    {
        public const string LoginServiceConfig = "LoginServiceConfig";

        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public int GameServerPort { get; set; }
        public bool AutoCreateAccount { get; set; }
        public string DataBaseHost { get; set; }
        public int DataBasePort { get; set; }
        public string DataBaseUser { get; set; }
        public string DataBasePassword { get; set; }
        public string DataBaseName { get; set; }
    }
}