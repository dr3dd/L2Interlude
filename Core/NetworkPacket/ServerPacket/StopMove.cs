using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class StopMove : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;
        private Character _character;

        public StopMove(Character character) : this(character.ObjectId,
            character.GetX(), character.GetY(),
            character.GetZ(), character.Heading)
        {
            _character = character;
        }

        private StopMove(int objectId, int x, int y, int z, int heading)
        {
            _objectId = objectId;
            _x = x;
            _y = y;
            _z = z;
            _heading = heading;
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x47);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
            await WriteIntAsync(_heading);
        }
    }
}