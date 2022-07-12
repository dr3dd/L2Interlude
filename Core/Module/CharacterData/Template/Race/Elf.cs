namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Elf : CreatureAbstract
    {
        private const byte RaceId = 1;

        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}