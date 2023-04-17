using System.Threading.Tasks;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.Module.SkillData;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test;

public class PlayerEffectTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly SkillDataInit _dataInit;
    private readonly PlayerInstance _playerInstance;
    private readonly EffectInit _effectInit;
    
    public PlayerEffectTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _playerInstance.CharacterEffect().RemoveEffects();
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
            await value.Process(_playerInstance, _playerInstance);
        }
        _playerInstance.CharacterMovement().SetRunning();
        var speed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(146.5, speed);
    }

    [Fact]
    public async Task AddWindWalk1Test()
    {
        SkillDataModel skillData = _dataInit.GetSkillByName("s_wind_walk1");
        var effects = skillData.Effects;
        foreach (var (key, value) in effects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
        _playerInstance.CharacterMovement().SetRunning();
        var speed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(146.5, speed);
    }

    [Fact]
    public async Task AddWindWalk2Test()
    {
        SkillDataModel skillData = _dataInit.GetSkillByName("s_wind_walk2");
        var effects = skillData.Effects;
        foreach (var (key, value) in effects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
        _playerInstance.CharacterMovement().SetRunning();
        var speed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(159.5, speed);
    }

    [Fact]
    public async Task AddWindWalkAndSongOfWindTest()
    {
        SkillDataModel skillDataSongWind = _dataInit.GetSkillByName("s_song_of_wind");
        var songOfWindEffects = skillDataSongWind.Effects;
        foreach (var (key, value) in songOfWindEffects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
            
        SkillDataModel skillDataWindWalk = _dataInit.GetSkillByName("s_wind_walk2");
        var windWalkEffects = skillDataWindWalk.Effects;
        foreach (var (key, value) in windWalkEffects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
        _playerInstance.CharacterMovement().SetRunning();
        var speed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(179.5, speed);
    }

    [Fact]
    public async Task AddWeaponMasteryTest()
    {
        var skillWeaponMastery = _dataInit.GetSkillByName("s_weapon_mastery1");
        var weaponMasteryEffects = skillWeaponMastery.Effects;
        foreach (var (key, value) in weaponMasteryEffects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
        
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        Assert.Equal(6, pAtk);
    }
    
    [Fact]
    public async Task AddArmorMasteryTest()
    {
        var skillArmorMastery = _dataInit.GetSkillByName("s_armor_mastery11");
        var armorMasteryEffects = skillArmorMastery.Effects;
        foreach (var (key, value) in armorMasteryEffects)
        {
            await value.Process(_playerInstance, _playerInstance);
        }
        
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(84, pDef);
    }
}