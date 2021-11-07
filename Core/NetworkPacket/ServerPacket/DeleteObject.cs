namespace Core.NetworkPacket.ServerPacket
{
    public class DeleteObject : Network.ServerPacket
    {
        private readonly int _objectId;
        public DeleteObject(int objectId)
        {
            _objectId = objectId;
        }

        public override void Write()
        {
            WriteByte(0x12);
            WriteInt(_objectId);
            WriteInt(0x00); // c2
        }
    }
}