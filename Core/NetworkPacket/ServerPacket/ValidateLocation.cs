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
        
        public override void Write()
        {
            WriteByte(0x61);
            WriteInt(_objectId);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
            WriteInt(_heading);
        }
    }
}