using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestUnEquipItem : PacketBase
    {
        private readonly int _slot;
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerInventory _playerInventory;
        
        public RequestUnEquipItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) :
            base(serviceProvider)
        {
            _slot = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerInventory = _playerInstance.PlayerInventory();
        }

        public override Task Execute()
        {
            //todo there should be added logic for validation
            //todo Prevent player from unequipping items in special conditions like Stun, Confuse, etc..
            
            _playerInventory.UnEquipItemInBodySlot(_slot);
            
            
            throw new NotImplementedException();
        }
    }
}