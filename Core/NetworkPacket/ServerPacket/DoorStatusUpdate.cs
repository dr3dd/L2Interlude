using Core.Module.DoorData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket;

public class DoorStatusUpdate : Network.ServerPacket
{
    private readonly DoorInstance _doorInstance;
    private readonly PlayerInstance _playerInstance;
    private readonly int _openClose;
    public DoorStatusUpdate(DoorInstance doorInstance, PlayerInstance playerInstance, int openClose)
    {
        _doorInstance = doorInstance;
        _playerInstance = playerInstance;
        _openClose = openClose;
    }

    public override void Write()
    {
        WriteByte(0x4D);
        WriteInt(_doorInstance.ObjectId);
        WriteInt(_openClose);
        WriteInt(0);
        WriteInt(0);
        WriteInt(_doorInstance.DoorStat().EditorId); //door id, editor_id
        WriteInt(_doorInstance.DoorStat().Hp); //MaxHp
        WriteInt(_doorInstance.DoorStat().Hp); //CurrentHp
    }
}