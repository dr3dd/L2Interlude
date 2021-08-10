using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket.CharacterPacket
{
    public class CharacterSelected : PacketBase
    {
        private readonly GameServiceController _controller;
        
        private readonly int _charSlot;
        private int _unk1; // new in C4
        private int _unk2;
        private int _unk3;
        private int _unk4;
        private readonly int _objectId; 
        
        public CharacterSelected(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _objectId = serviceProvider.GetRequiredService<ObjectIdInit>().NextObjectId();
            _controller = controller;
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
                PlayerInstance playerInstance = await PlayerInstance.Load(player.CharacterId, ServiceProvider);
                _controller.GameServiceHelper.CurrentPlayer = playerInstance;
                playerInstance.Controller = _controller;
                playerInstance.ObjectId = _objectId;
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