using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player.Response;
using Network;

namespace Core.Module.Player.Request
{
    public class EnterWorld : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        
        public EnterWorld(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            await _playerInstance.SendActionFailedPacketAsync();
            await _playerInstance.SendPacketAsync(new EtcStatusUpdate(_playerInstance));
            await _playerInstance.SendPacketAsync(new UserInfo(_playerInstance));
            await _playerInstance.SendPacketAsync(new ClientSetTime()); // SetClientTime
            //await _playerInstance.SendPacketAsync(new ItemList(_playerInstance, false));
            //await _playerInstance.SendPacketAsync(new ShortCutInit(_playerInstance)); // SetClientTime
            await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.WelcomeToLineage));
        }
    }
}