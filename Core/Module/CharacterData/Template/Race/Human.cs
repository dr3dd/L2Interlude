using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Human : CreatureAbstract
    {
        private const byte RaceId = 0;
        protected Human(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}