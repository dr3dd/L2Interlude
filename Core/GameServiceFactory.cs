using System;
using System.Net.Sockets;
using Core.Controller;

namespace Core;

public class GameServiceFactory : IGameServiceFactory
{
    private GameServiceController _controller;
    public void Create(TcpClient tcpClient, IServiceProvider serviceProvider)
    {
        _controller = new GameServiceController(serviceProvider);
        _controller.SetTcpClient(tcpClient);
    }

    public GameServiceController GameServiceController()
    {
        return _controller;
    }
}