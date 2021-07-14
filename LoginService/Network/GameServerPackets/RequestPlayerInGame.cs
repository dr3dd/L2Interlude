using System;
using System.Threading.Tasks;
using Network;

namespace LoginService.Network.GameServerPackets
{
    internal class RequestPlayerInGame : PacketBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GameServerClient _gameServerClient;
        
        private readonly int _accountId;
        private readonly byte _status;

        public RequestPlayerInGame(IServiceProvider serviceProvider, Packet packet, GameServerClient gameServerClient)
            :base (serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _gameServerClient = gameServerClient;

            _accountId = packet.ReadInt();
            _status = packet.ReadByte();
        }

        public override async Task Execute()
        {
            await Task.Run(() => _gameServerClient.AccountInGame(_accountId, _status));
        }
    }
}
