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
        public override void Write()
        {
            WriteByte(0x03);
            WriteString(_account.ToLower());
            WriteByte(_status ? (byte)1 : (byte)0);
        }
    }
}