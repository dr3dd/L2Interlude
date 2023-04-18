using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.SkillData.Effects;

public class MagicAttack : Effect
{
    private readonly double _magicDamage;
    public MagicAttack(IReadOnlyList<string> param, SkillDataModel skillDataModel)
    {
        _magicDamage = Utility.ToFloat(param[1]);
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
            
        var isMagicalCriticalHit = false;
        var isBss = false;
        var isSs = false;
        var damage = CalculateSkill.CalcMagicDam(currentInstance, targetInstance, (int)_magicDamage, isSs, isBss, isMagicalCriticalHit);
        targetInstance.CharacterStatus().DecreaseCurrentHp(damage);
        await SendStatusUpdate(targetInstance);
        //LoggerManager.Info($"Magic Attack: {damage}"); debug
        await SendDamageMessage(currentInstance, targetInstance, damage, isMagicalCriticalHit);
    }

    private async Task SendDamageMessage(Character currentInstance, Character targetInstance, double damage,
        bool isMagicalCriticalHit)
    {
        await CharacterMessage.SendDamageMessageAsync(currentInstance, targetInstance, damage, isMagicalCriticalHit);
    }

}