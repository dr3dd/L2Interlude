using System.Threading.Tasks;
using Network;

namespace LoginService.Network.ServerPackets
{
    internal class GgAuth : ServerPacket
    {
        private const byte SkipGameGuardAuthRequest = 0x0b;
        private readonly int _sessionId;
        public GgAuth(int sessionId)
        {
            _sessionId = sessionId;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(SkipGameGuardAuthRequest);
            await WriteIntAsync(_sessionId);
            await WriteBytesArrayAsync(new byte[4]);
        }
    }
}
