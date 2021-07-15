using System;
using System.Collections.Concurrent;
using Core.Module.LoginService.Response;
using L2Logger;
using Network;

namespace Core.Controller.Handlers
{
    public class LoginServicePacketHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<byte, Type> _loginServerPackets;
        public LoginServicePacketHandler(IServiceProvider serviceProvider)
        {
            _loginServerPackets = new ConcurrentDictionary<byte, Type>();
            _serviceProvider = serviceProvider;
            
            _loginServerPackets.TryAdd(0xA1, typeof(LoginServPingResponse));
            _loginServerPackets.TryAdd(0xA5, typeof(LoginServLoginFail));
            _loginServerPackets.TryAdd(0xA6, typeof(LoginServLoginOk));
            _loginServerPackets.TryAdd(0xA7, typeof(LoginServAcceptPlayer));
            _loginServerPackets.TryAdd(0xA8, typeof(LoginServKickAccount));
        }

        public void HandlePacket(Packet packet, LoginServiceController loginServiceController)
        {
            byte opCode = packet.FirstOpcode();
            LoggerManager.Info($"Received packet with Opcode:{opCode:X2}");
            
            PacketBase packetBase = null;
            
            if (_loginServerPackets.ContainsKey(opCode))
            {
                packetBase = (PacketBase)Activator.CreateInstance(_loginServerPackets[opCode], _serviceProvider, packet, loginServiceController);
            }

            if (packetBase == null)
            {
                throw new ArgumentNullException(nameof(packetBase), $"Packet with opcode: {opCode:X2} doesn't exist in the dictionary.");
            }
            packetBase.Execute();
        }
    }
}
