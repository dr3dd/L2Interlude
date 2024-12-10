using Core.Controller;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Core.NetworkPacket.ServerPacket;
using Network;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.Module.Player.ShortCuts;
using L2Logger;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 23:27:47

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestShortCutReg : PacketBase
    {
        private readonly ShortCutType _type;
        private readonly int _id;
        private readonly int _slotNum;
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerShortCut _playerShortCut;

        public RequestShortCutReg(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerShortCut = _playerInstance.PlayerShortCut();

            int typeId = packet.ReadInt();
            _type = (ShortCutType)((typeId < 1) || (typeId > 6) ? 0 : typeId);
            _slotNum = packet.ReadInt();
            _id = packet.ReadInt();
        }

        public override async Task Execute()
        {
            if ( _slotNum > 143)
            {
                LoggerManager.Warn($"RequestShortCutReg >143 char:{_playerInstance.CharacterName}");
                return;
            }

            await _playerShortCut.RegisterShortCut(new ShortCut(_slotNum, _type, _id));
        }
    }
}
