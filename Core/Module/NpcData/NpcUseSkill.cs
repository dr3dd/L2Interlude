using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using L2Logger;

namespace Core.Module.NpcData
{
    public class NpcUseSkill
    {
        private readonly NpcInstance _npcInstance;
        private static CancellationTokenSource _cts;
        
        public NpcUseSkill(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }
        
        public async Task UseSkill(int pchSkillId, PlayerInstance player)
        {
            var skillName = Initializer.SkillPchInit().GetSkillNameById(pchSkillId);
            
            SkillDataModel skill = Initializer.SkillDataInit().GetSkillByName(skillName);
            // Get the Identifier of the skill
            int skillId = skill.SkillId;
            //todo need add calculator
            short coolTime = (short) skill.SkillCoolTime;
            short hitTime = (short)(skill.SkillHitTime * 1000);
            short reuseDelay = (short)(skill.ReuseDelay * 1000);
            await HandleMagicSkill(skill, player, hitTime);
            await SendToKnownListAsync(skill, player, hitTime, reuseDelay);
            await CharacterMessage.SendMessageToPlayerAsync(player, skill, skillId);
            await player.SendUserInfoAsync();
        }
        
        private async Task HandleMagicSkill(SkillDataModel skill, PlayerInstance target, float hitTime)
        {
            await Task.Run(() =>
            {
                try
                {
                    _cts = new CancellationTokenSource();
                    var effects = skill.Effects;
                    foreach (var (key, value) in effects)
                    {
                        TaskManagerScheduler.ScheduleAtFixed(async () =>
                        {
                            await value.Process(target, target);
                        }, (int)hitTime, _cts.Token);
                    }
                }
                catch (Exception ex)
                {
                    LoggerManager.Error(skill.SkillName + " " + ex.Message);
                }
            });
        }
        
        private async Task SendToKnownListAsync(SkillDataModel skill, PlayerInstance target, float hitTime, float reuseDelay)
        {
            var skillUse = new MagicSkillUse(_npcInstance, target, skill.SkillId, skill.Level, hitTime, reuseDelay);
            await target.SendPacketAsync(skillUse);
            await target.SendToKnownPlayers(skillUse);
        }
    }
}