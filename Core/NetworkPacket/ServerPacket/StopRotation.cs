using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class StopRotation : Network.ServerPacket
    {    
        private readonly int _objectId;
        private readonly int _degree;
        private readonly int _speed;
        public StopRotation(Character character, int degree, int speed)
        {
            _objectId = character.ObjectId;
            _degree = degree;
            _speed = speed;
        }
        public override void Write()
        {
            WriteByte(0x63);
            WriteInt(_objectId);
            WriteInt(_degree);
            WriteInt(_speed);
            WriteByte(0); // ?
        }
    }
}