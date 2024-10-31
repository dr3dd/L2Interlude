using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class SocialAction : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _actionId;

        public SocialAction(int objectId, int actionId)
        {
            _objectId = objectId;
            _actionId = actionId;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x2d);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_actionId);
        }
    }
}