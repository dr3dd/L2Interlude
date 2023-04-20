using System;
using System.Net.Sockets;
using Core.Controller;

namespace Core;

public class GameServiceFactory : IGameServiceFactory
{
    public GameServiceController Create(TcpClient tcpClient, IServiceProvider serviceProvider)
    {
        var controller = new GameServiceController(serviceProvider);
        controller.SetTcpClient(tcpClient);
        return controller;
    }
}