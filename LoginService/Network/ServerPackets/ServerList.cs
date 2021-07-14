using System.Collections.Generic;
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

            string ip = "127.0.0.1";

            byte[] DefaultAddress = new byte[4];
            string[] w = ip.Split('.');
            DefaultAddress[0] = byte.Parse(w[0]);
            DefaultAddress[1] = byte.Parse(w[1]);
            DefaultAddress[2] = byte.Parse(w[2]);
            DefaultAddress[3] = byte.Parse(w[3]);

            WriteByte(0x04);
            WriteByte((byte)1);
            WriteByte((byte)2);

            foreach (Server server in servers)
            {
                WriteByte((byte)server.ServerId); //ServerId
                WriteBytesArray(DefaultAddress);
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
