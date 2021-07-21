using Core.Controller;
using Helpers;

namespace Core.NetworkPacket.ServerPacket.LoginServicePacket
{
    public class LoginServPing : Network.ServerPacket
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