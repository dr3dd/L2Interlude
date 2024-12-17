using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Orc : CreatureAbstract
    {
        private const byte RaceId = 3;
        protected Orc(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}