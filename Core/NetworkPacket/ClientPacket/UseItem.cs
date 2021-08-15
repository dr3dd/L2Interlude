using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.ItemData;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class UseItem : PacketBase
    {
        private readonly int _userItemId;
        private readonly PlayerInstance _playerInstance;
        
        public UseItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _userItemId = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            ItemInstance item = GetItemByUserItemId();
            await _playerInstance.PlayerUseItem().UseItemAsync(item);
        }

        private ItemInstance GetItemByUserItemId()
        {
            return _playerInstance.PlayerInventory().GetItemInstance(_userItemId);
        }
    }
}