using Core.Module.Player.ShortCuts;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 0:27:05

namespace Core.NetworkPacket.ServerPacket
{
    public class ShortCutRegister : Network.ServerPacket
    {
        private readonly ShortCut _shortcut;
        public ShortCutRegister(ShortCut shortcut)
        {
            _shortcut = shortcut;
        }

        public override void Write()
        {
            WriteByte(0x44);

            WriteInt((int)_shortcut.Type);
            WriteInt(_shortcut.Slot + (_shortcut.Page * 12)); // C4 Client
            switch (_shortcut.Type)
            {
                case ShortCutType.NONE:
                case ShortCutType.ITEM:
                case ShortCutType.ACTION:
                case ShortCutType.MACRO:
                case ShortCutType.RECIPE:
                case ShortCutType.BOOKMARK:
                    WriteInt(_shortcut.Id);
                    break;
                case ShortCutType.SKILL:
                    WriteInt(_shortcut.Id);
                    WriteInt(_shortcut.Level);
                    WriteByte(0x00); // C5
                    break;
            }
        }
    }
}
