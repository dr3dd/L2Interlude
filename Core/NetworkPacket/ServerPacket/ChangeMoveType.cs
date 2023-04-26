using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket;

public class ChangeMoveType : Network.ServerPacket
{
    private readonly int _objectId;
    private readonly bool _isHighSpeed;
    private const int HighSpeed = 1;
    private const int LowSpeed = 0;

    public ChangeMoveType(Character character)
    {
        _objectId = character.ObjectId;
        _isHighSpeed = character.CharacterMovement().CharacterMovementStatus().IsGroundHigh();
    }
    
    public override void Write()
    {
        WriteByte(0x2E);
        WriteInt(_objectId);
        WriteInt(_isHighSpeed ? HighSpeed : LowSpeed);
        WriteInt(0); //C2
    }
}