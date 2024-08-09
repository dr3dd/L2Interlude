using System;
using System.Collections.Concurrent;
using Config;
using Core.NetworkPacket.ClientPacket.LoginServicePacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
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
            var config = _serviceProvider.GetService<GameConfig>();
            try
            {
                byte opCode = packet.FirstOpcode();

                if (config.DebugConfig.ShowHeaderPacket){ // show packet header  
                    LoggerManager.Debug($"LoginServicePacketHandler: AUTH>>GAME header: [{opCode.ToString("x2")}] size: [{packet.GetBuffer().Length}]");
                }
                PacketBase packetBase = null;

                if (!_loginServerPackets.ContainsKey(opCode))
                {
                    LoggerManager.Warn($"LoginServicePacketHandler: Not found packet FirstOpcode={opCode.ToString("x2")}");
                    printPacketBody(packet);
                    return;
                }

                packetBase = (PacketBase)Activator.CreateInstance(_loginServerPackets[opCode], _serviceProvider, packet, loginServiceController);

                if (config.DebugConfig.ShowNamePacket)
                {
                    LoggerManager.Debug($"LoginServicePacketHandler: AUTH>>GAME name: {packetBase.GetType().Name}");
                }
                if (config.DebugConfig.ShowPacket) // show packet header & body 
                {
                    printPacketBody(packet);
                }

                packetBase?.Execute();
            }
            catch (Exception ex)
            {
                LoggerManager.Error("LoginServicePacketHandler: " + ex.StackTrace);
            }
        }

        private void printPacketBody(Packet packet)
        {
            string str = "";
            foreach (byte b in packet.GetBuffer())
                str += b.ToString("x2") + " ";
            LoggerManager.Debug($"LoginServicePacketHandler: AUTH>>GAME body: [ {str} ]");
        }
    }
}
