using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using Core.Controller;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public class ClientManager
{
    private readonly ConcurrentDictionary<string, GameServiceController> _loggedClients;
    private readonly IGameServiceFactory _gameServiceFactory;
    private readonly IServiceProvider _serviceProvider;

    public ClientManager(IServiceProvider serviceProvider)
    {
        _loggedClients = new ConcurrentDictionary<string, GameServiceController>();
        _gameServiceFactory = serviceProvider.GetRequiredService<IGameServiceFactory>();
        _serviceProvider = serviceProvider;
    }

    public void AcceptClient(TcpClient client)
    {
        //GameServiceController controller = new GameServiceController(this, client, _gameServicePacketHandler, _bufferBlock);
        _gameServiceFactory.Create(client, _serviceProvider);
        var controller = _gameServiceFactory.GameServiceController();
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