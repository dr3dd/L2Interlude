using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    public class CharTemplates : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x17);
        }
    }
}