using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class TargetUnselected : Network.ServerPacket
    {
        private readonly int _targetObjId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        
        public TargetUnselected(Character character)
        {
            _targetObjId = character.ObjectId;
            _x = character.GetX();
            _y = character.GetY();
            _z = character.GetZ();
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x2a);
            await WriteIntAsync(_targetObjId);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
        }
    }
}