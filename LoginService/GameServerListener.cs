using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Config;
using L2Logger;
using LoginService.Model;

namespace LoginService
{
    public class GameServerListener
    {
        private readonly IServiceProvider _serviceProvider;
        public IList<Server> Servers { get; private set; }
        private TcpListener _tcpListener;
        public GameServerListener(IServiceProvider serviceProvider, GameServerPacketHandler packetHandler)
        {
            _serviceProvider = serviceProvider;
            Initialise();
        }

        private void Initialise()
        {
            Servers = new List<Server>
            {
                new Server
                {
                    ServerId = 2,
                    Name = "Kain",
                    ServerKey = "-4865d8fc93374b41fb387a308bf6c3d6"
                },
            };

            LoggerManager.Info($"GameServerListener: loaded {Servers.Count} servers");
        }

        public async Task StartAsync()
        {
            var config = _serviceProvider.GetService<GameConfig>();

            try
            {
                string serverHost = config.LoginServerConfig.ServerHost;
                _tcpListener = new TcpListener(serverHost.Equals("*") ? IPAddress.Any : IPAddress.Parse(serverHost), config.LoginServerConfig.GameServerPort);
                _tcpListener.Start();
                LoggerManager.Info($"Auth server listening gameservers at {config.LoginServerConfig.ServerHost}:{config.LoginServerConfig.GameServerPort}");
            }
            catch (FormatException ex)
            {
                LoggerManager.Error($"Format Exception Message: '{ex.Message}'");
            }
            catch (SocketException ex)
            {
                LoggerManager.Error($"Socket Error: '{ex.SocketErrorCode}'. Message: '{ex.Message}'");
            }

            await StartListener();
        }

        private async Task StartListener()
        {
            while (true)
            {
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                LoggerManager.Info($"Received connection request from: {client.Client.RemoteEndPoint}");
                await HandleGameServer(client);
            }
        }

        private async Task HandleGameServer(TcpClient client)
        {
            await _serviceProvider.GetService<GameServerClient>().AcceptServer(client);
        }

        public Server Get(short serverId)
        {
            return Servers.FirstOrDefault(s => s.ServerId == serverId);
        }

    }
}
