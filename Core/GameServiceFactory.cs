using System;
using System.Net.Sockets;
using Core.Controller;

namespace Core;

public class GameServiceFactory : IGameServiceFactory
{
    public GameServiceController Create(TcpClient tcpClient, IServiceProvider serviceProvider)
    {
        return new GameServiceController(tcpClient, serviceProvider);
    }
}