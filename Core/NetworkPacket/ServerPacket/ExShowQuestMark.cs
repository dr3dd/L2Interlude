using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 21.11.2024 9:21:13

namespace Core.NetworkPacket.ServerPacket
{
    public class ExShowQuestMark : Network.ServerPacket
    {
        private readonly int _quest_id;

        public ExShowQuestMark(int quest_id)
        {
            _quest_id = quest_id;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xFE);
            await WriteShortAsync(0x1A);
            await WriteIntAsync(_quest_id);
        }
    }
}