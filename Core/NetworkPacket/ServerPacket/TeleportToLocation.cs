using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket
{
    public class TeleportToLocation : Network.ServerPacket
    {
        private readonly int _targetObjId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;
        
        public TeleportToLocation(Character character, int x, int y, int z)
        {
            _targetObjId = character.ObjectId;
            _x = x;
            _y = y;
            _z = z;
            _heading = character.WorldObjectPosition().Heading;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x28);
            await WriteIntAsync(_targetObjId);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
            await WriteIntAsync(0x00); // isValidation ??
            await WriteIntAsync(_heading); // nYaw
        }
    }
}