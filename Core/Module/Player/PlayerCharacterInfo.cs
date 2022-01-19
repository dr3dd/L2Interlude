using Core.Module.CharacterData.Template;

namespace Core.Module.Player
{
    public class PlayerCharacterInfo
    {
        public int CharacterId { get; set; }
        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        
        public long Exp { get; set; }
        public int Sp { get; set; }
        public int StUnderwear { get; set; }
        public int StRightEar { get; set; }
        public int StLeftEar { get; set; }
        public int StNeck { get; set; }
        public int StRightFinger { get; set; }
        public int StLeftFinger { get; set; }
        public int StHead { get; set; }
        public int StRightHand { get; set; }
        public int StLeftHand { get; set; }
        public int StGloves { get; set; }
        public int StChest { get; set; }
        public int StLegs { get; set; }
        public int StFeet { get; set; }
        public int StBack { get; set; }
        public int StBothHand { get; set; }
        public int StHair { get; set; }
        public int StFace { get; set; }
        public int StHairAll { get; set; }

        private readonly PlayerInstance _playerInstance;
        private readonly ITemplateHandler _templateHandler;

        public PlayerCharacterInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = _playerInstance.TemplateHandler();
        }

        public int GetMAtkSpd()
        {
            return _templateHandler.GetBaseAttackSpeed();
        }
    }
}