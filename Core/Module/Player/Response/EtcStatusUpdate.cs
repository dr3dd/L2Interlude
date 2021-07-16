using Network;

namespace Core.Module.Player.Response
{
    internal sealed class EtcStatusUpdate : ServerPacket
    {
        private readonly PlayerInstance _player;

        public EtcStatusUpdate(PlayerInstance playerInstance)
        {
            _player = playerInstance;
        }
        
        public override void Write()
        {
            WriteByte(0xF3);
            WriteInt(0x00);
            WriteInt(0); //writeD(_player.getWeightPenalty()); // 1-4 weight penalty, lvl (1=50%, 2=66.6%, 3=80%, 4=100%)
            WriteInt(0); //writeD(_player.isInRefusalMode() || _player.isChatBanned() ? 1 : 0); // 1 = block all chat
            WriteInt(0); // 1 = danger area
            WriteInt(0); // writeD(Math.min(_player.getExpertisePenalty() + _player.getMasteryPenalty() + _player.getMasteryWeapPenalty(), 1)); // 1 = grade penalty
            WriteInt(0); // writeD(_player.getCharmOfCourage() ? 1 : 0); // 1 = charm of courage (no xp loss in siege..)
            WriteInt(0); // writeD(_player.getDeathPenaltyBuffLevel()); // 1-15 death penalty, lvl (combat ability decreased due to death)
        }
    }
}