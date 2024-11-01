using Core.Controller;
using Core.Module.Player;
using Core.Module.WorldData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 01.11.2024 9:43:01

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestLinkHtml : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        private readonly WorldInit _worldInit;
        private string _link;

        public RequestLinkHtml(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _worldInit = serviceProvider.GetRequiredService<WorldInit>();
            _link = packet.ReadString(); // link to html file
        }

        public override async Task Execute()
        {
            if (string.IsNullOrEmpty(_link))
            {
                LoggerManager.Warn($"RequestLinkHtml: Player : {_playerInstance.CharacterName} Invalid link: {_link}");
                return;
            }

            //TODO add some checks
            var split = _link.Split("#");
            await GoLink(split);
        }

        private async Task GoLink(IEnumerable<string> split)
        {
            var npcObjectId = Convert.ToInt32(split.First());
            var link = split.Last();
            var npcInstance = _worldInit.GetNpcInstance(npcObjectId);
            await npcInstance.ShowPage(_playerInstance, link);
        }

    }
}
