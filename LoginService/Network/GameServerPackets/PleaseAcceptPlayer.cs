using System.Threading.Tasks;
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
        public override async Task WriteAsync()
        {
            await WriteByteAsync(Opcode);
            await WriteIntAsync(_accountId);
            await WriteIntAsync(_key.LoginOkId1);
            await WriteIntAsync(_key.LoginOkId2);
            await WriteIntAsync(_key.PlayOkId1);
            await WriteIntAsync(_key.PlayOkId2);
        }
    }
}
