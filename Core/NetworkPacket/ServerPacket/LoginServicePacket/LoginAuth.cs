using System.Threading.Tasks;
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

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xA1);
            await WriteShortAsync(_gameConfig.ServerConfig.ServerPort);
            await WriteStringAsync(_gameConfig.ServerConfig.ServerHost);
            await WriteStringAsync(string.Empty);
            await WriteStringAsync(_gameConfig.ServerConfig.AuthKey);
            await WriteIntAsync(0);
            await WriteShortAsync(100); //max players
            await WriteByteAsync(0x00); //only gm or not
            await WriteByteAsync(0x00); //test or not
        }
    }
}