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
        var status = _playerInstance.CharacterBaseStatus();
        Assert.Equal(50, status.GetMaxCp());
    }

    [Fact]
    public void GetMaxHpTest()
    {
        var status = _playerInstance.CharacterBaseStatus();
        Assert.Equal(126, status.GetMaxHp());
    }

    [Fact]
    public void GetMaxMpTest()
    {
        var status = _playerInstance.CharacterBaseStatus();
        Assert.Equal(38, status.GetMaxMp());
    }

    [Fact]
    public void GetHpRegenTest()
    {
        var status = _playerInstance.CharacterBaseStatus();
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetSit();
        Assert.Equal("14.22", status.GetHpRegenRate().ToString("F"));
        
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetStand();
        Assert.Equal("10.43", status.GetHpRegenRate().ToString("F"));
        
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetGroundHigh();
        Assert.Equal("6.64", status.GetHpRegenRate().ToString("F"));
        
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetGroundLow();
        Assert.Equal("9.48", status.GetHpRegenRate().ToString("F"));
    }

    [Fact]
    public void GetPlayerCurrentSpeedTest()
    {
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetGroundHigh();
        var highSpeed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(126.50000000000001, highSpeed);
        
        _playerInstance.CharacterMovement().CharacterMovementStatus().SetGroundLow();
        var lowSpeed = _playerInstance.CharacterCombat().GetCharacterSpeed();
        Assert.Equal(88, lowSpeed);
    }
    
    [Fact]
    public void GetPlayerMaxLoadTest()
    {
        var maxLoad = _playerInstance.CharacterBaseStatus().GetMaxLoad();
        Assert.Equal(109020, maxLoad);
    }
    
}