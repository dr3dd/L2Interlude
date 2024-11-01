using System.Threading.Tasks;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.NpcData
{
    public static class NpcChatWindow
    {
        public static async Task ShowPage(PlayerInstance player, string fnHi, NpcInstance npcInstance)
        {
            if (player.IsGM)
            {
                await player.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[HTML] {fnHi}"));
            }
            var html = Initializer.HtmlCacheInit().GetHtmlText(fnHi);
            var htmlText = new NpcHtmlMessage(npcInstance.ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
        
        public static async Task ShowShiftPage(PlayerInstance player, NpcInstance npcInstance)
        {
            if (player.IsGM)
            {
                await player.SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[HTML] generated on the fly"));
            }
            var html = Initializer.HtmlCacheInit().GetHtmlText("templates/onShiftNpc.htm");
                html = html.Replace("%npcId%", npcInstance.NpcId.ToString());
                html = html.Replace("%objectId%", npcInstance.ObjectId.ToString());
               
                html = html.Replace("%aiName%", npcInstance.CharacterName);
                html = html.Replace("%aiNameSpace%", npcInstance.NpcAi().GetDefaultNpc().GetType().Namespace);
                html = html.Replace("%aiModule%", npcInstance.NpcAi().GetDefaultNpc().GetType().Name);

                html = html.Replace("%level%", npcInstance.Level.ToString());
                html = html.Replace("%race%", npcInstance.GetStat().Race);
                html = html.Replace("%sex%", npcInstance.GetStat().Sex);
                html = html.Replace("%str%", npcInstance.GetStat().Str.ToString());
                html = html.Replace("%dex%", npcInstance.GetStat().Dex.ToString());
                html = html.Replace("%con%", npcInstance.GetStat().Con.ToString());
                html = html.Replace("%int%", npcInstance.GetStat().Int.ToString());
                html = html.Replace("%wit%", npcInstance.GetStat().Wit.ToString());
                html = html.Replace("%men%", npcInstance.GetStat().Men.ToString());
            var htmlText = new NpcHtmlMessage(npcInstance.ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
        
        public static async Task MenuSelect(int askId, int replyId, PlayerInstance playerInstance, NpcInstance npcInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.MenuSelect,
                NpcName = npcInstance.GetTemplate().GetStat().Name,
                NpcType = npcInstance.GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = npcInstance.ObjectId,
                AskId = askId,
                ReplyId = replyId,
            };
            await Initializer.SendMessageToNpcService(npcServerRequest);
        }
    }
}