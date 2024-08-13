using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Config;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace LoginService
{
    internal class LoginService
    {
        public static IServiceProvider ServiceProvider;
        private TcpListener _tcpListener;
        public LoginService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task StartAsync()
        {
            var config = ServiceProvider.GetService<LoginConfig>();
            GameServerListener gameServerListener = ServiceProvider.GetService<GameServerListener>();
            await ServiceProvider.GetRequiredService<LoginController>().Initialise();
            ServiceProvider.GetService<GameServerListener>();

            try
            {
                string serverHost = config.ServerConfig.ServerHost;
                _tcpListener = new TcpListener(serverHost.Equals("*") ? IPAddress.Any : IPAddress.Parse(serverHost), config.ServerConfig.ServerPort);
                _tcpListener.Start();
                LoggerManager.Info("Waiting for a connections...");

                await Task.Factory.StartNew(gameServerListener.StartAsync);
                
                await StartListener();
            } 
            catch (SocketException ex)
            {
                LoggerManager.Error($"Socket Error: '{ex.SocketErrorCode}'. Message: '{ex.Message}'");
            } 
            catch (FormatException ex)
            {
                LoggerManager.Error($"Format Exception Message: '{ex.Message}'");
            }
            finally
            {
                _tcpListener?.Stop();
            }
        }

        private async Task StartListener()
        {
            while (true)
            {
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                LoggerManager.Info("Connected...");
                await HandleClient(client);
            }
        }

        private async Task HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            LoggerManager.Info($"Received connection request from: {client.Client.RemoteEndPoint}");

            await ServiceProvider.GetRequiredService<LoginController>().AcceptClient(client);
        }
    }
}
