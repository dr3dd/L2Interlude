using Network;

namespace LoginService.Network.ServerPackets
{
    internal class Init : ServerPacket
    {
        private readonly int _sessionId;

        private readonly byte[] _publicKey;
        private readonly byte[] _blowfishKey;

        public Init(LoginClient client) : this(client.GetScrambledModulus(), client.BlowFishKey, client.SessionId)
        {

        }

        public Init(byte[] publickey, byte[] blowfishkey, int sessionId)
        {
            _sessionId = sessionId;
            _publicKey = publickey;
            _blowfishKey = blowfishkey;
        }

        public override void Write()
        {
            WriteByte(0x00); // init packet id
            WriteInt(_sessionId); // session id
            WriteInt(0x0000c621); // protocol revision
            WriteBytesArray(_publicKey); // RSA Public Key
            
            WriteInt(0x29DD954E);
            WriteInt(0x77C39CFC);
            WriteInt(0x97ADB620);
            WriteInt(0x07BDE0F7);

            WriteBytesArray(_blowfishKey); // BlowFish key
            WriteByte(0x00);
        }
    }
}
