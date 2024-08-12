using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Config;
using Core;
using L2Logger;
using Security;

namespace GameService
{
    class GameService
    {
        private readonly IServiceProvider _serviceProvider;
        private TcpListener _tcpListener;
        private readonly GameConfig _gameConfig;

        public GameService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //Load Configs
            _gameConfig = serviceProvider.GetService<GameConfig>();
        }
        public async Task StartAsync()
        {
            //Generate BlowFish Keys
            BlowFishKeygen.GenerateKeys();
            _serviceProvider.GetService<Initializer>()?.Load();

            string serverHost = _gameConfig.ServerConfig.ServerHost;
            _tcpListener = new TcpListener(serverHost.Equals("*") ? IPAddress.Any : IPAddress.Parse(serverHost), _gameConfig.ServerConfig.ServerPort);

            try
            {
                _tcpListener.Start();
            }
            catch (SocketException ex)
            {
                LoggerManager.Error($"Socket Error: '{ex.SocketErrorCode}'. Message: '{ex.Message}' (Error Code: '{ex.NativeErrorCode}')");
            }

            LoggerManager.Info($"Listening Gameservers on {serverHost}: {_gameConfig.ServerConfig.ServerPort}");

            await StartListener();
        }

        private async Task StartListener()
        {
            while (true)
            {
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                LoggerManager.Info("Connected...");
                await Task.Factory.StartNew(() => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            LoggerManager.Info($"Received connection request from: {client.Client.RemoteEndPoint}");
            _serviceProvider.GetService<ClientManager>()?.AcceptClient(client);
        }
    }
}
