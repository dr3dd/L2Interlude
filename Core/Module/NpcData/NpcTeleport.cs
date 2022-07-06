using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.NpcData
{
    public static class NpcTeleport
    {
        
        public static async Task TeleportRequest(PlayerInstance playerInstance, NpcInstance npcInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.TeleportRequest,
                NpcName = npcInstance.GetTemplate().GetStat().Name,
                NpcType = npcInstance.GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = npcInstance.ObjectId
            };
            await Initializer.SendMessageToNpcService(npcServerRequest);
        }
        
        public static async Task TeleportToLocation(int teleportId, PlayerInstance playerInstance, NpcInstance npcInstance)
        {
            var npcServerRequest = new NpcServerRequest
            {
                EventName = EventName.TeleportRequested,
                NpcName = npcInstance.GetTemplate().GetStat().Name,
                NpcType = npcInstance.GetTemplate().GetStat().Type,
                PlayerObjectId = playerInstance.ObjectId,
                NpcObjectId = npcInstance.ObjectId,
                TeleportId = teleportId
            };
            await Initializer.SendMessageToNpcService(npcServerRequest);
        }
        
        public static async Task TeleportToLocation(int getX, int getY, int getZ, PlayerInstance playerInstance)
        {
            await playerInstance.CharacterMovement().StopMoveAsync(new Location(playerInstance.GetX(), playerInstance.GetY(), playerInstance.GetZ(), playerInstance.Heading));
            playerInstance.PlayerAction().SetTeleporting(true);
            await playerInstance.PlayerTargetAction().RemoveTargetAsync();
            playerInstance.CharacterKnownList().RemoveMeFromKnownObjects();
            playerInstance.CharacterKnownList().RemoveAllKnownObjects();
            playerInstance.WorldObjectPosition().GetWorldRegion().RemoveFromZones(playerInstance);

            var teleportToLocation = new TeleportToLocation(playerInstance, getX, getY, getZ);
            await playerInstance.SendPacketAsync(teleportToLocation);
            await playerInstance.SendToKnownPlayers(teleportToLocation);
            playerInstance.WorldObjectPosition().SetXYZ(getX, getY, getZ);
        }
        
        public static async Task ShowTeleportList(string html, PlayerInstance player, NpcInstance npcInstance)
        {
            var htmlText = new NpcHtmlMessage(npcInstance.ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
    }
}