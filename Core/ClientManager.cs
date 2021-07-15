using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks.Dataflow;
using Core.Controller;
using Core.Controller.Handlers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core
{
    public class ClientManager
    {
        private readonly GameServicePacketHandler _gameServicePacketHandler;
        private readonly ConcurrentDictionary<string, GameServiceController> _loggedClients;
        private readonly BufferBlock<PacketStream> _bufferBlock;
        
        public ClientManager(IServiceProvider serviceProvider)
        {
            _gameServicePacketHandler = serviceProvider.GetService<GameServicePacketHandler>();
            _bufferBlock = serviceProvider.GetService<NetworkWriter>()?.GetBufferBlock();
            _loggedClients = new ConcurrentDictionary<string, GameServiceController>();
        }
        
        public void AcceptClient(TcpClient client)
        {
            GameServiceController controller = new GameServiceController(this, client, _gameServicePacketHandler, _bufferBlock);
            _loggedClients.TryAdd(controller.Address.ToString(), controller);
            LoggerManager.Info($"{_loggedClients.Count} active connections");
        }
        
        public void Disconnect(string sock)
        {
            GameServiceController o;
            _loggedClients.TryRemove(sock, out o);

            LoggerManager.Info($"{_loggedClients.Count} active connections");
        }
    }
}