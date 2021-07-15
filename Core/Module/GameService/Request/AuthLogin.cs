using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.GameService.Response;
using Core.Module.Player.CharacterData.Response;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Network;
using Security;

namespace Core.Module.GameService.Request
{
    public class AuthLogin : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly SessionKey _key;
        private readonly ICharacterRepository _characterRepository;
        
        // loginName + keys must match what the accountName used.
        private readonly string _accountName;
        
        public AuthLogin(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _characterRepository = serviceProvider.GetService<IUnitOfWork>()?.Characters;

            _accountName = packet.ReadString().ToLower();
            int playKey2 = packet.ReadInt();
            int playKey1 = packet.ReadInt();
            int loginKey1 = packet.ReadInt();
            int loginKey2 = packet.ReadInt();
            
            _key = new SessionKey(loginKey1, loginKey2, playKey1, playKey2);
        }

        public override async Task Execute()
        {
            _controller.SessionKey = _key;
            _controller.AccountName = _accountName;
            
            await _controller.SendPacketAsync(new AccountInGame(_accountName, true));

            var characters= _characterRepository.GetCharactersByAccountNameAsync(_accountName);
            //CharSelectInfo charSelectInfo = new CharSelectInfo(_accountName, _controller.SessionKey.PlayOkId1, await characters);
            //_controller.GameServiceHelper.SetCharSelection(charSelectInfo.CharacterPackages);
            await _controller.SendPacketAsync(new CharacterInfoList(_accountName, _controller.SessionKey.PlayOkId1));
        }
    }
}