using Network;

namespace LoginService.Network.ServerPackets
{
    internal class GgAuth : ServerPacket
    {
        private const byte SkipGameGuardAuthRequest = 0x0b;
        private readonly int _sessionId;
        public GgAuth(int sessionId)
        {
            _sessionId = sessionId;
        }

        public override void Write()
        {
            WriteByte(SkipGameGuardAuthRequest);
            WriteInt(_sessionId);
            WriteBytesArray(new byte[4]);
        }
    }
}
