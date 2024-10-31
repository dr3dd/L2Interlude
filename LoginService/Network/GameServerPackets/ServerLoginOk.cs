using System.Threading.Tasks;
using Network;

namespace LoginService.Network.GameServerPackets
{
    public class ServerLoginOk : ServerPacket
    {
        private const byte Opcode = 0xA6;

        public override async Task WriteAsync()
        {
            await WriteByteAsync(Opcode);
            await WriteStringAsync("Gameserver Authenticated");
        }
    }
}
