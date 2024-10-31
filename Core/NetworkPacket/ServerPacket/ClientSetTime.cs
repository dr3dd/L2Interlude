using System.Threading.Tasks;
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
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xEC);
            await WriteIntAsync(_gameTimeController.GetGameTime()); // time in client minutes
            await WriteIntAsync(6); // constant to match the server time( this determines the speed of the client clock)
        }
    }
}