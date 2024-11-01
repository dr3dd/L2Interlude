using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class StopRotation : Network.ServerPacket
    {    
        private readonly int _objectId;
        private readonly int _degree;
        private readonly int _speed;
        public StopRotation(Character character, int degree, int speed)
        {
            _objectId = character.ObjectId;
            _degree = degree;
            _speed = speed;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x63);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_degree);
            await WriteIntAsync(_speed);
            await WriteByteAsync(0); // ?
        }
    }
}