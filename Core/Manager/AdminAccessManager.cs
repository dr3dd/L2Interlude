using Config;
using Core.Module.Player;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 19.08.2024 22:34:33

namespace Core.Manager
{
    public class AdminAccessManager
    {
        private AccessConfig accessConfig;
        public AdminAccessManager(IServiceProvider serviceProvider) {
            accessConfig = serviceProvider.GetService<GameConfig>().AccessConfig;
            LoggerManager.Info($"AdminAccessManager: Loaded {accessConfig.GMList.Count} GM and {accessConfig.Templates.Count} templates");
        }

        private AdminAccessTemplate GetAccessTemplate(string name)
        {
            string templateName = accessConfig.GMList.GetValueOrDefault(name);
            if (templateName == null)
            {
                return new AdminAccessTemplate();
            }
            return accessConfig.Templates.GetValueOrDefault(templateName);
        }

        public bool IsGM(string characterName)
        {
            string templateName = accessConfig.GMList.GetValueOrDefault(characterName);
            return templateName != null;
        }

        internal bool CheckCommand(PlayerInstance admin, string alias)
        {
            bool result = false;
            AdminAccessTemplate template = GetAccessTemplate(admin.CharacterName);
            //allow all commands
            if (template.AllowAllCommands)
            {
                return true;
            }
            //what commands are allowed
            foreach (string command in template.AllowCommands)
            {
                string cmd = alias.Split(' ')[0];
                if (command.Equals(cmd))
                {
                    result = true;
                }
            }
            //what commands are NOT allowed
            foreach (string command in template.DenyCommands)
            {
                string cmd = alias.Split(' ')[0];
                if (command.Equals(cmd))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
