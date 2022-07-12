namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Dwarf : CreatureAbstract
    {
        private const byte RaceId = 4;

        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}