namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ExCursedWeaponList : Network.ServerPacket
    {
        public override void Write()
        {
            WriteByte(0xfe);
            WriteShort(0x45);
            WriteInt(0);
        }
    }
}