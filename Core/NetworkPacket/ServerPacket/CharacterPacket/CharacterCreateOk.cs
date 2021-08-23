namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    internal sealed class CharacterCreateOk : Network.ServerPacket
    {
        public override void Write()
        {
            WriteByte(0x19);
            WriteInt(0x01);
        }
    }
}