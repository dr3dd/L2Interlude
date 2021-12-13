using System.IO;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData
{
    public sealed class NpcInstance : Character
    {
        private readonly NpcTemplateInit _npcTemplate;
        public readonly int NpcHashId;
        public int Heading;
        public NpcInstance(int objectId, NpcTemplateInit npcTemplateInit)
        {
            ObjectId = objectId;
            NpcHashId = npcTemplateInit.GetStat().Id + 1000000;
            _npcTemplate = npcTemplateInit;
        }

        public NpcTemplateInit GetTemplate()
        {
            return _npcTemplate;
        }
        
        public void OnSpawn(int x, int y, int z, int h)
        {
            Heading = h;
            SpawnMe(x, y, z);
        }

        public async Task OnActionAsync(PlayerInstance playerInstance)
        {
            await SendRequestAsync(playerInstance);
        }

        private async Task SendRequestAsync(PlayerInstance playerInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.Talked,
                NpcName = GetTemplate().GetStat().Name,
                NpcType = GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = ObjectId
            };
            await playerInstance.ServiceProvider.GetRequiredService<NpcServiceController>().SendMessageToNpcService(npcServerRequest);
        }

        public async Task ShowPage(PlayerInstance player, string fnHi)
        {
            var html = Initializer.HtmlCacheInit().GetHtmlText(fnHi);
            var htmlText = new NpcHtmlMessage(ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
    }
}