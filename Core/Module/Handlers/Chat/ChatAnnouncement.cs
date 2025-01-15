using Core.Attributes;
using Core.Enums;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.01.2025 1:23:45

namespace Core.Module.Handlers.Chat
{
    [ChatType(Type = ChatType.ANNOUNCEMENT)]
    class ChatAnnouncement : AbstractChatMessage
    {
        protected internal override async Task Chat(PlayerInstance player, ChatType chatType, string text, string paramsValue)
        {

            foreach (PlayerInstance targetInstance in Initializer.WorldInit().GetAllPlayerInstance())
            {

                await targetInstance.SendPacketAsync(new Say2(null, chatType, text));
            }

        }
    }
}
