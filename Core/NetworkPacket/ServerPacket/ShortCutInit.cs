using Core.Module.CharacterData;
using Core.Module.Player;
using Core.Module.Player.ShortCuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override void Write()
        {
            WriteByte(0x45);
            WriteInt(_shortCuts.Count);
            foreach (var shortCut in _shortCuts)
            {
                WriteInt((int)shortCut.Type);
                WriteInt(shortCut.Slot + (shortCut.Page * 12));
                switch (shortCut.Type)
                {
                    case ShortCutType.NONE:
                    case ShortCutType.ACTION:
                    case ShortCutType.MACRO:
                    case ShortCutType.RECIPE:
                    case ShortCutType.BOOKMARK:
                        WriteInt(shortCut.Id);
                        WriteInt(0x01); // C6
                        break;
                    case ShortCutType.ITEM:
                        WriteInt(shortCut.Id);
                        WriteInt(0x01);
                        WriteInt(-1);
                        WriteInt(0x00);
                        WriteInt(0x00);
                        WriteShort(0x00);
                        WriteShort(0x00);
                        break;
                    case ShortCutType.SKILL:
                        WriteInt(shortCut.Id);
                        WriteInt(shortCut.Level);
                        WriteByte(0x00); // C5
                        WriteInt(0x01); // C6
                        break;
                }
            }
        }
    }
}
