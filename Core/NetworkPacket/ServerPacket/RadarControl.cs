using Core.Enums;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 14.11.2024 11:52:02

namespace Core.NetworkPacket.ServerPacket
{
    public class RadarControl : Network.ServerPacket
    {
        private readonly int _show;
        private readonly int _type;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;

        public RadarControl(RadarControlType show, RadarPositionType type, int x, int y, int z)
        {
            _show = (int)show;
            _type = (int)type;
            _x = x;
            _y = y;
            _z = z;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xEB);
            await WriteIntAsync(_show);
            await WriteIntAsync(_type);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
        }
    }
}