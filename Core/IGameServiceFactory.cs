using System;
using System.Net.Sockets;
using Core.Controller;

namespace Core;

public interface IGameServiceFactory
{
    GameServiceController Create(TcpClient tcpClient, IServiceProvider serviceProvider);
}