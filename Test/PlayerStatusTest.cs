using Core.Module.Player;
using Xunit;

namespace Test;

public class PlayerStatusTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly PlayerInstance _playerInstance;
    public PlayerStatusTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _playerInstance.CharacterEffect().RemoveEffects();
    }

    [Fact]
    public void GetMaxCpTest()
    {
        var status = _playerInstance.PlayerStatus();
        Assert.Equal(50, status.GetMaxCp());
    }

    [Fact]
    public void GetMaxHpTest()
    {
        var status = _playerInstance.PlayerStatus();
        Assert.Equal(126, status.GetMaxHp());
    }

    [Fact]
    public void GetMaxMpTest()
    {
        var status = _playerInstance.PlayerStatus();
        Assert.Equal(38, status.GetMaxMp());
    }
}