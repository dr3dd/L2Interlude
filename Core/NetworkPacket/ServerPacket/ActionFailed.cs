using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class ActionFailed : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x25);
        }
    }
}