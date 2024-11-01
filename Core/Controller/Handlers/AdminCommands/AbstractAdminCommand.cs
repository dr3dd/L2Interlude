using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using L2Logger;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:04:34

namespace Core.Controller.Handlers.AdminCommands
{
    public abstract class AbstractAdminCommand
    {
        internal async Task UseCommand(PlayerInstance admin, string alias)
        {
            if (!Initializer.AdminAccessManager().CheckCommand(admin, alias))
            {
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString("Access denied"));
                return;
            }

            LoggerManager.Debug($"{GetType().Name}: char {admin.CharacterName} alias {alias}");

            await Use(admin, alias);
        }

        protected internal abstract Task Use(PlayerInstance admin, string command);

        public async Task LastAction(PlayerInstance admin, string[] args)
        {
            int len = args.Length;
            if (len > 2)
            {
                if (args[len - 1].EndsWithIgnoreCase(".htm"))
                {
                    await admin.ShowHtm($"admin/{args[len - 1]}");
                }
            }
        }
    }
}
