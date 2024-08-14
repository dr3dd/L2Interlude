namespace Config
{
    public class LoginServerConfig
    {
        public const string LoginServiceConfig = "LoginServiceConfig";

        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public int GameServerPort { get; set; }
        public bool AutoCreateAccount { get; set; }
    }
}