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


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 23:27:47

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestShortCutReg : PacketBase
    {
        private readonly ShortCutType _type;
        private readonly int _id;
        private readonly int _slot;
        private readonly int _page;
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerShortCut _playerShortCut;

        public RequestShortCutReg(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerShortCut = _playerInstance.PlayerShortCut();

            int typeId = packet.ReadInt();
            _type = (ShortCutType)((typeId < 1) || (typeId > 6) ? 0 : typeId);
            int slot = packet.ReadInt();
            _id = packet.ReadInt();
            _slot = slot % 12;
            _page = slot / 12;
        }

        public override async Task Execute()
        {
            if (_slot < 0 || _slot > 11 || _page < 0 || _page > 10)
            {
                return;
            }

            await _playerShortCut.RegisterShortCut(new ShortCut(_slot, _page, _type, _id));
        }
    }
}
