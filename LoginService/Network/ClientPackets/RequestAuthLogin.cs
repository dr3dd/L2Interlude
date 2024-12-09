using System;
using System.Threading.Tasks;
using Config;
using DataBase.Entities;
using DataBase.Interfaces;
using LoginService.Controller;
using LoginService.Enum;
using LoginService.Network.ServerPackets;
using Microsoft.Extensions.DependencyInjection;
using Network;
using Security;

namespace LoginService.Network.ClientPackets
{
    internal class RequestAuthLogin : PacketBase
    {
        private readonly LoginServiceController _client;
        private readonly IAccountRepository _accountRepository;
        private readonly LoginServerConfig _config;
        private readonly byte[] _raw;
        public RequestAuthLogin(IServiceProvider serviceProvider, Packet packet, LoginServiceController client) : base (serviceProvider)
        {
            _accountRepository = ServiceProvider.GetService<IUnitOfWorkLogin>()?.Accounts;
            _client = client;
            _raw = packet.ReadByteArray(128);
            _config = ServiceProvider.GetRequiredService<LoginConfig>().ServerConfig;
        }

        public override async Task Execute()
        {
            _client.State = LoginClientState.AuthedLogin;

            var login = _client.GetDecryptedLogin(_raw);
            var password = _client.GetDecryptedPassword(_raw);

            UserAuthEntity userAuthEntity = await _accountRepository.GetAccountByLoginAsync(login);
            
            if (userAuthEntity == null)
            {
                if (_config.AutoCreateAccount)
                {
                    userAuthEntity = await _accountRepository.CreateAccountAsync(login, password);
                }
                else
                {
                    await _client.SendPacketAsync(new LoginFail(LoginFailReason.ReasonUserOrPassWrong));
                    _client.Close();
                    return;
                }
            }
            else
            {
                if (!userAuthEntity.Password.Equals(L2Security.HashPassword(password))) {
                    await _client.SendPacketAsync(new LoginFail(LoginFailReason.ReasonUserOrPassWrong));
                    _client.Close();
                    return;
                }
            }
            
            _client.ActiveUserAuthEntity = userAuthEntity;
            _client.State = LoginClientState.AuthedLogin;
            await _client.SendPacketAsync(new LoginOk(_client.SessionKey));
        }
    }
}
