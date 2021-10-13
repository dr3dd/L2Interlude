using System;
using System.Threading.Tasks;
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
        private readonly EffectInit _effectInit;
        
        public PlayerEffectTest()
        {
            _playerInstance = GetPlayerInstance();
            _dataInit = _playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
            _effectInit = _playerInstance.ServiceProvider.GetRequiredService<EffectInit>();
        }
        
        [Fact]
        public async Task AddSongOfWindTest()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_song_of_wind");
            var effects = skillData.Effects;
            foreach (var (key, value) in effects)
            {
                var effect = (Effect)Activator.CreateInstance(_effectInit.GetEffectHandler(key));
                await effect.Process(_playerInstance);
            }
            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(146, speed);
        }
        
        [Fact]
        public async Task AddWindWalkTest()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_wind_walk2");
            var effects = skillData.Effects;
            foreach (var (key, value) in effects)
            {
                var effect = (Effect)Activator.CreateInstance(_effectInit.GetEffectHandler(key));
                await effect.Process(_playerInstance);
            }

            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(159, speed);
        }

        [Fact]
        public async Task AddWindWalkAndSongOfWindTest()
        {
            SkillDataModel skillDataSongWind = _dataInit.GetSkillByName("s_song_of_wind");
            var songOfWindEffects = skillDataSongWind.Effects;
            foreach (var (key, value) in songOfWindEffects)
            {
                var effect = (Effect)Activator.CreateInstance(_effectInit.GetEffectHandler(key));
                await effect.Process(_playerInstance);
            }
            
            SkillDataModel skillDataWindWalk = _dataInit.GetSkillByName("s_wind_walk2");
            var windWalkEffects = skillDataWindWalk.Effects;
            foreach (var (key, value) in windWalkEffects)
            {
                var effect = (Effect)Activator.CreateInstance(_effectInit.GetEffectHandler(key));
                await effect.Process(_playerInstance);
            }
            
            var speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            Assert.Equal(179, speed);
        }
    }
}