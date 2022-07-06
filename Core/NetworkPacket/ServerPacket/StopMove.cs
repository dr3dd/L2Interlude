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
        
        public override void Write()
        {
            WriteByte(0x47);
            WriteInt(_objectId);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
            WriteInt(_heading);
        }
    }
}