using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Manor.Response;
using Network;

namespace Core.Module.Manor.Request
{
    public class RequestManorList : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly IServiceProvider _serviceProvider;
        
        public RequestManorList(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _serviceProvider = serviceProvider;
        }

        public override async Task Execute()
        {
            List<string> manorsName = new List<string>
            {
                "gludio",
                "dion",
                "giran",
                "oren",
                "aden",
                "innadril",
                "goddard",
                "rune",
                "schuttgart"
            };
            await _controller.SendPacketAsync(new ExSendManorList(manorsName));
        }
    }
}