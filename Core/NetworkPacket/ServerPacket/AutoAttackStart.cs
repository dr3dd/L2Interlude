namespace Core.NetworkPacket.ServerPacket
{
    public class AutoAttackStart : global::Network.ServerPacket
    {
        private readonly int _targetObjId;
        public AutoAttackStart(int targetObjId)
        {
            _targetObjId = targetObjId;
        }
        public override void Write()
        {
            WriteByte(0x2b);
            WriteInt(_targetObjId);
        }
    }
}