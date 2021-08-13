using System;

namespace Core.Module.ItemData
{
    [Flags]
    public enum SlotBitType
    {
        None = 0,
        UnderWear = 1,
        RightEarning = 2,
        LeftEarning = 4,
        Necklace = 8,
        RightFinger = 16,
        LeftFinger = 32,
        Head = 64,
        RightHand = 128,
        LeftHand = 256,
        Gloves = 512,
        Chest = 1024,
        Legs = 2048,
        Feet = 4096,
        Back = 8192,
        LeftRightHand = 16384,
        OnePiece = 32768,
        Hair = 65536,
        Face = 262144,
        HairAll = 524288
    }
}