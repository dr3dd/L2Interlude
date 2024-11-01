using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class AutoAttackStart : global::Network.ServerPacket
    {
        private readonly int _targetObjId;
        public AutoAttackStart(int targetObjId)
        {
            _targetObjId = targetObjId;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x2b);
            await WriteIntAsync(_targetObjId);
        }
    }
}