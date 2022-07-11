namespace Core.NetworkPacket.ServerPacket
{
    public class AutoAttackStop : Network.ServerPacket
    {
        private readonly int _targetObjId;
        public AutoAttackStop(int targetObjId)
        {
            _targetObjId = targetObjId;
        }
        public override void Write()
        {
            WriteByte(0x2c);
            WriteInt(_targetObjId);
        }
    }
}