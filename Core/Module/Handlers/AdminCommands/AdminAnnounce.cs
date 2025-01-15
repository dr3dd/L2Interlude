using Core.Attributes;
using Core.Enums;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using Helpers;
using L2Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 13.01.2025 18:42:50

namespace Core.Module.Handlers.AdminCommands
{
    [Command(CommandName = "admin_announce")]
    class AdminAnnounce : AbstractAdminCommand
    {
        protected internal override async Task Use(PlayerInstance admin, string alias)
        {
            string[] args = alias.Split(' ');

            if (args.Length > 1)
            {
                string cmd = args[1];
                switch (cmd)
                {
                    case "show":
                        await Initializer.ChatHandler().Chat(admin, ChatType.ANNOUNCEMENT, null, string.Join(" ", args.Skip(2)));
                        break;
                    case "add":
                        await AddAnnounce(admin, args);
                        break;
                    case "del":
                        await DelAnnounce(admin, args);
                        break;
                    case "reload":
                    case "start":
                    case "stop":
                        await ControlAnnounce(admin, cmd);
                        break;
                    default:
                        await ShowAnnouncePage(admin);
                        break;
                }
                await ShowAnnouncePage(admin, false);
            }
            else
            {
                await ShowAnnouncePage(admin, false);
            }
        }
        private async Task ControlAnnounce(PlayerInstance admin, string cmd)
        {
            if (cmd == "reload")
            {
                await Initializer.AnnounceManager().Reload();
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Announce reloaded"));
            }
            else if (cmd == "start")
            {
                if (Initializer.AnnounceManager().AnnounceTaskIsRun())
                {
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Announce task already run"));
                }
                else
                {
                    Initializer.AnnounceManager().StartIntervalAnnounceTask();
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Announce task started"));
                }
            }
            else if (cmd == "stop")
            {
                Initializer.AnnounceManager().StopIntervalAnnounceTask();
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Announce task stopped"));
            }
            else
            {
                return;
            }
        }
        private async Task DelAnnounce(PlayerInstance admin, string[] args)
        {
            if (args.Length > 2)
            {
                try
                {
                    int id = int.Parse(args[2]);
                    await Initializer.UnitOfWork().Announce.DeleteAsync(id);
                }
                catch (FormatException e)
                {
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Parameter 'id' incorrect"));
                }
            }
            else
            {
                return;
            }
        }
        private async Task AddAnnounce(PlayerInstance admin, string[] args)
        {
            if (args.Length > 3)
            {
                try
                {
                    int interval = int.Parse(args[2]);
                    string text = string.Join(" ", args.Skip(3));
                    await Initializer.UnitOfWork().Announce.AddAsync(new AnnounceEntity() { Interval = interval, AnnounceMsg = text });
                }
                catch (FormatException e)
                {
                    await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] Parameter 'interval' incorrect"));
                }
            }
            else
            {
                return;
            }
        }

        private async Task ShowAnnouncePage(PlayerInstance admin, bool showHelp = true)
        {
            
            if (showHelp)
            {
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] //announce [start|stop|reload]"));
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] //announce [add] [interval] [msg]"));
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[SYS] //announce [del] [id]"));
            }

            var html = Initializer.HtmlCacheInit().GetHtmlText("admin/game/announce/main.htm");
            var announces = (await Initializer.UnitOfWork().Announce.GetAllAsync()).OrderBy(a => a.Interval).ToList();

            if (announces.Count > 0)
            {
                string announcesList = "<table width=270 border=0>";
                foreach (var item in announces)
                {
                    announcesList += $"<tr><td width=30>{item.Interval}</td><td>{item.AnnounceMsg}</td><td><button value=\"del\" action=\"bypass admin_announce del {item.AnnounceId}\" width=30 height=15 back=\"sek.cbui94\" fore=\"sek.cbui92\"></td></tr>";
                }

                announcesList += "</table>";

                html = html.Replace("%announce_list%", announcesList);
            }
            else
            {
                html = html.Replace("%announce_list%", "<font color=\"ffad46\">announces not found</font>");

            }

            html = html.Replace("%status%", Initializer.AnnounceManager().AnnounceTaskIsRun() ? "ON" : "OFF");

            await admin.ShowHtmText(html);
        }
    }
}
