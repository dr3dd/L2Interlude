using Network;

namespace LoginService.Network.GameServerPackets
{
    internal class LoginServer : ServerPacket
    {
        private const byte Opcode = 0xA1;
        private readonly int _key;
        public LoginServer(int randomKey)
        {
            _key = randomKey;
        }

        public override void Write()
        {
            WriteByte(Opcode);
            WriteInt(_key);
        }
    }
}
