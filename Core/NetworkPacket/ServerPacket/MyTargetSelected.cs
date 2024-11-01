using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class MyTargetSelected : Network.ServerPacket
    {
        /** The _object id. */
        private readonly int _objectId;
        /** The _color. */
        private readonly int _color;

        public MyTargetSelected(int objectId, int color)
        {
            _objectId = objectId;
            _color = color;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xa6);
            await WriteIntAsync(_objectId);
            await WriteShortAsync(_color);
        }
    }
}