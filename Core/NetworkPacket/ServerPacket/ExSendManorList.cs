using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class ExSendManorList : Network.ServerPacket
    {
        private readonly List<string> _list;

        public ExSendManorList(List<string> list)
        {
            _list = list;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xFE);
            await WriteShortAsync(0x1B);
            await WriteIntAsync(_list.Count);

            int id = 1;
            foreach (string manor in _list)
            {
                await WriteIntAsync(id);
                id++;
                await WriteStringAsync(manor);
            }
        }
    }
}