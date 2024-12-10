using Core.Module.Player;
using System.Collections.Generic;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 26.11.2024 2:19:14

namespace Core.NetworkPacket.ServerPacket
{
    public class QuestList : Network.ServerPacket
    {
        private readonly PlayerInstance _playerInstance;

        public QuestList(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }
        public override async Task WriteAsync()
        {
            var activeQuests = _playerInstance.PlayerQuest().GetMemoAll();
            await WriteByteAsync(0x80);
            await WriteShortAsync((byte)activeQuests.Count);
            //TODO Add one time quest flags
            foreach (var quest in activeQuests)
            {
                await WriteIntAsync(quest.QuestNo);
                await WriteIntAsync(quest.Journal);
            }
        }
    }
}