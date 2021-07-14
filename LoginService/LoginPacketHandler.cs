using System;
using System.Collections.Concurrent;
using L2Logger;
using LoginService.Network.ClientPackets;
using Network;

namespace LoginService
{
    public class LoginPacketHandler
    {
        private readonly IServiceProvider _serviceProvider;
        
        private readonly ConcurrentDictionary<byte, Type> _clientPackets;

        public LoginPacketHandler(IServiceProvider serviceProvider)
        {
            _clientPackets = new ConcurrentDictionary<byte, Type>();
            _serviceProvider = serviceProvider;

            _clientPackets.TryAdd(0x00, typeof(RequestAuthLogin));
            _clientPackets.TryAdd(0x02, typeof(RequestServerLogin));
            _clientPackets.TryAdd(0x05, typeof(RequestServerList));
            _clientPackets.TryAdd(0x07, typeof(AuthGameGuard));
        }

        public void HandlePacket(Packet packet, LoginClient client)
        {
            byte opCode = packet.FirstOpcode();
            LoggerManager.Info($"Received packet with Opcode:{opCode:X2} for State:{client.State}");

            PacketBase loginClientPacket = (PacketBase)Activator.CreateInstance(_clientPackets[opCode], _serviceProvider, packet, client);
            loginClientPacket?.Execute();
        }
    }
}
