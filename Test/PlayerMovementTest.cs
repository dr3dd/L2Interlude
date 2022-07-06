using Core.Module.CharacterData;
using Xunit;

namespace Test
{
    public class PlayerMovementTest : PlayerBase
    {
        [Fact]
        public void MoveToLocationTest()
        {
            var playerInstance = GetPlayerInstance();
            playerInstance.Location = new Location(-71453, 258305, -3104);
            playerInstance.CharacterMovement().MoveToLocation(-71953, 258905, -3154, 0);
            
            Assert.Equal(-71953, playerInstance.CharacterMovement().GetXDestination());
            Assert.Equal(258905, playerInstance.CharacterMovement().GetYDestination());
            Assert.Equal(-3154, playerInstance.CharacterMovement().GetZDestination());
        }
    }
}