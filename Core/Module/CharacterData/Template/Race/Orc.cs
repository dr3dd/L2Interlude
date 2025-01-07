using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Orc : CreatureAbstract
    {
        private const byte RaceId = 3;
        private const string RaceName = "orc";
        protected Orc(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}