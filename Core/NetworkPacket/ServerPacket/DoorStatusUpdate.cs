namespace Core.NetworkPacket.ServerPacket
{
    public class DoorStatusUpdate : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _isOpen;
        public DoorStatusUpdate(int objectId, int isOpen)
        {
            _objectId = objectId;
            _isOpen = isOpen;
        }
        public override void Write()
        {
            WriteByte(0x4D);
            WriteInt(_objectId);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(19210001);
            WriteInt(126600);
            WriteInt(126600);
        }
    }
}