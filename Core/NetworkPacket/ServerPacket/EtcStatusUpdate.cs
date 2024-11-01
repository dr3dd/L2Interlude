using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class EtcStatusUpdate : Network.ServerPacket
    {
        private readonly PlayerInstance _player;

        public EtcStatusUpdate(PlayerInstance playerInstance)
        {
            _player = playerInstance;
        }
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xF3);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0); //writeD(_player.getWeightPenalty()); // 1-4 weight penalty, lvl (1=50%, 2=66.6%, 3=80%, 4=100%)
            await WriteIntAsync(0); //writeD(_player.isInRefusalMode() || _player.isChatBanned() ? 1 : 0); // 1 = block all chat
            await WriteIntAsync(0); // 1 = danger area
            await WriteIntAsync(0); // writeD(Math.min(_player.getExpertisePenalty() + _player.getMasteryPenalty() + _player.getMasteryWeapPenalty(), 1)); // 1 = grade penalty
            await WriteIntAsync(0); // writeD(_player.getCharmOfCourage() ? 1 : 0); // 1 = charm of courage (no xp loss in siege..)
            await WriteIntAsync(0); // writeD(_player.getDeathPenaltyBuffLevel()); // 1-15 death penalty, lvl (combat ability decreased due to death)
        }
    }
}