using Core.Module.CharacterData;
using Core.Module.Player;
using Xunit;

namespace Test;

public class PlayerMovementTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly PlayerInstance _playerInstance;
    public PlayerMovementTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _playerInstance.CharacterEffect().RemoveEffects();
    }

    [Fact]
    public void MoveToLocationTest()
    {
        _playerInstance.Location = new Location(-71453, 258305, -3104);
        _playerInstance.CharacterMovement().MoveToLocation(-71953, 258905, -3154, 0);
            
        Assert.Equal(-71953, _playerInstance.CharacterMovement().GetXDestination());
        Assert.Equal(258905, _playerInstance.CharacterMovement().GetYDestination());
        Assert.Equal(-3154, _playerInstance.CharacterMovement().GetZDestination());
    }
}