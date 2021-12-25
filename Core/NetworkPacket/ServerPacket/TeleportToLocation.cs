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
        public override void Write()
        {
            WriteByte(0x28);
            WriteInt(_targetObjId);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
            WriteInt(0x00); // isValidation ??
            WriteInt(_heading); // nYaw
        }
    }
}