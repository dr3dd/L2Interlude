using Config;
using Core.Controller;
using Core.Enums;
using Core.Module.Handlers;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 11.08.2024 0:00:56

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestSay : PacketBase
    {
        private string text;
        private int type;
        private string target;

        private readonly ChatHandler _chatHandler;
        private readonly PlayerInstance _playerInstance;

        public RequestSay(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _chatHandler = serviceProvider.GetService<ChatHandler>();
            text = packet.ReadString();
            text = text.Replace("\n", "");
            try
            {
                type = packet.ReadInt();
            }
            catch (Exception e)
            {
                type = 0;
            }
            target = type == (int)ChatType.WHISPER ? packet.ReadString() : null;

        }

        public override async Task Execute()
        {
            ChatType chatType = (ChatType)type;

            if (chatType == null)
            {
                LoggerManager.Warn($"Say2C: Invalid type: {type} Player : {_playerInstance.CharacterName} text: {text}");
                await _playerInstance.SendPacketAsync(new ActionFailed());
                return;
            }

            if (text.Equals(string.Empty))
            {
                LoggerManager.Warn($"SayClient: Player : {_playerInstance.CharacterName} sending empty text. Possible packet hack!");
                await _playerInstance.SendPacketAsync(new ActionFailed());
                return;
            }

            //TODO more condition

            _chatHandler.Chat(_playerInstance, chatType, target, text);
        }
    }
}
