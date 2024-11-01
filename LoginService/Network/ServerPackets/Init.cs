using System.Threading.Tasks;
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

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x00); // init packet id
            await WriteIntAsync(_sessionId); // session id
            await WriteIntAsync(0x0000c621); // protocol revision
            await WriteBytesArrayAsync(_publicKey); // RSA Public Key
            
            await WriteIntAsync(0x29DD954E);
            await WriteIntAsync(0x77C39CFC);
            await WriteIntAsync(0x97ADB620);
            await WriteIntAsync(0x07BDE0F7);

            await WriteBytesArrayAsync(_blowfishKey); // BlowFish key
            await WriteByteAsync(0x00);
        }
    }
}
