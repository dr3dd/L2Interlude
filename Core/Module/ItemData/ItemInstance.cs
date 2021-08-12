using Core.Module.WorldData;

namespace Core.Module.ItemData
{
    public class ItemInstance : WorldObject
    {
        public int ItemId { get; set; }
        public ItemDataModel ItemData { get; set; }
        public ItemInstance(int objectId)
        {
            ObjectId = objectId;
        }
    }
}