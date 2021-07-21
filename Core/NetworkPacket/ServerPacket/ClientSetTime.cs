using Core.Controller;
using Microsoft.Extensions.DependencyInjection;

namespace Core.NetworkPacket.ServerPacket
{
    public class ClientSetTime : Network.ServerPacket
    {
        private readonly GameTimeController _gameTimeController;
        public ClientSetTime()
        {
            _gameTimeController = Initializer.ServiceProvider.GetService<GameTimeController>();
        }
        public override void Write()
        {
            WriteByte(0xEC);
            WriteInt(_gameTimeController.GetGameTime()); // time in client minutes
            WriteInt(6); // constant to match the server time( this determines the speed of the client clock)
        }
    }
}