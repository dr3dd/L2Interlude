using System;
using System.Collections.Concurrent;
using Config;
using L2Logger;
using LoginService.Network;
using LoginService.Network.ClientPackets;
using LoginService.Network.GameServerPackets;
using Microsoft.Extensions.DependencyInjection;
using Network;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LoginService.Controller.Handlers
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
            var config = _serviceProvider.GetService<LoginConfig>();
            try
            {
                byte opCode = packet.FirstOpcode();
                if (config.DebugConfig.ShowHeaderPacket && config.DebugConfig.ShowPacketToAuth)
                { // show packet header  
                    LoggerManager.Debug($"GameServerPacketHandler: GAME>>AUTH header: [{opCode.ToString("x2")}] size: [{packet.GetBuffer().Length}]");
                }

                PacketBase loginClientPacket = null;

                if (!_serverPackets.ContainsKey(opCode))
                {
                    LoggerManager.Warn($"GameServerPacketHandler: Not found packet FirstOpcode={opCode.ToString("x2")}");
                    printPacketBody(packet);
                    return;
                }

                loginClientPacket = (PacketBase)Activator.CreateInstance(_serverPackets[opCode], _serviceProvider, packet, gameServerClient);

                if (config.DebugConfig.ShowNamePacket && config.DebugConfig.ShowPacketToAuth)
                {
                    LoggerManager.Debug($"GameServerPacketHandler: GAME>>AUTH name: {loginClientPacket.GetType().Name}");
                }
                if (config.DebugConfig.ShowPacket && config.DebugConfig.ShowPacketToAuth) // show packet header & body 
                {
                    printPacketBody(packet);
                }

                loginClientPacket?.Execute();
            }
            catch (Exception ex)
            {
                LoggerManager.Error("GameServerPacketHandler: " + ex.StackTrace);
            }
        }

        private void printPacketBody(Packet packet)
        {
            string str = "";
            foreach (byte b in packet.GetBuffer())
                str += b.ToString("x2") + " ";
            LoggerManager.Debug($"GameServerPacketHandler: GAME>>AUTH body: [ {str} ]");
        }
    }
}
