using System;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test
{
    public class PlayerMovementTest
    {
        private readonly IServiceProvider _serviceProvider;
        public PlayerMovementTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<GameTimeController>();
            _serviceProvider = services.BuildServiceProvider();
        }
        [Fact]
        public void MoveToLocationTest()
        {
            var templateInit = new TemplateInit();
            PlayerAppearance playerAppearance = new PlayerAppearance("Test", "Test", 0, 0, 0, 0);
            
            //var test1 = serviceProvider.GetService<GameTimeController>();
            
            PlayerInstance playerInstance = new PlayerInstance(templateInit.GetTemplateByClassId(0), playerAppearance, _serviceProvider);
            playerInstance.PlayerCharacterInfo().Location = new Location(-71453, 258305, -3104);
            playerInstance.PlayerMovement().MoveToLocation(-71953, 258905, -3154, 0);
            
            Assert.Equal(-71953, playerInstance.PlayerMovement().GetXDestination());
            Assert.Equal(258905, playerInstance.PlayerMovement().GetYDestination());
            Assert.Equal(-3154, playerInstance.PlayerMovement().GetZDestination());
        }
    }
}