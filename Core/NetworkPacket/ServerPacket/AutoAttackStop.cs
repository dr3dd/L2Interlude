using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    public class AutoAttackStop : Network.ServerPacket
    {
        private readonly int _targetObjId;
        public AutoAttackStop(int targetObjId)
        {
            _targetObjId = targetObjId;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x2c);
            await WriteIntAsync(_targetObjId);
        }
    }
}