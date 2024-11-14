using System.Collections.Generic;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 14.11.2024 13:44:22

namespace Core.NetworkPacket.ServerPacket
{
    public class ExShowQuestInfo : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xFE);
            await WriteShortAsync(0x19);
        }
    }
}