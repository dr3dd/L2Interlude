using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;
using Network;

namespace Core.NetworkPacket.ClientPacket.CharacterPacket
{
    public class CharacterSelected : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly IServiceProvider _serviceProvider;
        
        private readonly int _charSlot;
        private int _unk1; // new in C4
        private int _unk2;
        private int _unk3;
        private int _unk4;
        
        public CharacterSelected(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _serviceProvider = serviceProvider;
            
            _charSlot = packet.ReadInt();
            _unk1 = packet.ReadShort();
            _unk2 = packet.ReadInt();
            _unk3 = packet.ReadInt();
            _unk4 = packet.ReadInt();
        }

        public override async Task Execute()
        {
            try
            {
                var player = _controller.GameServiceHelper.GetCharacterBySlot(_charSlot);
                PlayerInstance playerInstance = await PlayerInstance.Load(player.CharacterId);
                _controller.GameServiceHelper.CurrentPlayer = playerInstance;
                playerInstance.Controller = _controller;
                await _controller.SendPacketAsync(new CharSelected(playerInstance, _controller.SessionKey.PlayOkId1));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
    }
}