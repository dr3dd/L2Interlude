using System.Threading.Tasks;
using Network;

namespace LoginService.Network.GameServerPackets
{
    internal class LoginServer : ServerPacket
    {
        private const byte Opcode = 0xA1;
        private readonly int _key;
        public LoginServer(int randomKey)
        {
            _key = randomKey;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(Opcode);
            await WriteIntAsync(_key);
        }
    }
}
