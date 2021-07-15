using Network;

namespace Core.Module.Player.CharacterData.Response
{
    public class CharTemplates : ServerPacket
    {
        public CharTemplates()
        {
        }
        public override void Write()
        {
            WriteByte(0x17);
        }
    }
}