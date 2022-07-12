namespace Core.Module.CharacterData.Template.Race
{
    public abstract class DarkElf : CreatureAbstract
    {
        private const byte RaceId = 2;

        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}