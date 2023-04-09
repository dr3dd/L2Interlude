using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket;

public class Die : Network.ServerPacket
{
    private readonly Character _character;
    public Die(Character character)
    {
        _character = character;
    }

    public override void Write()
    {
        WriteByte(0x06);
        WriteInt(_character.ObjectId);
        WriteInt(0x01); //to nearest village
        WriteInt(0x00);
        WriteInt(0x00);
        WriteInt(0x00);
        WriteInt(0x00);
        WriteInt(0x00);
    }
}