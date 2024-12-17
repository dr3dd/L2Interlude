using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class DarkElf : CreatureAbstract
    {
        private const byte RaceId = 2;
        protected DarkElf(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}