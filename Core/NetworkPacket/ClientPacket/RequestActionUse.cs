using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket;
public class RequestActionUse : PacketBase
{
    private readonly int _actionId;
    private readonly bool _ctrlPressed;
    private readonly bool _shiftPressed;
    private readonly PlayerInstance _playerInstance;
    private readonly CharacterMovementStatus _characterMovement;

    public RequestActionUse(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
    {
        _actionId = packet.ReadInt();
        _ctrlPressed = packet.ReadInt() == 1;
        _shiftPressed = packet.ReadByte() == 1;
        _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        _characterMovement = _playerInstance.CharacterMovement().CharacterMovementStatus();
    }

    public override async Task Execute()
    {
        switch (_actionId)
        {
            case 0:
            break;
            case 1:
                if (_characterMovement.IsGroundHigh())
                {
                    _characterMovement.SetGroundLow();
                    break;
                }
                _characterMovement.SetGroundHigh();
                break;
        }
        await _playerInstance.SendToKnownPlayers(new ChangeMoveType(_playerInstance));
    }
}