using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.Module.NpcData
{
    public sealed class NpcInstance : Character
    {
        private readonly NpcTemplateInit _npcTemplate;
        private readonly NpcKnownList _playerKnownList;
        public readonly int NpcHashId;
        public int Heading;
        public NpcInstance(int objectId, NpcTemplateInit npcTemplateInit)
        {
            ObjectId = objectId;
            NpcHashId = npcTemplateInit.GetStat().Id + 1000000;
            _playerKnownList = new NpcKnownList(this);
            _npcTemplate = npcTemplateInit;
        }

        public NpcKnownList NpcKnownList() => _playerKnownList;
        
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

        public async Task SendToKnownPlayers(ServerPacket packet)
        {
            foreach (var (objectId, playerInstance) in NpcKnownList().GetKnownPlayers())
            {
                await playerInstance.SendPacketAsync(packet);
            }
        }

        public async Task TeleportRequest(PlayerInstance playerInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.TeleportRequest,
                NpcName = GetTemplate().GetStat().Name,
                NpcType = GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = ObjectId
            };
            await playerInstance.ServiceProvider.GetRequiredService<NpcServiceController>().SendMessageToNpcService(npcServerRequest);
        }

        public async Task ShowTeleportList(string html, PlayerInstance player)
        {
            var htmlText = new NpcHtmlMessage(ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }

        public async Task TeleportToLocation(int teleportId, PlayerInstance playerInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.TeleportRequested,
                NpcName = GetTemplate().GetStat().Name,
                NpcType = GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = ObjectId,
                TeleportId = teleportId
            };
            await playerInstance.ServiceProvider.GetRequiredService<NpcServiceController>().SendMessageToNpcService(npcServerRequest);
        }

        public async Task DoTeleportToLocation(TeleportList teleport, PlayerInstance player)
        {
            await TeleportToLocation(teleport.GetX, teleport.GetY, teleport.GetZ, player);
        }

        private async Task TeleportToLocation(int getX, int getY, int getZ, PlayerInstance playerInstance)
        {
            await playerInstance.PlayerTargetAction().RemoveTargetAsync();
            playerInstance.WorldObjectPosition().GetWorldRegion().RemoveFromZones(playerInstance);

            var tele = new TeleportToLocation(playerInstance, getX, getY, getZ);
            await playerInstance.SendPacketAsync(tele);
            await playerInstance.SendToKnownPlayers(tele);
            playerInstance.WorldObjectPosition().SetXYZ(getX, getY, getZ);
            playerInstance.PlayerZone().RevalidateZone();
            await playerInstance.SendActionFailedPacketAsync();
        }
    }
}