
using Network;

namespace Core.Module.LoginService.Request
{
    public class LoginAuth : ServerPacket
    {
        public override void Write()
        {
            WriteByte(0xA1);
            WriteShort(7777);
            WriteString("127.0.0.1");
            WriteString(string.Empty);
            WriteString("-4865d8fc93374b41fb387a308bf6c3d6");
            WriteInt(0);
            WriteShort(100); //max players
            WriteByte(0x00); //only gm or not
            WriteByte(0x00); //test or not
        }
    }
}