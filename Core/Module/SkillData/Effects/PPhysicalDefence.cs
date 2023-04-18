using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.SkillData.Effects;

public class PPhysicalDefence : Effect
{
    private readonly double _defence;
    private readonly int _abnormalTime;

    public PPhysicalDefence(IReadOnlyList<string> param, SkillDataModel skillDataModel)
    {
        SkillDataModel = skillDataModel;
        var reverse = param.Reverse().ToArray();
        _defence = Utility.ToDouble(reverse[1]);
        _abnormalTime = skillDataModel.AbnormalTime;
        IsModPer = (reverse[0] == "per");
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
        
    public int GetPhysicalDefence()
    {
        return (int) _defence;
    }
}