using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ValidateLocation : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;

        public ValidateLocation(PlayerInstance playerInstance)
        {
            _objectId = playerInstance.ObjectId;
            _x = playerInstance.GetX();
            _y = playerInstance.GetY();
            _z = playerInstance.GetZ();
            _heading = playerInstance.Heading;
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