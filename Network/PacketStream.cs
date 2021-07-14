using System.Net.Sockets;
using Security;

namespace Network
{
    public struct PacketStream
    {
        public ServerPacket Packet { get; set; }
        public NetworkStream Stream { get; set; }
        public GameCrypt Crypt { get; set; }
    }
}