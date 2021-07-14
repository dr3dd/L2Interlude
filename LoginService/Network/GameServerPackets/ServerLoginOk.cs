using Network;

namespace LoginService.Network.GameServerPackets
{
    public class ServerLoginOk : ServerPacket
    {
        private const byte Opcode = 0xA6;

        public override void Write()
        {
            WriteByte(Opcode);
            WriteString("Gameserver Authenticated");
        }
    }
}
