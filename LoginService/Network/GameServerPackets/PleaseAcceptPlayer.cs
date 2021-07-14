using Network;
using Security;

namespace LoginService.Network.GameServerPackets
{
    internal class PleaseAcceptPlayer : ServerPacket
    {
        private const byte Opcode = 0xA7;
        private readonly int _accountId;
        private readonly SessionKey _key;
        public PleaseAcceptPlayer(int accountId, SessionKey key)
        {
            _accountId = accountId;
            _key = key;
        }
        public override void Write()
        {
            WriteByte(Opcode);
            WriteInt(_accountId);
            WriteInt(_key.LoginOkId1);
            WriteInt(_key.LoginOkId2);
            WriteInt(_key.PlayOkId1);
            WriteInt(_key.PlayOkId2);
        }
    }
}
