using System;
using System.Collections.Generic;
using System.Text;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 19.08.2024 22:12:39

namespace Config
{
    public class AccessConfig
    {
        public const string AccessSection = "AccessSection";
        /// <summary>
        /// GmList "player_name":"access_template"
        /// Список администраторов "player_name":"access_template"
        /// </summary>
        public Dictionary<string, string> GMList { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Access templates list
        /// Список шаблонов доступа
        /// </summary>
        public Dictionary<string, AdminAccessTemplate> Templates { get; set; } = new Dictionary<string, AdminAccessTemplate>();
    }

    public class AdminAccessTemplate
    {
        /// <summary>
        /// List of allowed commands
        /// Список разрешенных команд
        /// </summary>
        public string[] AllowCommands { get; set; } = new string[] { };
        /// <summary>
        /// List of unallowed commands
        /// Список запрещенных команд
        /// </summary>
        public string[] DenyCommands { get; set; } = new string[] { };
        /// <summary>
        /// Allow all commands
        /// Разрешить все команды
        /// </summary>
        public bool AllowAllCommands { get; set; } = false;
    }
}
