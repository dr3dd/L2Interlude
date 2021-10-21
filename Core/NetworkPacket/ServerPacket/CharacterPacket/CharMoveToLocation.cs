using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    public class CharMoveToLocation : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _xDst;
        private readonly int _yDst;
        private readonly int _zDst;

        public CharMoveToLocation(PlayerInstance playerInstance)
        {
            _objectId = playerInstance.ObjectId;
            _x = playerInstance.GetX();
            _y = playerInstance.GetY();
            _z = playerInstance.GetZ();
            _xDst = playerInstance.PlayerMovement().GetXDestination();
            _yDst = playerInstance.PlayerMovement().GetYDestination();
            _zDst = playerInstance.PlayerMovement().GetZDestination();
        }
        
        public override void Write()
        {
            WriteByte(0x01);
            WriteInt(_objectId);
            WriteInt(_xDst);
            WriteInt(_yDst);
            WriteInt(_zDst);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
        }
    }
}