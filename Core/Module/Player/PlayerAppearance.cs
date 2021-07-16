namespace Core.Module.Player
{
    public struct PlayerAppearance
    {
        public string AccountName { get; }
        public string CharacterName { get; }
        public byte Face { get; }
        public byte HairColor { get; }
        public byte HairStyle { get; }
        public byte Gender { get; }
        public PlayerAppearance(string accountName, string characterName, byte face, byte hairColor, byte hairStyle, byte gender)
        {
            AccountName = accountName;
            CharacterName = characterName;
            Face = face;
            HairColor = hairColor;
            HairStyle = hairStyle;
            Gender = gender;
        }
    }
}