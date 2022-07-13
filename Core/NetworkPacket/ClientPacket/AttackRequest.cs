using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class AttackRequest : PacketBase
    {
        private readonly int _objectId;
        private int _originX;
        private int _originY;
        private int _originZ;
        private int _attackId;
        private readonly PlayerInstance _playerInstance;
        private readonly WorldInit _worldInit;
        
        public AttackRequest(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _objectId = packet.ReadInt();
            _originX = packet.ReadInt();
            _originY = packet.ReadInt();
            _originZ = packet.ReadInt();
            _attackId = packet.ReadByte(); // 0 for simple click - 1 for shift-click
            
            _worldInit = serviceProvider.GetRequiredService<WorldInit>();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            WorldObject target;
            if (_playerInstance.CharacterTargetAction().GetTarget().ObjectId == _objectId)
            {
                target = _playerInstance.CharacterTargetAction().GetTarget();
            }
            else
            {
                target = GetWorldObject();
            }

            if (_playerInstance.CharacterTargetAction().GetTarget().ObjectId == _playerInstance.ObjectId)
            {
                await _playerInstance.SendActionFailedPacketAsync();
            }

            await target.RequestForcedAttack(_playerInstance);
        }
        
        private WorldObject GetWorldObject()
        {
            return _worldInit.GetWorldObject(_objectId);
        }
    }
}