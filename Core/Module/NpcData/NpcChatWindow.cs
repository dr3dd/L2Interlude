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