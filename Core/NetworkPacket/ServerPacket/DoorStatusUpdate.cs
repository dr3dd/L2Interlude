using System.Threading.Tasks;
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

    public override async Task WriteAsync()
    {
        await WriteByteAsync(0x4D);
        await WriteIntAsync(_doorInstance.ObjectId);
        await WriteIntAsync(_openClose);
        await WriteIntAsync(0);
        await WriteIntAsync(0);
        await WriteIntAsync(_doorInstance.DoorStat().EditorId); //door id, editor_id
        await WriteIntAsync(_doorInstance.DoorStat().Hp); //MaxHp
        await WriteIntAsync(_doorInstance.DoorStat().Hp); //CurrentHp
    }
}