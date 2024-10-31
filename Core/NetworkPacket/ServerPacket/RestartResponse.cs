using System.Threading.Tasks;

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
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x5f);
            await WriteIntAsync(_result ? 1 : 0);
            await WriteStringAsync(_message);
        }
    }
}