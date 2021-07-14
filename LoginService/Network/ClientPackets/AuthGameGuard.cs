using System;
using System.Threading.Tasks;
using L2Logger;
using LoginService.Enum;
using LoginService.Network.ServerPackets;
using Network;

namespace LoginService.Network.ClientPackets
{
    internal class AuthGameGuard : PacketBase
    {
        private readonly LoginClient _client;
        private readonly int _sessionId;
        public AuthGameGuard(IServiceProvider serviceProvider, Packet packet, LoginClient client) : base(serviceProvider)
        {
            _client = client;
            _sessionId = packet.ReadInt();
        }

        public override async Task Execute()
        {
            if (_sessionId == _client.SessionId)
            {
                _client.State = LoginClientState.AuthedGG;
                await _client.SendPacketAsync(new GgAuth(_client.SessionId));
            } else
            {
                LoggerManager.Info("Session Id error");
            }
        }
    }
}
