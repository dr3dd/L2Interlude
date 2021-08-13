namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ShowMiniMap : Network.ServerPacket
    {
        private readonly int _mapId;
        public ShowMiniMap(int mapId)
        {
            _mapId = mapId;
        }
        public override void Write()
        {
            WriteByte(0x9d);
            WriteInt(_mapId);
            WriteInt(0); //SevenSigns.getInstance().getCurrentPeriod()
        }
    }
}