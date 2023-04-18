using System.Diagnostics;
using System.Threading.Tasks;
using Core.Module.NpcData;
using Xunit;

namespace Test.NpcAi.Teleporter;

public class Valentina : IClassFixture<NpcInstanceFixture>
{
    private readonly NpcInstance _npcInstance;
    private readonly NpcStat _npcStat;
    
    
    public Valentina(NpcInstanceFixture npcInstanceFixture)
    {
        _npcInstance = npcInstanceFixture.GetNpcInstance("valentina");
        _npcStat = _npcInstance.GetStat();
    }

    [Fact]
    public void CheckValentinaStatsTest()
    {
        Assert.Equal(30177, _npcInstance.NpcId);
        Assert.Equal(70, _npcStat.Level);
        Assert.Equal("valentina", _npcStat.NpcAiData.NpcAiName);
        Assert.Equal("valentina001.htm", _npcStat.NpcAiData.FnHi);
        Assert.Equal("fornobless.htm", _npcStat.NpcAiData.FnNobless);
        Assert.Equal("fornonobless.htm", _npcStat.NpcAiData.FnNoNobless);
        Assert.Equal("fornonoblessitem.htm", _npcStat.NpcAiData.FnNoNoblessItem);
        Assert.Equal("valentina003.htm", _npcStat.NpcAiData.FnYouAreChaotic);
        Assert.Equal(0, _npcStat.NpcAiData.MoveAroundSocial);
        Assert.Equal(90, _npcStat.NpcAiData.MoveAroundSocial1);
        Assert.Equal(90, _npcStat.NpcAiData.MoveAroundSocial2);
    }

    [Fact]
    public void CheckAiTest()
    {
        _npcInstance.NpcAi().Created();
        //await Process.GetCurrentProcess().WaitForExitAsync();
    }
}