using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class AccountInGame : Network.ServerPacket
    {
        private readonly string _account;
        private readonly bool _status;
        
        public AccountInGame(string account, bool status)
        {
            _account = account;
            _status = status;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x03);
            await WriteStringAsync(_account.ToLower());
            await WriteByteAsync(_status ? (byte)1 : (byte)0);
        }
    }
}