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
        
        public override void Write()
        {
            WriteByte(0x2a);
            WriteInt(_targetObjId);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
        }
    }
}