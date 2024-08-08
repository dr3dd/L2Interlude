using Config;

namespace Core.NetworkPacket.ServerPacket.LoginServicePacket
{
    public class LoginAuth : Network.ServerPacket
    {
        private GameConfig _gameConfig;

        public LoginAuth(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public override void Write()
        {
            WriteByte(0xA1);
            WriteShort(_gameConfig.ServerConfig.ServerPort);
            WriteString(_gameConfig.ServerConfig.ServerHost);
            WriteString(string.Empty);
            WriteString(_gameConfig.ServerConfig.AuthKey);
            WriteInt(0);
            WriteShort(100); //max players
            WriteByte(0x00); //only gm or not
            WriteByte(0x00); //test or not
        }
    }
}