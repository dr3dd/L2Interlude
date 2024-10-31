using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class SetupGauge : Network.ServerPacket
    {
        public const int Blue = 0;
        public const int Red = 1;
        public const int Cyan = 2;
	
        private readonly int _dat1;
        private readonly double _time;

        public SetupGauge(int dat1, double time)
        {
            _dat1 = dat1; // color 0-blue 1-red 2-cyan 3-
            _time = time;
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x6d);
            await WriteIntAsync(_dat1);
            await WriteIntAsync(_time);
            await WriteIntAsync(_time); // c2
        }
    }
}