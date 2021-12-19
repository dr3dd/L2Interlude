namespace NpcService.Model
{
    public class NpcDesire
    {
        public int ObjectId { get; set; }
        public int ActionId { get; set; }
        public int PlayerObjectId { get; set; }
        public ActionDesire ActionDesire { get; set; }

        public override int GetHashCode()
        {
            return ObjectId;
        }

        public override bool Equals(object? obj)
        {
            var objectId = obj as NpcDesire;
            return GetHashCode() == objectId.ObjectId;
        }
    }
}