using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestRestart : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly PlayerInstance _playerInstance;
        
        public RequestRestart(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _playerInstance = _controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            await _playerInstance.SendToKnownPlayers(new DeleteObject(_playerInstance.ObjectId));
            await _playerInstance.DeleteMeAsync();
            await _playerInstance.PlayerModel().CharacterStoreAsync();
            await _playerInstance.SendPacketAsync(new RestartResponse(true));
            await _controller.SendPacketAsync(new CharacterInfoList(_controller.AccountName, _controller));
        }
    }
}