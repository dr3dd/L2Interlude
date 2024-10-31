using Core.Module.Player;
using Core.Module.Player.ShortCuts;
using System.Collections.Generic;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 1:06:57

namespace Core.NetworkPacket.ServerPacket
{
    public class ShortCutInit : Network.ServerPacket
    {
        private readonly IList<ShortCut> _shortCuts;
        public ShortCutInit(PlayerInstance player)
        {
            _shortCuts = player.PlayerShortCut().GetAllShortCuts();
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x45);
            await WriteIntAsync(_shortCuts.Count);
            foreach (var shortCut in _shortCuts)
            {
                await WriteIntAsync((int)shortCut.Type);
                await WriteIntAsync(shortCut.Slot + (shortCut.Page * 12));
                switch (shortCut.Type)
                {
                    case ShortCutType.NONE:
                    case ShortCutType.ACTION:
                    case ShortCutType.MACRO:
                    case ShortCutType.RECIPE:
                    case ShortCutType.BOOKMARK:
                        await WriteIntAsync(shortCut.Id);
                        await WriteIntAsync(0x01); // C6
                        break;
                    case ShortCutType.ITEM:
                        await WriteIntAsync(shortCut.Id);
                        await WriteIntAsync(0x01);
                        await WriteIntAsync(-1);
                        await WriteIntAsync(0x00);
                        await WriteIntAsync(0x00);
                        await WriteShortAsync(0x00);
                        await WriteShortAsync(0x00);
                        break;
                    case ShortCutType.SKILL:
                        await WriteIntAsync(shortCut.Id);
                        await WriteIntAsync(shortCut.Level);
                        await WriteByteAsync(0x00); // C5
                        await WriteIntAsync(0x01); // C6
                        break;
                }
            }
        }
    }
}
