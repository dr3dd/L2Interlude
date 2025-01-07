using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Human : CreatureAbstract
    {
        private const byte RaceId = 0;
        private const string RaceName = "human";
        protected Human(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}