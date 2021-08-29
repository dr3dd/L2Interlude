using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Core.Module.SkillData;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerEffect
    {
        private PlayerInstance _playerInstance;
        private readonly ConcurrentDictionary<string, SkillDataModel> _currentEffects;
        private readonly SkillDataInit _dataInit;
        private readonly EffectInit _effectInit;
        private CancellationTokenSource _cts;
        public PlayerEffect(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _currentEffects = new ConcurrentDictionary<string, SkillDataModel>();
            _dataInit = playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
            _effectInit = playerInstance.ServiceProvider.GetRequiredService<EffectInit>();

            //debug
            AddWindWalk2();
            AddSongOfWind();
        }

        private void AddSongOfWind()
        {
            SkillDataModel skillData = _dataInit.GetSkillByName("s_song_of_wind");
            _currentEffects.TryAdd("s_song_of_wind", skillData);
        }

        private void AddWindWalk2()
        {
            _cts = new CancellationTokenSource();
            SkillDataModel skillData = _dataInit.GetSkillByName("s_wind_walk2");
            _currentEffects.TryAdd("s_wind_walk2", skillData);
        }

        public IEnumerable<SkillDataModel> GetEffects()
        {
            return _currentEffects.Values;
        }
    }
}