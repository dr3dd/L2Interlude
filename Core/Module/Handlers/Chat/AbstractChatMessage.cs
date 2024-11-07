using Core.Enums;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using L2Logger;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:15:26

namespace Core.Module.Handlers.Chat
{
    public abstract class AbstractChatMessage
    {
        internal async Task Chatting(PlayerInstance player, ChatType chatType, string text, string paramsValue)
        {
            LoggerManager.Debug($"{GetType().Name}: login {player.Controller.AccountName} text {text}");


            await Chat(player, chatType, text, paramsValue);
        }
        protected internal abstract Task Chat(PlayerInstance player, ChatType chatType, string text, string paramsValue);

        public async Task<bool> commonChecksChat(PlayerInstance player, ChatType chatType, string text, string paramsValue)
        {
            bool allow = true;

            if (text == "жопа") //TODO config
            {
                await player.SendPacketAsync(new SystemMessage(SystemMessageId.ChattingIsCurrentlyProhibited));
                allow = false;
            }

            return allow;
        }
    }
}
