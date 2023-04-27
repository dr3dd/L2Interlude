using System.Threading.Tasks;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.SkillData;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.NpcAi.Warrior;

public class TutorialGremlin : IClassFixture<NpcInstanceFixture>, IClassFixture<PlayerInstanceFixture>
{
    private readonly NpcInstance _npcInstance;
    private readonly PlayerInstance _playerInstance;
    private readonly NpcStat _npcStat;
    private readonly SkillDataInit _dataInit;

    public TutorialGremlin(NpcInstanceFixture npcInstanceFixture, PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _npcInstance = npcInstanceFixture.GetNpcInstance("tutorial_gremlin");
        _npcStat = _npcInstance.GetStat();
        _dataInit = _npcInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
    }

    [Fact]
    public void CheckGremlinStatsTest()
    {
        var physicalAttack = _npcInstance.CharacterCombat().GetPhysicalAttack();
        var magicalAttack = _npcInstance.CharacterCombat().GetMagicalAttack();
        var physicalDefence = _npcInstance.CharacterCombat().GetPhysicalDefence();
        var magicalDefence = _npcInstance.CharacterCombat().GetMagicalDefence();
        var physicalAttackSpeed = _npcInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var accuracy = _npcInstance.CharacterCombat().GetAccuracy();
        var criticalRate = _npcInstance.CharacterCombat().GetCriticalRate();
        var evasion = _npcInstance.CharacterCombat().GetEvasion();
        var hpRegen = _npcInstance.CharacterBaseStatus().GetHpRegenRate();
        var mpRegen = _npcInstance.CharacterBaseStatus().GetMpRegenRate();

        var lowSpeed = _npcInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(19.799999237060547, lowSpeed); //Character Low Speed
        Assert.Equal(1, _npcStat.Level); //Check Level
        Assert.Equal(62, _npcInstance.CharacterBaseStatus().GetMaxHp()); //Check MaxHp
        Assert.Equal(49, _npcInstance.CharacterBaseStatus().GetMaxMp()); //Check MaxMp
        
        Assert.Equal(8, physicalAttack); //Check PhysicalAttack
        Assert.Equal(6, magicalAttack); //Check MagicalAttack
        Assert.Equal(39, physicalDefence); //Check PhysicalDefence
        Assert.Equal(29, magicalDefence); //Check MagicalDefence
        Assert.Equal(272, physicalAttackSpeed); //Check PhysicalAttackSpeed
        Assert.Equal(30, accuracy); //Check Accuracy
        Assert.Equal(34, evasion); //Check Evasion
        Assert.Equal(88, criticalRate); //Check Critical Rate
        Assert.Equal(4.989999771118164, hpRegen); //Check Hp Regen Rate
        Assert.Equal(1.0099999904632568, mpRegen); //Check Mp Regen Rate
    }

    [Fact]
    public async Task AddGremlinWindWalkTest()
    {
        var skillData = _dataInit.GetSkillByName("s_wind_walk2");
        var effects = skillData.Effects;
        foreach (var (key, value) in effects)
        {
            await value.Process(_playerInstance, _npcInstance);
        }
        _npcInstance.CharacterMovement().CharacterMovementStatus().SetGroundHigh();
        var highSpeed = _npcInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(82.5, highSpeed); //Check High Speed
        
        _npcInstance.CharacterMovement().CharacterMovementStatus().SetGroundLow();
        var lowSpeed = _npcInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(52.79999923706055, lowSpeed); //Check Low Speed
    }
}