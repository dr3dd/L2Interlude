namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class RestartResponse : Network.ServerPacket
    {
        private readonly string _message;
        private readonly bool _result;
        
        public RestartResponse(bool result)
        {
            _result = result;
            _message = "ok merong~ khaha"; // Message like L2OFF
        }
        public override void Write()
        {
            WriteByte(0x5f);
            WriteInt(_result ? 1 : 0);
            WriteString(_message);
        }
    }
}