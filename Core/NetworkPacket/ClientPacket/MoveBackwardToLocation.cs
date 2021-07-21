using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class MoveBackwardToLocation : PacketBase
    {
        private readonly int _targetX;
        private readonly int _targetY;
        private readonly int _targetZ;
        private readonly int _originX;
        private readonly int _originY;
        private readonly int _originZ;
        private readonly int _movementMode;
        private readonly PlayerInstance _playerInstance;

        public MoveBackwardToLocation(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _targetX = packet.ReadInt();
            _targetY = packet.ReadInt();
            _targetZ = packet.ReadInt();
            _originX = packet.ReadInt();
            _originY = packet.ReadInt();
            _originZ = packet.ReadInt();
            _movementMode = packet.ReadInt(); // is 0 if cursor keys are used 1 if mouse is used
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }
        

        public override async Task Execute()
        {
            await PlayerLocation().MoveToLocationAsync(
                _targetX, 
                _targetY, 
                _targetZ, 
                _originX, 
                _originY, 
                _originZ);
        }

        private PlayerMoveToLocation PlayerLocation()
        {
            return _playerInstance.Location();
        }
    }
}