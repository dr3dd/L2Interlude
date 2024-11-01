using System.Threading.Tasks;
using Core.Controller;

namespace Core.NetworkPacket.ServerPacket
{
    class KeyPacket : Network.ServerPacket
    {
        private readonly byte[] _key;
        private byte _next;

        public KeyPacket(GameServiceController controller, byte n)
        {
            _key = controller.EnableCrypt();
            _next = n;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x00);
            await WriteByteAsync(0x01);
            await WriteBytesArrayAsync(_key);
            await WriteIntAsync(0x01);
            await WriteIntAsync(0x01);
        }
    }
}