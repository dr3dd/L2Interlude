using System.Linq;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.SkillData.Effects;
using Microsoft.Extensions.DependencyInjection;
using Test.NpcAi;
using Xunit;

namespace Test;

public class PlayerPhysicalSkillTest : IClassFixture<PlayerInstanceFixture>, IClassFixture<NpcInstanceFixture>
{
    private readonly ItemDataInit _itemDataInit;
    private readonly PlayerInstance _playerInstance;
    private readonly SkillDataInit _dataInit;
    private readonly NpcInstanceFixture _npcInstanceFixture;
    

    public PlayerPhysicalSkillTest(PlayerInstanceFixture playerInstanceFixture, NpcInstanceFixture npcInstanceFixture)
    {
        _npcInstanceFixture = npcInstanceFixture;
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _itemDataInit = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
        _dataInit = _playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
    }
    
    [Fact]
    public void UseMortalBlowTest()
    {
        var npcInstance = _npcInstanceFixture.GetNpcInstance("tutorial_gremlin");
        var skillArmorMastery = _dataInit.GetSkillByName("s_mortal_blow11");
        var armorMasteryEffects = skillArmorMastery.Effects;
        var effect =  armorMasteryEffects.FirstOrDefault();
        var fatalBlow = (FatalBlow) effect.Value;

        var damage = CalculateSkill.CalcPhysSkillDam(npcInstance, fatalBlow.GetDamage());
        
        Assert.True( damage is >= 130 and <= 150);
    }
    
    [Fact]
    public void UsePowerStrikeTest()
    {
        var npcInstance = _npcInstanceFixture.GetNpcInstance("tutorial_gremlin");
        var skillArmorMastery = _dataInit.GetSkillByName("s_power_strike11");
        var armorMasteryEffects = skillArmorMastery.Effects;
        var effect =  armorMasteryEffects.FirstOrDefault();
        var powerStrike = (PAttackOverHit) effect.Value;

        var damage = CalculateSkill.CalcPhysSkillDam(npcInstance, powerStrike.GetDamage());
        
        Assert.True( damage is >= 40 and <= 50);
    }
}