using System.Threading.Tasks;
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

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x44);

            await WriteIntAsync((int)_shortcut.Type);
            await WriteIntAsync(_shortcut.Slot + (_shortcut.Page * 12)); // C4 Client
            switch (_shortcut.Type)
            {
                case ShortCutType.NONE:
                case ShortCutType.ITEM:
                case ShortCutType.ACTION:
                case ShortCutType.MACRO:
                case ShortCutType.RECIPE:
                case ShortCutType.BOOKMARK:
                    await WriteIntAsync(_shortcut.Id);
                    break;
                case ShortCutType.SKILL:
                    await WriteIntAsync(_shortcut.Id);
                    await WriteIntAsync(_shortcut.Level);
                    await WriteByteAsync(0x00); // C5
                    break;
            }
        }
    }
}
