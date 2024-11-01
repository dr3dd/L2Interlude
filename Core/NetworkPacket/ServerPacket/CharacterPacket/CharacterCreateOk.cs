using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    internal sealed class CharacterCreateOk : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x19);
            await WriteIntAsync(0x01);
        }
    }
}