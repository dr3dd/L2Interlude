using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket;

public class CharMoveToLocation : Network.ServerPacket
{
    private readonly int _objectId;
    private readonly int _x;
    private readonly int _y;
    private readonly int _z;
    private readonly int _xDst;
    private readonly int _yDst;
    private readonly int _zDst;

    public CharMoveToLocation(Character character)
    {
        _objectId = character.ObjectId;
        _x = character.GetX();
        _y = character.GetY();
        _z = character.GetZ();
        _xDst = character.CharacterMovement().GetXDestination();
        _yDst = character.CharacterMovement().GetYDestination();
        _zDst = character.CharacterMovement().GetZDestination();
    }
        
    public override void Write()
    {
        WriteByte(0x01);
        WriteInt(_objectId);
        WriteInt(_xDst);
        WriteInt(_yDst);
        WriteInt(_zDst);
        WriteInt(_x);
        WriteInt(_y);
        WriteInt(_z);
    }
}