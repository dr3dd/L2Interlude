using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.SkillData.Effects;

public class PPhysicalAttack : Effect
{
    private readonly double _attackDamage;
    private readonly int _abnormalTime;

    public PPhysicalAttack(IReadOnlyList<string> param, SkillDataModel skillDataModel)
    {
        var reverse = param.Reverse().ToArray();
        _attackDamage = Utility.ToDouble(reverse[1]);
        IsModPer = (reverse[0] == "per");
            
        _abnormalTime = skillDataModel.AbnormalTime;
        SkillDataModel = skillDataModel;
    }
    
    public override async Task Process(Character currentInstance, Character targetInstance)
    {
        var effectResult = CanPlayerUseSkill(currentInstance, targetInstance);
        if (effectResult.IsNotValid)
        {
            await currentInstance.SendPacketAsync(new SystemMessage(effectResult.SystemMessageId));
            return;
        }
        await StartEffectTask(_abnormalTime * 1000, targetInstance);
    }

    public int GetAttackDamage()
    {
        return (int)_attackDamage;
    }
}