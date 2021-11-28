using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.SkillData.Effects
{
    public class PMaxHp : Effect
    {
        private readonly int _maxHp;
        private readonly int _abnormalTime;

        public PMaxHp(IReadOnlyList<string> param, SkillDataModel skillDataModel)
        {
            _maxHp = Convert.ToInt32(param[1]);
            _abnormalTime = skillDataModel.AbnormalTime;
            SkillDataModel = skillDataModel;
            IsModPer = (param[2] == "per");
        }
        public override async Task Process(PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            
            var effectResult = CanPlayerUseSkill(playerInstance, targetInstance);
            if (effectResult.IsNotValid)
            {
                await playerInstance.SendPacketAsync(new SystemMessage(effectResult.SystemMessageId));
                return;
            }
            await StartEffectTask(_abnormalTime * 1000, targetInstance);
        }
        
        public int GetMaxHp()
        {
            return _maxHp;
        }
    }
}