using Network;
using Security;

namespace LoginService.Network.ServerPackets
{
    internal class LoginOk : ServerPacket
    {
        private readonly int _loginOk1;
        private readonly int _loginOk2;
        public LoginOk(SessionKey sessionKey)
        {
            _loginOk1 = sessionKey.LoginOkId1;
            _loginOk2 = sessionKey.LoginOkId2;
        }
        public override void Write()
        {
            WriteByte(0x03);
            WriteInt(_loginOk1);
            WriteInt(_loginOk2);
            WriteInt(0x00);
            WriteInt(0x00);
            WriteInt(0x000003ea);
            WriteInt(0x00);
            WriteInt(0x00);
            WriteInt(0x00);
            WriteBytesArray(new byte[16]);
        }
    }
}
