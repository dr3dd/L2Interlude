using System;
using System.Threading.Tasks;
using LoginService.Model;
using LoginService.Network.ServerPackets;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace LoginService.Network.ClientPackets
{
    internal class RequestServerLogin : PacketBase
    {
        private readonly LoginClient _client;
        private readonly int _loginOkID1;
        private readonly int _loginOkID2;
        private readonly byte _serverId;

        public RequestServerLogin(IServiceProvider serviceProvider, Packet packet, LoginClient client)
            :base(serviceProvider)
        {
            _client = client;
            _loginOkID1 = packet.ReadInt();
            _loginOkID2 = packet.ReadInt();
            _serverId = packet.ReadByte();
        }

        public override async Task Execute()
        {
            Server server = ServiceProvider.GetRequiredService<GameServerListener>().Get(_serverId);
            await server.GameServerClient.SendPlayerAsync(_client);
            await _client.SendPacketAsync(new PlayOk(_client));
        }
    }
}
