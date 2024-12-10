using Core.Enums;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 21.11.2024 0:31:54

namespace Core.NetworkPacket.ServerPacket
{
    public class PlaySound : Network.ServerPacket
    {
        private readonly string _soundFile;
        private readonly SoundType _soundType = 0;
        private readonly int _hasCenterObject = 0;
        private readonly int _objectId = 0;
        private readonly int _x = 0;
        private readonly int _y = 0;
        private readonly int _z = 0;

        public PlaySound(string soundFile)
        {
            _soundFile = soundFile;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x98);
            await WriteIntAsync((int)_soundType);
            await WriteStringAsync(_soundFile);
            await WriteIntAsync(_hasCenterObject); //0 for quest; 1 for ship;
            await WriteIntAsync(_objectId); //0 for quest; objectId of ship
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
        }
    }
}