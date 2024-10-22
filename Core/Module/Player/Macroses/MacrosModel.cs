using DataBase.Entities;
using System.Collections.Generic;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 26.09.2024 23:46:20

namespace Core.Module.Player.Macroses
{
    public class MacrosModel : UserMacrosEntity
    {
        public string Commands { get; set; }

        public List<MacrosCmd> CommandArray { get => GetMacroCommands(Commands); set => Commands = SetMacroCommands(value); }

        private List<MacrosCmd> GetMacroCommands(string commands)
        {
            List<MacrosCmd> result = new List<MacrosCmd>();
            foreach (string str in commands.Split(";"))
            {
                if (str.Length > 1)
                {
                    string[] command = str.Split(",");
                    result.Add(new MacrosCmd(int.Parse(command[0]), (MacroType)int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]), command[4]));
                }
            }
            return result;
        }

        private string SetMacroCommands(List<MacrosCmd> macroses)
        {
            string result = "";
            foreach (MacrosCmd macro in macroses)
            {
                result += macro.entry + "," + (int)macro.type + "," + macro.d1 + "," + macro.d2 + "," + macro.cmd + ";";
            }
            return result;
        }
    }
}
