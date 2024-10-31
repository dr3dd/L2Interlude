using System.Threading.Tasks;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket;

public class Die : Network.ServerPacket
{
    private readonly Character _character;
    public Die(Character character)
    {
        _character = character;
    }

    public override async Task WriteAsync()
    {
        await WriteByteAsync(0x06);
        await WriteIntAsync(_character.ObjectId);
        await WriteIntAsync(0x01); //to nearest village
        await WriteIntAsync(0x00);
        await WriteIntAsync(0x00);
        await WriteIntAsync(0x00);
        await WriteIntAsync(0x00);
        await WriteIntAsync(0x00);
    }
}