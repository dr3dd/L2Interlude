using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.SkillData.Effects
{
    public class PPhysicalAttack : Effect
    {
        private readonly int _attackDamage;
        private readonly int _abnormalTime;
        
        public PPhysicalAttack(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _attackDamage = Convert.ToInt32(param[2]);
            _abnormalTime = skillDataModel.AbnormalTime;
            SkillDataModel = skillDataModel;
            IsModPer = (param[3] == "per");
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
            return _attackDamage;
        }
    }
}