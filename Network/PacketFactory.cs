namespace Network
{
    public class PacketFactory : IPacketFactory
    {
        public Packet Create(byte[] buffer, int offset)
        {
            return new Packet(buffer, offset);
        }
    }
}