using Core.Attributes;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 16.08.2024 23:10:48

namespace Core.Controller.Handlers.AdminCommands
{
    [Command(CommandName = "admin_teleport")]
    class AdminTeleport : AbstractAdminCommand
    {
        protected internal override async Task Use(PlayerInstance admin, string alias)
        {
            string[] args = alias.Split(' ');
            if (args.Length > 1)
            {
                int len = args[args.Length - 1].EndsWithIgnoreCase(".htm") ? args.Length - 1 : args.Length;
                if (len.Equals(3))
                {
                    int x = int.Parse(args[1]);
                    int y = int.Parse(args[2]);
                    int z = admin.GetZ() + 300; //TODO get height
                    //admin.TeleportToLocation(x, y, z);
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"Teleport need implementation."));
                    await admin.TeleportToLocation(x, y, z);
                }
                else if (len.Equals(4))
                {
                    int x = int.Parse(args[1]);
                    int y = int.Parse(args[2]);
                    int z = int.Parse(args[3]);
                    //admin.TeleportToLocation(x, y, z);
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"Teleport need implementation."));
                    await admin.TeleportToLocation(x, y, z);
                }
            }
            else
            {
                await admin.ShowHtm("admin/teleport/main.htm", admin);
            }

            await LastAction(admin, args);
        }
    }
}
