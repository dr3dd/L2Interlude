using System.Threading.Tasks;
using Core.Module.DoorData;

namespace Core.NetworkPacket.ServerPacket;

public class DoorInfo : Network.ServerPacket
{
    private readonly DoorInstance _doorInstance;
    private readonly bool _showHp;
    public DoorInfo(DoorInstance doorInstance, bool showHp)
    {
        _doorInstance = doorInstance;
        _showHp = showHp;
    }
    public override async Task WriteAsync()
    {
        await WriteByteAsync(0x4C);
        await WriteIntAsync(_doorInstance.ObjectId);
        await WriteIntAsync(_doorInstance.DoorStat().EditorId);
        await WriteIntAsync(_showHp ? 0x01 : 0x00);
    }
}