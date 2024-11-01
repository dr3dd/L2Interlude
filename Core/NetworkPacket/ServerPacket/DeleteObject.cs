using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class DeleteObject : Network.ServerPacket
    {
        private readonly int _objectId;
        public DeleteObject(int objectId)
        {
            _objectId = objectId;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x12);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(0x00); // c2
        }
    }
}