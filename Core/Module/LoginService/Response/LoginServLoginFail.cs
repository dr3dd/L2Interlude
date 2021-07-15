using System;
using System.Threading.Tasks;
using Core.Controller;
using L2Logger;
using Network;

namespace Core.Module.LoginService.Response
{
    public class LoginServLoginFail : PacketBase
    {
        private readonly LoginServiceController _controller;

        public LoginServLoginFail(IServiceProvider serviceProvider, Packet p, LoginServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
        }

        public override async Task Execute()
        {
            LoggerManager.Info("TODO: Login Failed");
            await Task.FromResult(1);
        }
    }
}