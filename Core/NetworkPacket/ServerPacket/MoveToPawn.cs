using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket
{
    public class MoveToPawn : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _targetId;
        private readonly int _distance;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        
        public MoveToPawn(Character character, Character target, int distance)
        {
            _objectId = character.ObjectId;
            _targetId = target.ObjectId;
            _distance = distance;
            _x = character.GetX();
            _y = character.GetY();
            _z = character.GetZ();
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x60);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_targetId);
            await WriteIntAsync(_distance);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
        }
    }
}