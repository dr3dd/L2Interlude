using System;
using System.Threading.Tasks;
using Network;

namespace LoginService.Network.GameServerPackets
{
    class RequestPlayersOnline : PacketBase
    {
        private readonly short _currentPlayers;
        private readonly GameServerClient _gameServerClient;

        public RequestPlayersOnline(IServiceProvider serviceProvider, Packet packet, GameServerClient gameServerClient)
            : base(serviceProvider)
        {
            _gameServerClient = gameServerClient;
            _currentPlayers = packet.ReadShort();
        }

        public override async Task Execute()
        {
            await Task.Run(() => _gameServerClient.CurrentPlayers = _currentPlayers);
        }
    }
}
