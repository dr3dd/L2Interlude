using System;
using System.Threading.Tasks;
using Core.Controller;
using L2Logger;
using Network;

namespace Core.NetworkPacket.ClientPacket.LoginServicePacket
{
    public class LoginServAcceptPlayer : PacketBase
    {
        private readonly LoginServiceController _controller;
        private readonly int _accountId;
        
        public LoginServAcceptPlayer(IServiceProvider serviceProvider, Packet p, LoginServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _accountId = p.ReadInt();
        }

        public override async Task Execute()
        {
            LoggerManager.Info("TODO: AwaitAddAccount");
            await Task.FromResult(1);
        }
    }
}