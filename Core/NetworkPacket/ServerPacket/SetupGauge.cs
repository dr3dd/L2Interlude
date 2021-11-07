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
        
        public override void Write()
        {
            WriteByte(0x6d);
            WriteInt(_dat1);
            WriteInt(_time);
            WriteInt(_time); // c2
        }
    }
}