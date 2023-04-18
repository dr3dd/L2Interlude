using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.SkillData.Effects;

public class PMaxHp : Effect
{
    private readonly double _maxHp;
    private readonly int _abnormalTime;

    public PMaxHp(IReadOnlyList<string> param, SkillDataModel skillDataModel)
    {
        _maxHp = Utility.ToDouble(param[1]);
        _abnormalTime = skillDataModel.AbnormalTime;
        SkillDataModel = skillDataModel;
        IsModPer = (param[2] == "per");
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
        
    public int GetMaxHp()
    {
        return (int)_maxHp;
    }
}