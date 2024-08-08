using System;
using System.Collections.Concurrent;
using Config;
using Core.NetworkPacket.ClientPacket;
using Core.NetworkPacket.ClientPacket.CharacterPacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.Controller.Handlers
{
    public class GameServicePacketHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<byte, Type> _clientPackets;
        private readonly ConcurrentDictionary<short, Type> _clientPacketsD0;

        public GameServicePacketHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _clientPackets = new ConcurrentDictionary<byte, Type>();
            _clientPacketsD0 = new ConcurrentDictionary<short, Type>();
            
            _clientPackets.TryAdd(0x00, typeof(ProtocolVersion));
            _clientPackets.TryAdd(0x01, typeof(MoveBackwardToLocation));
            _clientPackets.TryAdd(0x03, typeof(EnterWorld));
            _clientPackets.TryAdd(0x04, typeof(ActionRequest));
            _clientPackets.TryAdd(0x08, typeof(AuthLogin));
            _clientPackets.TryAdd(0x09, typeof(Logout));
            _clientPackets.TryAdd(0x0A, typeof(AttackRequest));
            _clientPackets.TryAdd(0x11, typeof(RequestUnEquipItem));
            _clientPackets.TryAdd(0x14, typeof(UseItem));
            _clientPackets.TryAdd(0x1F, typeof(RequestBuyItem));
            _clientPackets.TryAdd(0x2F, typeof(RequestMagicSkillUse));
            _clientPackets.TryAdd(0x3F, typeof(RequestSkillList));
            _clientPackets.TryAdd(0x0E, typeof(NewCharacter));
            _clientPackets.TryAdd(0x0B, typeof(CharacterCreate));
            _clientPackets.TryAdd(0x0D, typeof(CharacterSelected));
            _clientPackets.TryAdd(0x6b, typeof(RequestAcquireSkillInfo));
            _clientPackets.TryAdd(0x6c, typeof(RequestAcquireSkill));
            _clientPackets.TryAdd(0x0F, typeof(RequestItemList));
            _clientPackets.TryAdd(0x9D, typeof(RequestSkillCoolTime));
            _clientPackets.TryAdd(0x21, typeof(RequestBypass));
            _clientPackets.TryAdd(0x30, typeof(Appearing));
            _clientPackets.TryAdd(0x36, typeof(CannotMoveAnymore));
            _clientPackets.TryAdd(0x37, typeof(RequestTargetCancel));
            _clientPackets.TryAdd(0x45, typeof(RequestActionUse));
            _clientPackets.TryAdd(0x46, typeof(RequestRestart));
            _clientPackets.TryAdd(0x48, typeof(ValidatePosition));
            _clientPackets.TryAdd(0x59, typeof(RequestDestroyItem));
            _clientPackets.TryAdd(0xCD, typeof(RequestShowMiniMap));
            
            _clientPacketsD0.TryAdd(0x08, typeof(RequestManorList));
            _clientPacketsD0.TryAdd(0x22, typeof(RequestCursedWeaponList));
            _clientPacketsD0.TryAdd(0x23, typeof(RequestCursedWeaponLocation));
        }
        
        public void HandlePacket(Packet packet, GameServiceController controller)
        {
            var config = _serviceProvider.GetService<GameConfig>();
            try
            {
                byte opCode = packet.FirstOpcode();

                if (config.DebugConfig.ShowHeaderPacket) // показывать заголовок
                {
                    LoggerManager.Debug($"GameServicePacketHandler: CLIENT>>GS header: [{opCode.ToString("x2")}] size: [{packet.GetBuffer().Length}]");
                }

                PacketBase packetBase = null;
                if (opCode != 0xD0 && _clientPackets.ContainsKey(opCode))
                {
                    packetBase = (PacketBase)Activator.CreateInstance(_clientPackets[opCode], _serviceProvider, packet, controller);
                }
                else if (opCode == 0xD0)
                {
                    short opCode2 = packet.ReadShort();
                    if (config.DebugConfig.ShowHeaderPacket) // показывать заголовок
                    {
                        LoggerManager.Debug($"GameServicePacketHandler: CLIENT>>GS header: SecondOpcode [{opCode2.ToString("x2")}]");
                    }

                    if (_clientPacketsD0.ContainsKey(opCode2))
                    {
                        packetBase = (PacketBase)Activator.CreateInstance(_clientPacketsD0[opCode2], _serviceProvider, packet, controller);
                    }
                    else
                    {
                        LoggerManager.Warn($"GameServicePacketHandler: Not found packet FirstOpcode={opCode.ToString("x2")} SecondOpcode={opCode2.ToString("x2")}");
                        printPacketBody(packet);
                        return;
                    }
                }
                else
                {
                    LoggerManager.Warn($"GameServicePacketHandler: Not found packet FirstOpcode={opCode.ToString("x2")}");
                    printPacketBody(packet);
                    return;
                }

                if (controller.IsDisconnected)
                {
                    return;
                }

                if (packetBase != null && config.DebugConfig.ShowNamePacket)
                {
                    LoggerManager.Debug($"GameServicePacketHandler: CLIENT>>GS name: {packetBase.GetType().Name}");
                }

                if (config.DebugConfig.ShowPacket) // показывать заголовок и содержимое
                {
                    printPacketBody(packet);
                }

                if (packetBase == null)
                {
                    return;
                }

                packetBase.Execute();
            }
            catch (Exception ex)
            {
                LoggerManager.Error("GameServicePacketHandler: " + ex.StackTrace);
            }
        }

        private void printPacketBody(Packet packet)
        {
            string str = "";
            foreach (byte b in packet.GetBuffer())
                str += b.ToString("x2") + " ";
            LoggerManager.Debug($"GameServicePacketHandler: CLIENT>>GS body: [ {str} ]");
        }

    }
}