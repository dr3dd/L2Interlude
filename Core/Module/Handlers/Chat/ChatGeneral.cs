using Core.Attributes;
using Core.Enums;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:31:36

namespace Core.Module.Handlers.Chat
{
    [ChatType(Type = ChatType.GENERAL)]
    class ChatGeneral : AbstractChatMessage
    {
        protected internal override async Task Chat(PlayerInstance player, ChatType chatType, string text, string paramsValue)
        {

            if (!await commonChecksChat(player, chatType, text, paramsValue))
            {
                return;
            }

            await player.SendPacketAsync(new Say2(player, chatType, text));
            foreach (PlayerInstance targetInstance in Initializer.WorldInit().GetVisiblePlayers(player))
            {

                await targetInstance.SendPacketAsync(new Say2(player, chatType, text));
            }

        }
    }
}
