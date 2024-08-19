using Core.Attributes;
using Core.Module.Player;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:52:56

namespace Core.Controller.Handlers.AdminCommands
{
    [Command(CommandName = "admin_admin")]
    class AdminAdmin : AbstractAdminCommand
    {
        protected internal override void Use(PlayerInstance admin, string alias)
        {
            string[] args = alias.Split(' ');

            if (args.Length > 1)
            {
                if (args[1].EndsWithIgnoreCase(".htm"))
                {
                    admin.ShowHtm($"admin/{args[1]}", admin);
                }
            }
            else
            {

                admin.ShowHtm("admin/admin.htm", admin);
            }
        }
    }
}
