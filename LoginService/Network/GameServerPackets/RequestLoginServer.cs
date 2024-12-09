using System;
using System.Threading.Tasks;
using LoginService.Controller;
using Network;

namespace LoginService.Network.GameServerPackets
{
    internal class RequestLoginServer : PacketBase
    {
        private readonly Packet _packet;
        private readonly GameServerClient _gameServerClient;
        private readonly int _randomKey;

        public RequestLoginServer(IServiceProvider serviceProvider, Packet packet, GameServerClient gameServerClient)
            : base(serviceProvider)
        {
            _packet = packet;
            _gameServerClient = gameServerClient;
            _randomKey = packet.ReadInt();
        }

        public override async Task Execute()
        {
            await _gameServerClient.SendAsync(new LoginServer(_randomKey));
        }
    }
}
