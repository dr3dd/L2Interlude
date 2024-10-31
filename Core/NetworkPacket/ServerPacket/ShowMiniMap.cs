using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class ShowMiniMap : Network.ServerPacket
    {
        private readonly int _mapId;
        public ShowMiniMap(int mapId)
        {
            _mapId = mapId;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x9d);
            await WriteIntAsync(_mapId);
            await WriteIntAsync(0); //SevenSigns.getInstance().getCurrentPeriod()
        }
    }
}