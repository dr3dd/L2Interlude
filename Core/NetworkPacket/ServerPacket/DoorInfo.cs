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
    public override void Write()
    {
        WriteByte(0x4C);
        WriteInt(_doorInstance.ObjectId);
        WriteInt(_doorInstance.DoorStat().EditorId);
        WriteInt(_showHp ? 0x01 : 0x00);
    }
}