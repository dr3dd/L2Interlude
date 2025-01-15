using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;
using Network;

namespace Core.NetworkPacket.ClientPacket
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
            try
            {
                await _playerInstance.SendActionFailedPacketAsync();
                _playerInstance.SpawnMe(_playerInstance.GetX(), _playerInstance.GetY(), _playerInstance.GetZ());
                await _playerInstance.SendToKnownPlayers(new CharInfo(_playerInstance));
                await _playerInstance.UpdateKnownObjects();
                await _playerInstance.SendPacketAsync(new EtcStatusUpdate(_playerInstance));
                await _playerInstance.PlayerQuest().SendQuestList();
                await _playerInstance.SendPacketAsync(new UserInfo(_playerInstance));
                await _playerInstance.SendPacketAsync(new ClientSetTime()); // SetClientTime
                await _playerInstance.SendPacketAsync(new ItemList(_playerInstance, false));
                await _playerInstance.PlayerMacros().SendAllMacros();
                await _playerInstance.SendPacketAsync(new ShortCutInit(_playerInstance));
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.WelcomeToLineage));
                await Initializer.AnnounceManager().ShowLoginAnnounces(_playerInstance);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            
        }
    }
}