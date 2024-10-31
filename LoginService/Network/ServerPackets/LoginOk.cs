using System.Threading.Tasks;
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
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x03);
            await WriteIntAsync(_loginOk1);
            await WriteIntAsync(_loginOk2);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x000003ea);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteBytesArrayAsync(new byte[16]);
        }
    }
}
