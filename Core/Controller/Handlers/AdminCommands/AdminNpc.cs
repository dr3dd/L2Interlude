using Core.Attributes;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 05.11.2024 09:49:56

namespace Core.Controller.Handlers.AdminCommands
{
    [Command(CommandName = "admin_npc")]
    class AdminNpc : AbstractAdminCommand
    {
        private int Page { get; set; } = 0;
        private int Size { get; set; } = 14;
        protected internal override async Task Use(PlayerInstance admin, string alias)
        {
            string[] args = alias.Split(' ');

            if (args.Length > 1)
            {
                if (args.Length == 3)
                {
                    var command = args[1];
                    int objId = 0;
                    int npcId = 0;
                    switch (command)
                    {
                        case "find":
                            await ShowNpcList(admin, args[2].Trim());
                            break;
                        case "page":
                            int p = 0;
                            int.TryParse(args[2].Trim(), out p);
                            Page = p;
                            await ShowNpcList(admin);
                            break;
                        case "show":
                            int.TryParse(args[2].Trim(), out npcId);
                            await ShowNpc(admin, npcId);
                            break;
                        case "show_i":
                            int.TryParse(args[2].Trim(), out objId);
                            await ShowNpcInstance(admin, objId);
                            break;
                        case "goto":
                            int.TryParse(args[2].Trim(), out objId);
                            await GoToNpc(admin, objId);
                            break;
                        default:
                            await ShowNpcList(admin);
                            break;
                    }
                }
                else {
                    await ShowNpcList(admin);
                }         
            }
            else
            {
                await ShowNpcList(admin);
            }
        }

        private async Task GoToNpc(PlayerInstance admin, int objId) {
            NpcInstance npcInstance = Initializer.WorldInit().GetNpcInstance(objId);
            await admin.TeleportToLocation(npcInstance.GetX(), npcInstance.GetY(), npcInstance.GetZ());
            await ShowNpcInstance(admin, objId);
        }
        private async Task ShowNpcInstance(PlayerInstance admin, int objId) {
            var html = Initializer.HtmlCacheInit().GetHtmlText("admin/gm/npc_instance.htm");

            NpcInstance npcInstance = Initializer.WorldInit().GetNpcInstance(objId);
            html = html.Replace("%npcId%", npcInstance.NpcId.ToString());
            html = html.Replace("%objectId%", npcInstance.ObjectId.ToString());

            html = html.Replace("%aiName%", npcInstance.CharacterName);
            html = html.Replace("%aiNameSpace%", npcInstance.NpcAi().GetDefaultNpc().GetType().Namespace);
            html = html.Replace("%aiModule%", npcInstance.NpcAi().GetDefaultNpc().GetType().Name);

            html = html.Replace("%level%", npcInstance.Level.ToString());
            html = html.Replace("%race%", npcInstance.GetStat().Race);

            await admin.ShowHtmText(html);
        }
        private async Task ShowNpc(PlayerInstance admin, int npcId) {
            var html = Initializer.HtmlCacheInit().GetHtmlText("admin/gm/npc_info.htm");

            NpcTemplateInit npcTemplate = Initializer.NpcDataInit().GetNpcTemplate(npcId);
            if (npcTemplate == null)
            {
                await admin.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"Npc template NOT found."));
                await ShowNpcList(admin);
                return;
            }

            IEnumerable<NpcInstance> npcInstances = Initializer.WorldInit().GetAllNpcInstanceByNpcId(npcId);
            string npcInstanceLinks = "";
            int count = npcInstances.Count();
            //TODO adding pagination
            foreach (NpcInstance npc in npcInstances) {
                npcInstanceLinks = npcInstanceLinks + $"<a action=\"bypass -h admin_npc show_i {npc.ObjectId}\">[{npc.ObjectId}] {npc.CharacterName}</a><br1>"; 
            }
            html = html.Replace("%npcInstances%", count == 0 ? "<font color=\"ffad46\">NpcInstances not found</font>" : npcInstanceLinks);
            html = html.Replace("%npc_count%", count.ToString());

            var npcStat = npcTemplate.GetStat();
            html = html.Replace("%name%", npcStat.Name);
            html = html.Replace("%race%", npcStat.Race);
            html = html.Replace("%npcId%", npcStat.Id.ToString());
            html = html.Replace("%level%", npcStat.Level.ToString());

            await admin.ShowHtmText(html);
        }

        private async Task ShowNpcList(PlayerInstance admin, string searchStr = "")
        {
            var html = Initializer.HtmlCacheInit().GetHtmlText("admin/gm/npc_list.htm");

            IEnumerable<NpcTemplateInit> npcList = Initializer.NpcDataInit().GetAllNpcTemplate();
            string npcCount = npcList.Count().ToString();
            int count = 0;
            string npcListLinks = "";
            //search by name
            if (!string.IsNullOrEmpty(searchStr))
            {
                int id = 0;
                int.TryParse(searchStr, out id);
                npcList = npcList.Where(npc => npc.GetStat().Name.ToLowerInvariant().Contains(searchStr.ToLowerInvariant()) || npc.GetStat().Id == id).ToList();
                
            }
            //get range list
            npcList = npcList.Skip(Page* Size).Take((Page * Size) + Size);

            foreach (var npc in npcList)
            {
                if (count == Size)
                {
                    break;
                }
                string NpcName = npc.GetStat().Name;
                int NpcId = npc.GetStat().Id;
                npcListLinks = npcListLinks + $"<a action=\"bypass -h admin_npc show {NpcId}\">[{NpcId}]  {NpcName}</a><br1>";
                count++;
            }

            string pagePrev = "";
            string pageNext = "";
            if (Page > 0) {
                pagePrev = $"<button value=\"Page {Page - 1}\" action=\"bypass admin_npc page {Page - 1}\" width=45 height=15 back=\"sek.cbui94\" fore=\"sek.cbui92\">";
                pageNext = $"<button value=\"Page {Page + 1}\" action=\"bypass admin_npc page {Page + 1}\" width=45 height=15 back=\"sek.cbui94\" fore=\"sek.cbui92\">";
            }
            else if (count>=Size)
            {
                pageNext = $"<button value=\"Page {Page + 1}\" action=\"bypass admin_npc page {Page + 1}\" width=45 height=15 back=\"sek.cbui94\" fore=\"sek.cbui92\">";
            }

            html = html.Replace("%npc_list%", count == 0 ? "<font color=\"ffad46\">NpcTemplates not found</font>" : npcListLinks);
            html = html.Replace("%npc_count%", $"{count} of {npcCount}");
            html = html.Replace("%page_prev%", pagePrev);
            html = html.Replace("%page_next%", pageNext);
            //reset page
            Page = 0;

            await admin.ShowHtmText(html);
        }
    }
}
