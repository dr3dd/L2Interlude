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
        internal void UseCommand(PlayerInstance admin, string alias)
        {
            /* TODO
            if (!CheckCommand(admin, alias))
            {
                admin.SendPacketAsync(new SystemMessage("Access denied"));
                return;
            }
            */

            LoggerManager.Debug($"{GetType().Name}: char {admin.CharacterName} alias {alias}");

            Use(admin, alias);
        }

        protected internal abstract void Use(PlayerInstance admin, string command);

        public void LastAction(PlayerInstance admin, string[] args)
        {
            int len = args.Length;
            if (len > 2)
            {
                if (args[len - 1].EndsWithIgnoreCase(".htm"))
                {
                    admin.ShowHtm($"admin/{args[len - 1]}", admin);
                }
            }
        }
    }
}
