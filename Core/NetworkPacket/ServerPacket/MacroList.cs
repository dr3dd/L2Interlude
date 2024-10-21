using Core.Module.Player.Macroses;
using Google.Protobuf;
using L2Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.10.2024 23:51:08

namespace Core.NetworkPacket.ServerPacket
{
    internal class MacroList : Network.ServerPacket
    {
        private int _count;
        private MacrosModel _macros;
        private MacroUpdateType _updateType;

        public MacroList(int count, MacrosModel macros, MacroUpdateType updateType)
        {
            _count = count;
            _macros = macros;
            _updateType = updateType;
        }
        public override void Write()
        {
            WriteByte(0xe7);
            WriteByte((byte)_updateType);
            WriteInt(_updateType != MacroUpdateType.LIST ? _macros.UserMacrosId : 0); // modified, created or deleted macro's id
            WriteByte(_count);
            WriteByte(_macros != null);

            if (_macros != null && (_updateType != MacroUpdateType.DELETE))
            {
                WriteInt(_macros.UserMacrosId); // Macro ID
                WriteString(_macros.Name); // Macro Name
                WriteString(_macros.Description); // Desc
                WriteString(_macros.Acronym); // acronym
                WriteByte(_macros.Icon); // icon
                WriteByte(_macros.CommandArray.Count); // count
                int i = 0;
                foreach (MacrosCmd cmd in _macros.CommandArray)
                {
                    WriteByte(++i); // command count
                    WriteByte((byte)cmd.type); // type 1 = skill, 3 = action, 4 = shortcut
                    WriteInt(cmd.d1); // skill id
                    WriteByte(cmd.d2); // shortcut id
                    WriteString(cmd.cmd); // command name
                }
            }
            
        }
    }
}
