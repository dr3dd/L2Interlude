using Core.Attributes;
using Core.Module.Handlers.AdminCommands;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using L2Logger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:01:45

namespace Core.Module.Handlers
{
    public class AdminCommandHandler : IAdminCommandHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SortedList<string, AbstractAdminCommand> commands = new SortedList<string, AbstractAdminCommand>();
        public AdminCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            IEnumerable<Type> typelist = Utility.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Core.Module.Handlers.AdminCommands");
            foreach (Type t in typelist)
            {
                if (!t.Name.StartsWith("Abstract") && t.BaseType.Name.Equals("AbstractAdminCommand"))
                {
                    Register(Activator.CreateInstance(t));
                }
            }

            LoggerManager.Info($"AdminCommandHandler: Loaded {commands.Count} commands.");
        }

        public async Task Request(PlayerInstance admin, string alias)
        {
            if (!alias.StartsWith("admin_"))
                alias = "admin_" + alias;

            string cmd = alias;
            if (alias.Contains(" "))
                cmd = alias.Split(' ')[0];

            if (!commands.ContainsKey(cmd))
            {
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"Command {cmd} not exists."));
                //admin.SendActionFailedPacketAsync();
                LoggerManager.Warn($"AdminCommandHandler: Command {cmd} not exists.");
                return;
            }

            AbstractAdminCommand processor = commands[cmd];
            try
            {
                await processor.UseCommand(admin, alias);
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"AdminCommandHandler: {ex.Message} {ex.StackTrace}");
            }
        }

        public void Register(object processor)
        {
            CommandAttribute attribute =
                (CommandAttribute)processor.GetType().GetCustomAttribute(typeof(CommandAttribute));
            commands.Add(attribute.CommandName, (AbstractAdminCommand)processor);
        }
    }
}
