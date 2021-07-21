namespace Core.NetworkPacket.ServerPacket
{
    public class ActionFailed : Network.ServerPacket
    {
        public override void Write()
        {
            WriteByte(0x25);
        }
    }
}