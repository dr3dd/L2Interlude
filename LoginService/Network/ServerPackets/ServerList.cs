using System.Collections.Generic;
using System.Linq;
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
        public override void Write()
        {

            IEnumerable<Server> servers = LoginService.ServiceProvider.GetService<GameServerListener>().Servers;

            WriteByte(0x04);
            WriteByte((byte)servers.Count());
            WriteByte(_client.ActiveUserAuthEntity.LastWorld);

            foreach (Server server in servers)
            {
                WriteByte((byte)server.ServerId); //ServerId
                WriteBytesArray(server.GetIp(_client));
                WriteInt(server.Port); //port
                WriteByte(0); //// age limit
                WriteByte(1);
                WriteShort(5);
                WriteShort(10);
                WriteByte(server.Connected ? (byte)1 : (byte)0); //status of server
                WriteInt(0);
                WriteByte(0);
            }
        }
    }
}
