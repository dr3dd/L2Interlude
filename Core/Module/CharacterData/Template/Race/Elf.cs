using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Elf : CreatureAbstract
    {
        private const byte RaceId = 1;
        private const string RaceName = "elf";
        protected Elf(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}