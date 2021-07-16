using Network;

namespace Core.Module.CharacterData.Response
{
    public class CharTemplates : ServerPacket
    {
        public override void Write()
        {
            WriteByte(0x17);
        }
    }
}