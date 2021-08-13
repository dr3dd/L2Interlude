using System;
using System.Threading.Tasks;
using Core.Controller;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    /// <summary>
    /// TODO Not implemented yet
    /// </summary>
    internal sealed class RequestCursedWeaponLocation : PacketBase
    {
        public RequestCursedWeaponLocation(IServiceProvider serviceProvider, Packet packet,
            GameServiceController controller) : base(serviceProvider)
        {
        }

        public override async Task Execute()
        {
            await Task.FromResult(1);
        }
    }
}