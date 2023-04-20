using System;
using System.Net.Sockets;
using Core.Controller;

namespace Core;

public interface IGameServiceFactory
{
    void Create(TcpClient tcpClient, IServiceProvider serviceProvider);
    GameServiceController GameServiceController();
}