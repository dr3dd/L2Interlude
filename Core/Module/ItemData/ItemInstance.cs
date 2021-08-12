using Core.Module.Player;
using Core.Module.WorldData;

namespace Core.Module.ItemData
{
    public class ItemInstance : WorldObject
    {
        public int UserItemId { get; set; }
        public int ItemId { get; set; }
        public ItemDataModel ItemData { get; set; }
        public int Amount { get; set; }

        public ItemInstance(int objectId)
        {
            ObjectId = objectId;
        }

        public SlotBitType GetSlotBitType()
        {
            return ItemData.SlotBitType;
        }

        public ItemType GetItemType()
        {
            return ItemData.ItemType;
        }

        public bool IsEquipped(PlayerCharacterInfo characterInfo)
        {
            if (characterInfo.StBack == UserItemId)
            {
                return true;
            }
            if (characterInfo.StChest == UserItemId)
            {
                return true;
            }
            if (characterInfo.StFace == UserItemId)
            {
                return true;
            }
            if (characterInfo.StFeet == UserItemId)
            {
                return true;
            }
            if (characterInfo.StHair == UserItemId)
            {
                return true;
            }
            if (characterInfo.StHead == UserItemId)
            {
                return true;
            }
            if (characterInfo.StLegs == UserItemId)
            {
                return true;
            }
            if (characterInfo.StNeck == UserItemId)
            {
                return true;
            }
            if (characterInfo.StUnderwear == UserItemId)
            {
                return true;
            }
            if (characterInfo.StBothHand == UserItemId)
            {
                return true;
            }
            if (characterInfo.StHairAll == UserItemId)
            {
                return true;
            }
            if (characterInfo.StLeftEar == UserItemId)
            {
                return true;
            }
            if (characterInfo.StLeftFinger == UserItemId)
            {
                return true;
            }
            if (characterInfo.StLeftHand == UserItemId)
            {
                return true;
            }
            if (characterInfo.StRightEar == UserItemId)
            {
                return true;
            }
            if (characterInfo.StRightFinger == UserItemId)
            {
                return true;
            }
            if (characterInfo.StRightHand == UserItemId)
            {
                return true;
            }
            return false;
        }
    }
}