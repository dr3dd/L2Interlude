using Core.Attributes;
using Core.Module.Player;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 28.11.2024 20:48:14

namespace Core.Module.Handlers.AdminCommands
{
    [Command(CommandName = "admin_item")]
    class AdminItem : AbstractAdminCommand
    {
        protected internal override async Task Use(PlayerInstance admin, string alias)
        {
            string[] args = alias.Split(' ');
            if (args.Length > 1)
            {
                if (args.Length >= 3) 
                {
                    var command = args[1];
                    switch (command)
                    {
                        case "create":
                            await CreateItem(admin, Regex.Replace(args[2], @"\s+", "", RegexOptions.Multiline));
                            break;
                        default:
                            await ShowItemPage(admin);
                            break;
                    }
                }
                else
                {
                    await ShowItemPage(admin);
                }
            }
            else 
            {
                await ShowItemPage(admin);
            }
                
        }

        private async Task CreateItem(PlayerInstance admin, string item)
        {
            var itemSplit = item.Split(",");
            int item_id = Convert.ToInt32(itemSplit[0]);
            int count = 1;
            if (itemSplit.Length > 1)
            {
                int.TryParse(itemSplit[1], out count);
            }
            await admin.PlayerInventory().AddOrUpdate().AddOrUpdateItemToInventory(item_id, count);
            await ShowItemPage(admin);
        }

        private async Task ShowItemPage(PlayerInstance admin) {
            var html = Initializer.HtmlCacheInit().GetHtmlText("admin/gm/item/main.htm");
            await admin.ShowHtmText(html);
        }
    }
}
