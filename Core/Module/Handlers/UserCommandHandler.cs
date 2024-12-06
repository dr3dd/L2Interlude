using Core.Attributes;
using Core.Module.Handlers.UserCommands;
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
//DATE: 06.12.2024 13:08:53

namespace Core.Module.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SortedList<int, AbstractUserCommand> commands = new SortedList<int, AbstractUserCommand>();
        public UserCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            IEnumerable<Type> typelist = Utility.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Core.Module.Handlers.UserCommands");
            foreach (Type t in typelist)
            {
                if (!t.Name.StartsWith("Abstract") && t.BaseType.Name.Equals("AbstractUserCommand"))
                {
                    Register(Activator.CreateInstance(t));
                }
            }

            LoggerManager.Info($"UserCommandHandler: Loaded {commands.Count} commands.");
        }

        public async Task Request(PlayerInstance player, int commandId)
        {
 
            if (!commands.ContainsKey(commandId) && player.IsGM)
            {
                await player.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"Command {commandId} not implement."));
                LoggerManager.Warn($"UserCommandHandler: Command {commandId} not implement.");
                return;
            }

            AbstractUserCommand processor = commands[commandId];
            try
            {
                await processor.UseCommand(player, commandId);
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"UserCommandHandler: {ex.Message} {ex.StackTrace}");
            }
        }

        public void Register(object processor)
        {
            CommandAttribute attribute =
                (CommandAttribute)processor.GetType().GetCustomAttribute(typeof(CommandAttribute));
            commands.Add(attribute.CommandId, (AbstractUserCommand)processor);
        }
    }
}
