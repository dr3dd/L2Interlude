namespace DataBase.Entities
{
    public class UserItemEntity
    {
        public int ItemId { get; set; } 
        public int CharacterId { get; set; }
        public int ItemType { get; set; }
        public int Amount { get; set; }
        public int Enchant { get; set; }
    }
}