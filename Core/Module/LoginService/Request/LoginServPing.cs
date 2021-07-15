using Core.Controller;
using Helpers;
using Network;

namespace Core.Module.LoginService.Request
{
    public class LoginServPing : ServerPacket
    {
        private readonly LoginServiceController _loginServiceController;
        public LoginServPing(LoginServiceController loginServiceController)
        {
            _loginServiceController = loginServiceController;
            _loginServiceController.RandomPingKey = Rnd.Next();
        }
        public override void Write()
        {
            WriteByte(0xA0);
            WriteInt(_loginServiceController.RandomPingKey);
        }
    }
}