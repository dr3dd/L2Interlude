using System.Linq;
using System.Threading.Tasks;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData;

public class NpcLearnSkill
{

    private readonly NpcInstance _npcInstance;
    private readonly SkillAcquireInit _skillAcquireInit;
    public NpcLearnSkill(NpcInstance npcInstance)
    {
        _npcInstance = npcInstance;
        _skillAcquireInit = _npcInstance.ServiceProvider.GetRequiredService<SkillAcquireInit>();
    }
    
    public async Task LearnSkillRequest(PlayerInstance playerInstance)
    {
        var talker = new Talker(playerInstance);
        if (_npcInstance.NpcAi().GetDefaultNpc() is GuildCoach guildCoach)
        {
            await guildCoach.LearnSkillRequested(talker);
        }
    }

    public async Task ShowSkillList(PlayerInstance player)
    {
        player.LastTalkedNpc = _npcInstance;

        var acquiredSkills = await player.PlayerSkill().GetPlayerSkills();
        AcquireSkillList acquireSkillList = new AcquireSkillList(AcquireSkillList.SkillType.Usual);
        var skillList = _skillAcquireInit
            .GetSkillAcquireListByClassKey(player.PlayerCharacterInfo().ClassName)
            .Where(sam => sam.LevelToGetSkill <= player.PlayerStatus().Level);
        foreach (var skillAcquire in skillList)
        {
            var currentSkillLevel = 1;
            var skillDataModel = Initializer.SkillDataInit().GetSkillByName(skillAcquire.SkillName);
            if (acquiredSkills.ContainsKey(skillDataModel.SkillId))
            {
                if (acquiredSkills[skillDataModel.SkillId].Level >= skillDataModel.Level)
                {
                    continue;
                }
                var acquiredLevel = acquiredSkills[skillDataModel.SkillId].Level;
                currentSkillLevel = acquiredLevel + 1;
            }
            acquireSkillList.AddSkill(skillDataModel.SkillId, currentSkillLevel,
                skillAcquire.LevelUpSp, skillAcquire.LevelToGetSkill, 0);
                
        }
        await player.SendPacketAsync(acquireSkillList);
    }
}