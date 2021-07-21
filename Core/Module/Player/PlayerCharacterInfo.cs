using Core.Module.CharacterData;

namespace Core.Module.Player
{
    public class PlayerCharacterInfo
    {
        public int CharacterId { get; set; }
        public float CurrentCp { get; set; }
        public float CurrentHp { get; set; }
        public float CurrentMp { get; set; }
        
        public long Exp { get; set; }
        public int Sp { get; set; }
        public byte Level { get; set; } = 1;
        public Location Location { get; set; }

        private PlayerInstance _playerInstance;

        public PlayerCharacterInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public int GetPAtk()
        {
            return 1;
        }

        public int GetPAtkSpd()
        {
            return 1;
        }
    }
}