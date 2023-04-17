using Core.Module.NpcData;
using Xunit;

namespace Test.NpcAi.Warrior;

public class TutorialGremlin : IClassFixture<NpcInstanceFixture>
{
    private readonly NpcInstance _npcInstance;
    private readonly NpcStat _npcStat;

    public TutorialGremlin(NpcInstanceFixture npcInstanceFixture)
    {
        _npcInstance = npcInstanceFixture.GetNpcInstance("tutorial_gremlin");
        _npcStat = _npcInstance.GetStat();
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
}