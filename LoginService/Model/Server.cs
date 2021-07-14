namespace LoginService.Model
{
    public class Server
    {
        public byte ServerId { get; set; }

        public string ServerKey { get; set; }

        public string Name { get; set; }

        public GameServerClient GameServerClient { get; set; }

        public bool Connected => GameServerClient?.Connected ?? false;

        public short CurrentPlayers => GameServerClient?.CurrentPlayers ?? 0;

        public short MaxPlayers => GameServerClient?.MaxPlayers ?? 0;

        public int Port => GameServerClient?.Port ?? 0;

        public bool TestMode => GameServerClient?.TestMode ?? false;

        public bool GmOnly => GameServerClient?.GmOnly ?? false;

        private byte[] DefaultAddress { get; set; }

        public byte[] GetIp(LoginClient client)
        {
            if (DefaultAddress == null)
            {
                string ip = GameServerClient?.Wan ?? "0.0.0.0";

                DefaultAddress = new byte[4];
                string[] w = ip.Split('.');
                DefaultAddress[0] = byte.Parse(w[0]);
                DefaultAddress[1] = byte.Parse(w[1]);
                DefaultAddress[2] = byte.Parse(w[2]);
                DefaultAddress[3] = byte.Parse(w[3]);
            }

            if (GameServerClient == null)
                return DefaultAddress;

            return DefaultAddress;
        }
    }
}
