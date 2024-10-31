using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ValidateLocation : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;

        public ValidateLocation(Character character)
        {
            _objectId = character.ObjectId;
            _x = character.GetX();
            _y = character.GetY();
            _z = character.GetZ();
            _heading = character.Heading;
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x61);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
            await WriteIntAsync(_heading);
        }
    }
}