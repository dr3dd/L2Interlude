using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;

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

        private readonly PlayerInstance _playerInstance;
        private readonly ITemplateHandler _templateHandler;

        public PlayerCharacterInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = _playerInstance.TemplateHandler();
        }

        public int GetPAtk()
        {
            return _templateHandler.GetBasePhysicalAttack();
        }
        public int GetMAtk()
        {
            return _templateHandler.GetBaseMagicAttack();
        }

        public int GetPAtkSpd()
        {
            return _templateHandler.GetBaseAttackSpeed();
        }
        
        public int GetMAtkSpd()
        {
            return _templateHandler.GetBaseAttackSpeed();
        }

        public int GetPDef()
        {
            return _templateHandler.GetBaseDefend();
        }

        public int GetMDef()
        {
            return _templateHandler.GetBaseMagicDefend();
        }
    }
}