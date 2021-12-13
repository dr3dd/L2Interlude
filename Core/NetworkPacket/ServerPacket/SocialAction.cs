namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class SocialAction : Network.ServerPacket
    {
        private readonly int _objectId;
        private readonly int _actionId;

        public SocialAction(int objectId, int actionId)
        {
            _objectId = objectId;
            _actionId = actionId;
        }
        public override void Write()
        {
            WriteByte(0x2d);
            WriteInt(_objectId);
            WriteInt(_actionId);
        }
    }
}