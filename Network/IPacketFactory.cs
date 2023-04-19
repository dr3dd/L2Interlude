namespace Network
{
    public interface IPacketFactory
    {
        Packet Create(byte[] buffer, int offset);
    }
}