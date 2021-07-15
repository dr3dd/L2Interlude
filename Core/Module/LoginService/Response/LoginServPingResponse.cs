using System;
using System.Threading.Tasks;
using Core.Controller;
using L2Logger;
using Network;

namespace Core.Module.LoginService.Response
{
    public class LoginServPingResponse : PacketBase
    {
        private readonly LoginServiceController _controller;
        private readonly int _key;
        
        public LoginServPingResponse(IServiceProvider serviceProvider, Packet p, LoginServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _key = p.ReadInt();
        }

        public override async Task Execute()
        {
            if (_key != _controller.RandomPingKey)
            {
                LoggerManager.Info($"Invalid random ping response {_key} != {_controller.RandomPingKey}");
            }
            
            await Task.FromResult(1);
        }
    }
}