using System;
using System.Threading.Tasks;
using Core.Controller;
using L2Logger;
using Network;

namespace Core.Module.LoginService.Response
{
    public class LoginServLoginOk : PacketBase
    {
        private readonly LoginServiceController _controller;
        private readonly string _code;
        
        public LoginServLoginOk(IServiceProvider serviceProvider, Packet p, LoginServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _code = p.ReadString();
        }

        public override async Task Execute()
        {
            LoggerManager.Info("TODO: Login OK");
            await Task.FromResult(1);
        }
    }
}