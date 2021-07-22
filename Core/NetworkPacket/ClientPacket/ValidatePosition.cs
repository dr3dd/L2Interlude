using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class ValidatePosition : PacketBase
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;
        private readonly PlayerInstance _playerInstance;

        private int _data;
        
        public ValidatePosition(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _x = packet.ReadInt();
            _y = packet.ReadInt();
            _z = packet.ReadInt();
            _heading = packet.ReadInt();
            _data = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            await PlayerLocation().ValidatePositionAsync(_x, _y, _z, _heading);
        }
        
        private PlayerMoveToLocation PlayerLocation()
        {
            return _playerInstance.Location();
        }
    }
}