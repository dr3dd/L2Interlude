using System;
using System.Collections.Concurrent;
using L2Logger;
using LoginService.Network;
using LoginService.Network.ClientPackets;
using LoginService.Network.GameServerPackets;
using Network;

namespace LoginService
{
    public class GameServerPacketHandler
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<byte, Type> _serverPackets;

        public GameServerPacketHandler(IServiceProvider serviceProvider)
        {
            _serverPackets = new ConcurrentDictionary<byte, Type>();
            _serviceProvider = serviceProvider;

            _serverPackets.TryAdd(0xA0, typeof(RequestLoginServer));
            _serverPackets.TryAdd(0xA1, typeof(RequestLoginAuth));
            //_serverPackets.TryAdd(0xA2, typeof(RequestPlayerInGame));
            _serverPackets.TryAdd(0x03, typeof(RequestPlayersOnline));
        }

        public void HandlePacket(Packet packet, GameServerClient gameServerClient)
        {
            byte opCode = packet.FirstOpcode();
            LoggerManager.Info($"Received packet with Opcode:{opCode:X2}");

            PacketBase loginClientPacket = (PacketBase)Activator.CreateInstance(_serverPackets[opCode], _serviceProvider, packet, gameServerClient);
            loginClientPacket?.Execute();
        }
    }
}
