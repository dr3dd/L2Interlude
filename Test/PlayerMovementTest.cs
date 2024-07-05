using Core.GeoEngine;
using Core.Module.CharacterData;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test;

public class PlayerMovementTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly PlayerInstance _playerInstance;
    private readonly GeoEngineInit _geoEngine;
    public PlayerMovementTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _playerInstance.CharacterEffect().RemoveEffects();
        _geoEngine = _playerInstance.ServiceProvider.GetRequiredService<GeoEngineInit>();
    }

    [Fact]
    public void MoveToLocationTest()
    {
        _playerInstance.Location = new Location(-71453, 258305, -3104);
        _playerInstance.WorldObjectPosition().SetWorldPosition(-71453, 258305, -3104);
        _playerInstance.CharacterMovement().MoveToLocation(-71953, 258905, -3154, 0);
            
        Assert.Equal(-71953, _playerInstance.CharacterMovement().GetXDestination());
        Assert.Equal(258905, _playerInstance.CharacterMovement().GetYDestination());
        Assert.Equal(-3154, _playerInstance.CharacterMovement().GetZDestination());
    }

    [Fact]
    public void CanSeeTargetTest()
    {
        var chaX = -71453;
        var chaY = 258305;
        var chaZ = -3104;

        var tarX = -71653;
        var tarY = 258605;
        var tarZ = -3154;

        var canSee = _geoEngine.CanSeeTarget(chaX, chaY, chaZ, tarX, tarY, tarZ);
        Assert.True(canSee);
    }
    
    [Fact]
    public void CanNotSeeTargetTest()
    {
        var chaX = -71453;
        var chaY = 258305;
        var chaZ = -3104;

        var tarX = -71753;
        var tarY = 258605;
        var tarZ = -3154;

        var canSee = _geoEngine.CanSeeTarget(chaX, chaY, chaZ, tarX, tarY, tarZ);
        Assert.False(canSee);
    }

    [Fact]
    public void CanFindPathTest()
    {
        var chaX = -71153;
        var chaY = 258305;
        var chaZ = -3104;

        var tarX = -71953;
        var tarY = 258905;
        var tarZ = -3154;

        var dd = _geoEngine.CellPathFinding().FindPath(chaX, chaY, chaZ, tarX, tarY, tarZ, 1, true);
        var d = 1;
    }
}