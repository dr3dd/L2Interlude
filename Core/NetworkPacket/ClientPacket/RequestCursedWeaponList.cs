using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestCursedWeaponList : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        
        public RequestCursedWeaponList(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            await _playerInstance.SendPacketAsync(new ExCursedWeaponList());
        }
    }
}