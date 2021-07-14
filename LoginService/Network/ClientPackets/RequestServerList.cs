using System;
using System.Threading.Tasks;
using L2Logger;
using LoginService.Network.ServerPackets;
using Network;

namespace LoginService.Network.ClientPackets
{
    internal class RequestServerList : PacketBase
    {
        private readonly LoginClient _client;
        private readonly int _skey1;
        private readonly int _skey2;
        public RequestServerList(IServiceProvider serviceProvider, Packet packet, LoginClient client) : base(serviceProvider)
        {
            _client = client;
            _skey1 = packet.ReadInt();
            _skey2 = packet.ReadInt();
        }

        public override async Task Execute()
        {
            if (_client.SessionKey.CheckLoginPair(_skey1, _skey2))
            {
                await _client.SendPacketAsync(new ServerList(_client));
            } else
            {
                LoggerManager.Info("Access Filed");
            }
        }
    }
}
