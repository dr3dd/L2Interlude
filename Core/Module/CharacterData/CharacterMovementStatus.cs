using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData;

public class CharacterMovementStatus
{
    private MovementStatus _currentMovementStatus;
    public MovementStatus CurrentMovementStatus() => _currentMovementStatus;
    private readonly Character _character;

    public CharacterMovementStatus(CharacterMovement characterMovement)
    {
        _character = characterMovement.Character();
    }

    public void SetGroundHigh()
    {
        _currentMovementStatus = MovementStatus.GroundHighSpeed;
    }

    public void SetGroundLow()
    {
        _currentMovementStatus = MovementStatus.GroundLowSpeed;
    }

    public void SetSit()
    {
        _currentMovementStatus = MovementStatus.Sit;
    }

    public void SetStand()
    {
        _currentMovementStatus = MovementStatus.Stand;
    }
    
    public bool IsGroundHigh()
    {
        return _currentMovementStatus == MovementStatus.GroundHighSpeed;
    }
    
    public bool IsSit()
    {
        return _currentMovementStatus == MovementStatus.Sit;
    }

    public bool IsStand()
    {
        return _currentMovementStatus == MovementStatus.Stand;
    }

    public bool IsGroundLow()
    {
        return _currentMovementStatus == MovementStatus.GroundLowSpeed;
    }
}