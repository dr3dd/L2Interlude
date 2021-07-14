using Network;

namespace LoginService.Network.ServerPackets
{
    internal class PlayOk : ServerPacket
    {
        private const byte Opcode = 0x07;
        private readonly LoginClient _client;

        public PlayOk(LoginClient client)
        {
            _client = client;
        }

        public override void Write()
        {
            WriteByte(Opcode);
            WriteInt(_client.SessionKey.PlayOkId1);
            WriteInt(_client.SessionKey.PlayOkId2);
        }
    }
}
