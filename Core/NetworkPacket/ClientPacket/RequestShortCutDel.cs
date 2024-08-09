using Core.Controller;
using Core.Module.Player.ShortCuts;
using Core.Module.Player;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 1:32:55

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestShortCutDel : PacketBase
    {
        private readonly int _slot;
        private readonly int _page;

        private readonly PlayerInstance _playerInstance;

        public RequestShortCutDel(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            int id = packet.ReadInt();
            _slot = id % 12;
            _page = id / 12;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            if (_slot < 0 || _slot > 11 || _page < 0 || _page > 10)
            {
                return;
            }

            await _playerInstance.PlayerShortCut().DeleteShortCutAsync(_slot, _page);
        }
    }
}
