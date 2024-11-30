using Core.Controller;
using Core.Module.Player.ShortCuts;
using Core.Module.Player;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Logger;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 1:32:55

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestShortCutDel : PacketBase
    {
        private readonly int _slotNum;

        private readonly PlayerInstance _playerInstance;

        public RequestShortCutDel(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _slotNum = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            if (_slotNum > 143)
            {
                LoggerManager.Warn($"RequestShortCutDel >143 char:{_playerInstance.CharacterName}");
                return;
            }

            await _playerInstance.PlayerShortCut().DeleteShortCutAsync(_slotNum);
        }
    }
}
