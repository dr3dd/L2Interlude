using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 22:31:30

namespace Core.Module.Player.Macroses
{
    public class MacrosCmd
    {
        public int entry;
        public MacroType type;
        public int d1; // skill_id or page for shortcuts
        public int d2; // shortcut
        public string cmd;
        public MacrosCmd()
        {
            new MacrosCmd(3, 0, 0, 0, "");
        }

        public MacrosCmd(int entry, MacroType type, int d1, int d2, string cmd)
        {
            this.entry = entry;
            this.type = type;
            this.d1 = d1;
            this.d2 = d2;
            this.cmd = cmd;
        }

        override
        public string ToString()
        {
            return "MacrosCmd entry=" + entry + " type=" + type + " d1=" + d1 + " d2=" + d2 + " cmd=" + cmd;
        }
    }
}
