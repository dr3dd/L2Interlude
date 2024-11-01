using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginService.Model;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace LoginService.Network.ServerPackets
{

    //this class very important to connect to game server
    internal class ServerList : ServerPacket
    {
        LoginClient _client;
        public ServerList(LoginClient client)
        {
            _client = client;
        }
        public override async Task WriteAsync()
        {
            IEnumerable<Server> servers = LoginService.ServiceProvider.GetService<GameServerListener>().Servers;
            await WriteByteAsync(0x04);
            await WriteByteAsync((byte)servers.Count());
            await WriteByteAsync(_client.ActiveUserAuthEntity.LastWorld);

            foreach (Server server in servers)
            {
                await WriteByteAsync((byte)server.ServerId); //ServerId
                await WriteBytesArrayAsync(server.GetIp(_client));
                await WriteIntAsync(server.Port); //port
                await WriteByteAsync(0); //// age limit
                await WriteByteAsync(1);
                await WriteShortAsync(5);
                await WriteShortAsync(10);
                await WriteByteAsync(server.Connected ? (byte)1 : (byte)0); //status of server
                await WriteIntAsync(0);
                await WriteByteAsync(0);
            }
        }
    }
}
