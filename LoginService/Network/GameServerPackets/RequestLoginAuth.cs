using System;
using System.Linq;
using System.Threading.Tasks;
using L2Logger;
using LoginService.Controller;
using LoginService.Model;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace LoginService.Network.GameServerPackets
{
    internal class RequestLoginAuth : PacketBase
    {
        private readonly GameServerClient _gameServerClient;
        private readonly short _port;
        private readonly string _host;
        private readonly string _info;
        private readonly string _serverKey;
        private readonly int _currentPlayers;
        private readonly short _maxPlayers;
        private readonly byte _gmonly;
        private readonly byte _test;


        public RequestLoginAuth(IServiceProvider serviceProvider, Packet packet, GameServerClient gameServerClient) 
            : base(serviceProvider)
        {
            _gameServerClient = gameServerClient;
            _port = packet.ReadShort();
            _host = packet.ReadString().ToLower();
            _info = packet.ReadString().ToLower();
            _serverKey = packet.ReadString();
            _currentPlayers = packet.ReadInt();
            _maxPlayers = packet.ReadShort();
            _gmonly = packet.ReadByte();
            _test = packet.ReadByte();
        }

        public override async Task Execute()
        {
            Server server = ServiceProvider.GetRequiredService<GameServerListener>().Servers.FirstOrDefault((x => x.ServerKey == _serverKey));
            server.GameServerClient = _gameServerClient;

            _gameServerClient.ServerId = server.ServerId;
            _gameServerClient.Info = _info;
            _gameServerClient.Wan = _host;
            _gameServerClient.Port = _port;
            _gameServerClient.MaxPlayers = _maxPlayers;
            _gameServerClient.GmOnly = _gmonly == 1;
            _gameServerClient.TestMode = _test == 1;
            _gameServerClient.Connected = true;

            await _gameServerClient.SendAsync(new ServerLoginOk());

            LoggerManager.Info($"Server #{server.ServerId} connected");
        }
    }
}
