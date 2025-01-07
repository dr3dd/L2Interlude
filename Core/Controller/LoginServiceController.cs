using Config;
using Core.Controller.Handlers;
using Core.NetworkPacket.ServerPacket.LoginServicePacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Core.Controller
{
    public class LoginServiceController
    {
        public bool IsConnected { get; private set; }
        private TcpClient _authServerConnection;
        private NetworkStream _networkStream;
        private readonly LoginServicePacketHandler _loginServicePacketHandler;
        public int RandomPingKey { get; set; }
        private readonly GameConfig _gameConfig;

        public LoginServiceController(IServiceProvider serviceProvider, LoginServicePacketHandler loginServicePacketHandler)
        {
            _gameConfig = serviceProvider.GetService<GameConfig>();
            _loginServicePacketHandler = loginServicePacketHandler;
            
        }
        
        public async Task StartAsync()
        {
            IsConnected = false;
            try
            {
                _authServerConnection = new TcpClient(_gameConfig.ServerConfig.LoginServiceHost, _gameConfig.ServerConfig.LoginServicePort);
                _networkStream = _authServerConnection.GetStream();
            }
            catch (SocketException)
            {
                LoggerManager.Error("Login server is not responding. Retrying in 5 seconds...");
                await Task.Delay(5000).ContinueWith(x => StartAsync());
                return;
            }
            
            IsConnected = true;
            
            await SendPacketAsync(new LoginAuth(_gameConfig));
            await SendPacketAsync(new LoginServPing(this));
            
            await Task.Factory.StartNew(ReadAsync);
        }

        private async Task SendPacketAsync(ServerPacket packet)
        {
            if(_gameConfig.DebugConfig.ShowNamePacket && _gameConfig.DebugConfig.ShowPacketToClient)
            {
                LoggerManager.Debug($"LoginServiceController: GAME>>AUTH name: {packet.GetType().Name}");
            }
            await packet.WriteAsync();
            List<byte> blist = new List<byte>();
            byte[] db = packet.ToByteArray();

            short len = (short)db.Length;
            blist.AddRange(BitConverter.GetBytes(len));
            blist.AddRange(db);

            await _networkStream.WriteAsync(blist.ToArray(), 0, blist.Count);
            await _networkStream.FlushAsync();

            if (_gameConfig.DebugConfig.ShowPacket && _gameConfig.DebugConfig.ShowPacketToAuth) // show packet header & body 
            {
                printPacketBody(db);
            }
        }

        private async Task ReadAsync()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2];
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, 2);

                    if (bytesRead != 2)
                    {
                        if (_gameConfig.DebugConfig.ShowPacket && _gameConfig.DebugConfig.ShowPacketToGame) // show packet header & body 
                        {
                            printPacketBody(buffer, "AUTH>>GAME");
                        }
                        throw new Exception("Wrong packet");
                    }

                    short length = BitConverter.ToInt16(buffer, 0);

                    buffer = new byte[length];
                    bytesRead = await _networkStream.ReadAsync(buffer, 0, length);

                    if (bytesRead != length)
                    {
                        if (_gameConfig.DebugConfig.ShowPacket && _gameConfig.DebugConfig.ShowPacketToGame) // show packet header & body 
                        {
                            printPacketBody(buffer, "AUTH>>GAME");
                        }
                        throw new Exception("Wrong packet");
                    }

                    await Task.Factory.StartNew(() => _loginServicePacketHandler.HandlePacket(new Packet(buffer, 1), this));
                }
            }
            catch (Exception e)
            {
                LoggerManager.Error($"LoginServiceController: {e.Message}");
                await Task.Delay(5000).ContinueWith(x => StartAsync());
            }
        }
        private void printPacketBody(byte[] db, string target = "GAME>>AUTH")
        {
            string str = "";
            foreach (byte b in db)
                str += b.ToString("x2") + " ";
            LoggerManager.Debug($"LoginServiceController: {target} body: [ {str} ]");
        }
    }
}
