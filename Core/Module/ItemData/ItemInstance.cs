using Core.Module.WorldData;

namespace Core.Module.ItemData
{
    public class ItemInstance : WorldObject
    {
        public int ItemId { get; }
        public ItemDataModel ItemData { get; }
        public ItemInstance(int objectId, ItemDataModel itemDataModel)
        {
            ObjectId = objectId;
            ItemId = itemDataModel.ItemId;
            ItemData = itemDataModel;
        }
    }
}