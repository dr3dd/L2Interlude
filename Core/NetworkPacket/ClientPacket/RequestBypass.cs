using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestBypass : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        private readonly string _command;
        private readonly WorldInit _worldInit;
        
        public RequestBypass(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _worldInit = serviceProvider.GetRequiredService<WorldInit>();
            _command = packet.ReadString();
        }

        public override async Task Execute()
        {
            var split = _command.Split("#");
            var command = split.First();
            switch (command)
            {
                case "teleport_request":
                {
                    var npcObjectId = Convert.ToInt32(split.Last());
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.TeleportRequest(_playerInstance);
                    break;
                }
                case "teleport_goto":
                {
                    var parseNpc = split[1].Split("?");
                    var npcObjectId = Convert.ToInt32(parseNpc.First());
                    var teleportId = Convert.ToInt32(parseNpc.Last().Split("=")[1]);
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.TeleportToLocation(teleportId, _playerInstance);
                    break;
                }
            }

            var id = _command;
            
        }
        
        private NpcInstance GetNpcInstance(int objectId)
        {
            return _worldInit.GetNpcInstance(objectId);
        }
    }
}