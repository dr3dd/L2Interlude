using System.Threading.Tasks;
using Network;

namespace LoginService.Network.ServerPackets
{
    internal class PlayOk : ServerPacket
    {
        private const byte Opcode = 0x07;
        private readonly LoginClient _client;

        public PlayOk(LoginClient client)
        {
            _client = client;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(Opcode);
            await WriteIntAsync(_client.SessionKey.PlayOkId1);
            await WriteIntAsync(_client.SessionKey.PlayOkId2);
        }
    }
}
