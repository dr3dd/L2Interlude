using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ExCursedWeaponList : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xfe);
            await WriteShortAsync(0x45);
            await WriteIntAsync(0);
        }
    }
}