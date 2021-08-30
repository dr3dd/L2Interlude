using System.Linq;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.SkillData.Effects;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test
{
    public class PlayerEffectTest : PlayerBase
    {
        private readonly SkillDataInit _dataInit;
        private readonly PlayerInstance _playerInstance;
        
        public PlayerEffectTest()
        {
            _playerInstance = GetPlayerInstance();
            _dataInit = _playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
        }
        
        [Fact]
        public void AddSongOfWindTest()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_song_of_wind");
            Effect effect = skillData.Effects.First().Key;
            effect.SkillDataModel = skillData;
            effect.StartEffectTask(10000, _playerInstance);

            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(156, speed);
        }
        
        [Fact]
        public void AddWindWalkTest()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_wind_walk2");
            Effect effect = skillData.Effects.First().Key;
            effect.SkillDataModel = skillData;
            effect.StartEffectTask(10000, _playerInstance);

            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(156, speed);
        }

        [Fact]
        public void AddWindWalkAndSongOfWindTest()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_song_of_wind");
            Effect effect = skillData.Effects.First().Key;
            effect.SkillDataModel = skillData;
            effect.StartEffectTask(10000, _playerInstance);
            
            SkillDataModel skillDataWindWalk = _dataInit.GetSkillByName("s_wind_walk2");
            Effect effectWindWalk = skillDataWindWalk.Effects.First().Key;
            effectWindWalk.SkillDataModel = skillDataWindWalk;
            effectWindWalk.StartEffectTask(10000, _playerInstance);
            
            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(186, speed);
        }
    }
}