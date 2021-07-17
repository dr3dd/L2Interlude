using Network;

namespace Core.Module.Player.Response
{
    public class ActionFailed : ServerPacket
    {
        public override void Write()
        {
            WriteByte(0x25);
        }
    }
}