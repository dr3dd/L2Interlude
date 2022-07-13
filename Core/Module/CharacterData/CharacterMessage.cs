using System.Threading.Tasks;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData
{
    public static class CharacterMessage
    {
        public static void SendMessageToPlayerByNpc(Character character, int npcId, int damage)
        {
            var sm = new SystemMessage(SystemMessageId.S1GaveYouS2Dmg);
            sm.AddNpcName(npcId);
            sm.AddNumber(damage);
            character.SendPacketAsync(sm);
        }
        
        public static void SendMessageToPlayer(Character character, string name, int damage)
        {
            var sm = new SystemMessage(SystemMessageId.S1GaveYouS2Dmg);
            sm.AddString(name);
            sm.AddNumber(damage);
            character.SendPacketAsync(sm);		
        }
        
        public static async Task SendMessageToPlayerAsync(Character character, SkillDataModel skill, int skillId)
        {
            var sm = new SystemMessage(SystemMessageId.UseS1);
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
            await character.SendPacketAsync(sm);
        }

        public static async Task SendDamageMessageAsync(Character character, Character targetInstance, double damage, bool isMagicalCritical,
            bool pcrit = false, bool miss = false)
        {
            // Check if hit is missed
            if (miss)
            {
                await character.SendPacketAsync(new SystemMessage(SystemMessageId.MissedTarget));
                return;
            }
            // Check if hit is critical
            if (pcrit)
            {
                await character.SendPacketAsync(new SystemMessage(SystemMessageId.CriticalHit));
            }
            if (isMagicalCritical)
            {
                await character.SendPacketAsync(new SystemMessage(SystemMessageId.CriticalHitMagic));
            }
            if (character != targetInstance)
            {
                var sm = new SystemMessage(SystemMessageId.YouDidS1Dmg);
                sm.AddNumber(damage);
                await character.SendPacketAsync(sm);
            }
        }
        
        public static async Task SendMessageAsync(Character character, string message)
        {
            var sm = new SystemMessage(SystemMessageId.S1);
            sm.AddString(message);
            await character.SendPacketAsync(sm);
        }
    }
}