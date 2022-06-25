using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class ActionRequest : PacketBase
    {
        private readonly int _objectId;
        private int _originX;
        private int _originY;
        private int _originZ;
        private readonly int _actionId;
        private readonly PlayerInstance _playerInstance;
        private readonly WorldInit _worldInit;
        
        public ActionRequest(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _worldInit = serviceProvider.GetRequiredService<WorldInit>();
            _objectId = packet.ReadInt(); // Target object Identifier
            _originX = packet.ReadInt();
            _originY = packet.ReadInt();
            _originZ = packet.ReadInt();
            _actionId = packet.ReadByte(); // Action identifier : 0-Simple click, 1-Shift click
        }

        public override async Task Execute()
        {
            //var worldObject = GetWorldObject();
            //await _playerInstance.OnActionAsync(worldObject);
            await GetWorldObject().RequestActionAsync(_playerInstance);
        }
        
        private WorldObject GetWorldObject()
        {
            return _worldInit.GetWorldObject(_objectId);
        }
    }
}