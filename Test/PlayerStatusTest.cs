using Xunit;

namespace Test
{
    public class PlayerStatusTest : PlayerBase
    {
        
        [Fact]
        public void GetMaxCpTest()
        {
            var playerInstance = GetPlayerInstance();
            var status = playerInstance.PlayerStatus();
            Assert.Equal(50, status.GetMaxCp());
        }
        [Fact]
        public void GetMaxHpTest()
        {
            var playerInstance = GetPlayerInstance();
            var status = playerInstance.PlayerStatus();
            Assert.Equal(126, status.GetMaxHp());
        }

        [Fact]
        public void GetMaxMpTest()
        {
            var playerInstance = GetPlayerInstance();
            var status = playerInstance.PlayerStatus();
            Assert.Equal(38, status.GetMaxMp());
        }
        
    }
}