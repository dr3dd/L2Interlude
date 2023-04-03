using Core.Module.NpcData;
using Xunit;

namespace Test.NpcAi.Citizen;

public class Nerupa : IClassFixture<NpcInstanceFixture>
{
    private readonly NpcInstance _npcInstance;
    private readonly NpcStat _npcStat;
    
    public Nerupa(NpcInstanceFixture npcInstanceFixture)
    {
        _npcInstance = npcInstanceFixture.GetNpcInstance("nerupa");
        _npcStat = _npcInstance.GetStat();
    }

    [Fact]
    public void CheckNerupaStatsTest()
    {
        Assert.Equal(30370, _npcInstance.NpcId);
        Assert.Equal(70, _npcStat.Level);
    }
}