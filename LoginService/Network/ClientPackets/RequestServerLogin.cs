using System;
using System.Threading.Tasks;
using DataBase.Interfaces;
using LoginService.Controller;
using LoginService.Model;
using LoginService.Network.ServerPackets;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace LoginService.Network.ClientPackets
{
    internal class RequestServerLogin : PacketBase
    {
        private readonly LoginServiceController _client;
        private readonly int _loginOkID1;
        private readonly IAccountRepository _accountRepository;
        private readonly int _loginOkID2;
        private readonly byte _serverId;

        public RequestServerLogin(IServiceProvider serviceProvider, Packet packet, LoginServiceController client)
            :base(serviceProvider)
        {
            _client = client;
            _accountRepository = serviceProvider.GetService<IUnitOfWorkLogin>()?.Accounts;
            _loginOkID1 = packet.ReadInt();
            _loginOkID2 = packet.ReadInt();
            _serverId = packet.ReadByte();
        }

        public override async Task Execute()
        {
            Server server = ServiceProvider.GetRequiredService<GameServerListener>().Get(_serverId);
            _client.ActiveUserAuthEntity.LastWorld = _serverId;
            _client.ActiveUserAuthEntity.LastLogin = DateTime.Now;
            await _accountRepository.UpdateLastUseAsync(_client.ActiveUserAuthEntity);
            await server.GameServerClient.SendPlayerAsync(_client);
            await _client.SendPacketAsync(new PlayOk(_client));
        }
    }
}
