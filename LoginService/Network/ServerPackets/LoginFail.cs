using LoginService.Enum;
using Network;

namespace LoginService.Network.ServerPackets
{
    internal class LoginFail : ServerPacket
    {
        private const byte Opcode = 0x01;
        private readonly LoginFailReason _reason;

        public LoginFail(LoginFailReason reason)
        {
            _reason = reason;
        }

        public override void Write()
        {
            WriteByte(Opcode); // init packet id
            WriteByte((byte) _reason);
        }
    }
}