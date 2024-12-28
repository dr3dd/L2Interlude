using System;

namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Dwarf : CreatureAbstract
    {
        private const byte RaceId = 4;
        protected Dwarf(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}