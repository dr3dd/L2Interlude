using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks.Dataflow;
using Core.Controller;
using Core.Controller.Handlers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core;

public class ClientManager
{
    private readonly GameServicePacketHandler _gameServicePacketHandler;
    private readonly ConcurrentDictionary<string, GameServiceController> _loggedClients;
    private readonly BufferBlock<PacketStream> _bufferBlock;
    private readonly IGameServiceFactory _gameServiceFactory;
    private readonly IServiceProvider _serviceProvider;

    public ClientManager(IServiceProvider serviceProvider)
    {
        _gameServicePacketHandler = serviceProvider.GetRequiredService<GameServicePacketHandler>();
        _bufferBlock = serviceProvider.GetRequiredService<NetworkWriter>().GetBufferBlock();
        _loggedClients = new ConcurrentDictionary<string, GameServiceController>();
        _gameServiceFactory = serviceProvider.GetRequiredService<IGameServiceFactory>();
        _serviceProvider = serviceProvider;
    }

    public void AcceptClient(TcpClient client)
    {
        //GameServiceController controller = new GameServiceController(this, client, _gameServicePacketHandler, _bufferBlock);
        var controller = _gameServiceFactory.Create(client, _serviceProvider);
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