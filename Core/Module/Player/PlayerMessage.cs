using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player
{
    public sealed class PlayerMessage
    {
        private readonly PlayerInstance _playerInstance;
        public PlayerMessage(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        public void SendMessageToPlayerByNpc(int npcId, int damage)
        {
            SystemMessage sm = new SystemMessage(SystemMessageId.S1GaveYouS2Dmg);
            sm.AddNpcName(npcId);
            sm.AddNumber(damage);
            _playerInstance.SendPacketAsync(sm);		
        }
        
        public void SendMessageToPlayer(string name, int damage)
        {
            SystemMessage sm = new SystemMessage(SystemMessageId.S1GaveYouS2Dmg);
            sm.AddString(name);
            sm.AddNumber(damage);
            _playerInstance.SendPacketAsync(sm);		
        }
        
        public async Task SendMessageToPlayerAsync(SkillDataModel skill, int skillId)
        {
            SystemMessage sm = new SystemMessage(SystemMessageId.UseS1);
            switch (skillId)
            {
                case 2005:
                    sm.AddItemName(728);
                    break;
                case 2003:
                    sm.AddItemName(726);
                    break;
                case 2166 when (skill.Level == 2):
                    sm.AddItemName(5592);
                    break;
                case 2166 when (skill.Level == 1):
                    sm.AddItemName(5591);
                    break;
                default:
                    sm.AddSkillName(skillId, skill.Level);
                    break;
            }
            await _playerInstance.SendPacketAsync(sm);
        }

        public async Task SendDamageMessageAsync(Character targetInstance, double damage, bool isMagicalCritical,
            bool pcrit = false, bool miss = false)
        {
            // Check if hit is missed
            if (miss)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.MissedTarget));
                return;
            }
            // Check if hit is critical
            if (pcrit)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.CriticalHit));
            }
            if (isMagicalCritical)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.CriticalHitMagic));
            }
            if (_playerInstance != targetInstance)
            {
                SystemMessage sm = new SystemMessage(SystemMessageId.YouDidS1Dmg);
                sm.AddNumber(damage);
                await _playerInstance.SendPacketAsync(sm);
            }
        }
        
        public async Task SendMessageAsync(string message)
        {
            SystemMessage sm = new SystemMessage(SystemMessageId.S1);
            sm.AddString(message);
            await _playerInstance.SendPacketAsync(sm);
        }
    }
}