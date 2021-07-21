namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    public class CharTemplates : Network.ServerPacket
    {
        public override void Write()
        {
            WriteByte(0x17);
        }
    }
}