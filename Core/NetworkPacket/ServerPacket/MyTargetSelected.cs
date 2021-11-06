namespace Core.NetworkPacket.ServerPacket
{
    public class MyTargetSelected : Network.ServerPacket
    {
        /** The _object id. */
        private readonly int _objectId;
        /** The _color. */
        private readonly int _color;

        public MyTargetSelected(int objectId, int color)
        {
            _objectId = objectId;
            _color = color;
        }
        public override void Write()
        {
            WriteByte(0xa6);
            WriteInt(_objectId);
            WriteShort(_color);
        }
    }
}