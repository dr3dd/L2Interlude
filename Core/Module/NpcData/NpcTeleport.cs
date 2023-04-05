using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.NpcAi.Ai.NpcType;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.NpcData
{
    public static class NpcTeleport
    {
        
        public static async Task TeleportRequest(PlayerInstance playerInstance, NpcInstance npcInstance)
        {
            await npcInstance.NpcAi().TeleportRequested(playerInstance);
        }
        
        public static async Task TeleportToLocation(int teleportId, PlayerInstance playerInstance, NpcInstance npcInstance)
        {
            var npcTeleport = (Teleporter) npcInstance.NpcAi().GetDefaultNpc();
            var teleportList = npcTeleport.Position[teleportId];
            await npcInstance.DoTeleportToLocation(teleportList, playerInstance);
        }
        
        public static async Task TeleportToLocation(int getX, int getY, int getZ, PlayerInstance playerInstance)
        {
            await playerInstance.CharacterMovement().StopMoveAsync(new Location(playerInstance.GetX(), playerInstance.GetY(), playerInstance.GetZ(), playerInstance.Heading));
            playerInstance.PlayerAction().SetTeleporting(true);
            await playerInstance.CharacterTargetAction().RemoveTargetAsync();
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