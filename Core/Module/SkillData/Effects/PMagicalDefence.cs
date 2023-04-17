using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.SkillData.Effects
{
    public class PMagicalDefence : Effect
    {
        private readonly double _defence;
        private readonly int _abnormalTime;
        public PMagicalDefence(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            SkillDataModel = skillDataModel;
            _defence = Convert.ToDouble(param[2]);
            _abnormalTime = skillDataModel.AbnormalTime;
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

        public int GetMagicalDefence()
        {
            return (int)_defence;
        }
    }
}