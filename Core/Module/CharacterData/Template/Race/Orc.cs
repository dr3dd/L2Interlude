namespace Core.Module.CharacterData.Template.Race
{
    public abstract class Orc : CreatureAbstract
    {
        private const byte RaceId = 3;

        public byte GetRaceId()
        {
            return RaceId;
        }
    }
}