
namespace DataBase.Entities
{
    [Dapper.Contrib.Extensions.Table("characters")]
    public class CharacterEntity
    {
        public string CharacterName { get; set; }
        public int CharacterId { get; set; }
        public string AccountName { get; set; }
        public int AccountId { get; set; }
        public byte Gender { get; set; }
        public byte Race { get; set; }
        public byte ClassId { get; set; }
        public int XLoc { get; set; }
        public int YLoc { get; set; }
        public int ZLoc { get; set; }
        public bool IsInVehicle { get; set; }
        public float Cp { get; set; }
        public float Hp { get; set; }
        public float Mp { get; set; }
        public int Sp { get; set; }
        public long Exp { get; set; }
        public byte Level { get; set; }
        public int Pk { get; set; }
        public int Duel { get; set; }
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
        public byte QuestFlag { get; set; }
        public string Nickname { get; set; }
        public int MaxCp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public string QuestMemo { get; set; }
        public int Face { get; set; }
        public int HairStyle { get; set; }
        public int HairColor { get; set; }
    }
}