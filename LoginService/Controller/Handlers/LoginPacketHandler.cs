using System;
using System.Collections.Concurrent;
using Config;
using L2Logger;
using LoginService.Network.ClientPackets;
using Microsoft.Extensions.DependencyInjection;
using Network;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LoginService.Controller.Handlers
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

        public void HandlePacket(Packet packet, LoginServiceController client)
        {
            var config = _serviceProvider.GetService<LoginConfig>();
            try
            {
                byte opCode = packet.FirstOpcode();
                if (config.DebugConfig.ShowHeaderPacket && config.DebugConfig.ShowPacketFromClient)
                { // show packet header  
                    LoggerManager.Debug($"LoginPacketHandler: CLIENT>>LS header: [{opCode.ToString("x2")}] size: [{packet.GetBuffer().Length}] for State:{client.State}");
                }

                PacketBase loginClientPacket = null;

                if (!_clientPackets.ContainsKey(opCode))
                {
                    LoggerManager.Warn($"LoginPacketHandler: Not found packet FirstOpcode={opCode.ToString("x2")}");
                    printPacketBody(packet);
                    return;
                }

                loginClientPacket = (PacketBase)Activator.CreateInstance(_clientPackets[opCode], _serviceProvider, packet, client);

                if (config.DebugConfig.ShowNamePacket && config.DebugConfig.ShowPacketFromClient)
                {
                    LoggerManager.Debug($"LoginPacketHandler: CLIENT>>LS name: {loginClientPacket.GetType().Name}");
                }
                if (config.DebugConfig.ShowPacket && config.DebugConfig.ShowPacketFromClient) // show packet header & body 
                {
                    printPacketBody(packet);
                }

                loginClientPacket?.Execute();
            }
            catch (Exception ex)
            {
                LoggerManager.Error("LoginPacketHandler: " + ex.StackTrace);
            }
        }

        private void printPacketBody(Packet packet)
        {
            string str = "";
            foreach (byte b in packet.GetBuffer())
                str += b.ToString("x2") + " ";
            LoggerManager.Debug($"LoginPacketHandler: CLIENT>>LS body: [ {str} ]");
        }
    }
}
