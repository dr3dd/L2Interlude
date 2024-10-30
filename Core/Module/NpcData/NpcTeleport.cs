using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.NpcData;

public class NpcTeleport
{
    private readonly NpcInstance _npcInstance;
    public NpcTeleport(NpcInstance npcInstance)
    {
        _npcInstance = npcInstance;
    }
        
    public async Task TeleportToLocation(int teleportHashId, int teleportId, PlayerInstance playerInstance)
    {
        var npcTeleport = (Teleporter) _npcInstance.NpcAi().GetDefaultNpc();
        var teleportList = npcTeleport.GetPositionList(teleportHashId)[teleportId];
        await TeleportToLocation(playerInstance, teleportList.GetX, teleportList.GetY, teleportList.GetZ);
    }

    public async Task TeleportToLocation(PlayerInstance playerInstance, int x, int y, int z)
    {
        await playerInstance.CharacterMovement().StopMoveAsync(new Location(playerInstance.GetX(), playerInstance.GetY(), playerInstance.GetZ(), playerInstance.Heading));
        playerInstance.PlayerAction().SetTeleporting(true);
        await playerInstance.CharacterTargetAction().RemoveTargetAsync();
        playerInstance.CharacterKnownList().RemoveMeFromKnownObjects();
        playerInstance.CharacterKnownList().RemoveAllKnownObjects();
        playerInstance.WorldObjectPosition().GetWorldRegion().RemoveFromZones(playerInstance);

        var teleportToLocation = new TeleportToLocation(playerInstance, x, y, z);
        await playerInstance.SendPacketAsync(teleportToLocation);
        await playerInstance.SendToKnownPlayers(teleportToLocation);
        playerInstance.WorldObjectPosition().SetXYZ(x, y, z);
    }

    private async Task ShowTeleportList(string html, PlayerInstance player)
    {
        var htmlText = new NpcHtmlMessage(_npcInstance.ObjectId, html);
        await player.SendPacketAsync(htmlText);
        await player.SendActionFailedPacketAsync();
    }

    public async Task Teleport(Talker talker, IList<TeleportList> position, string shopName, string empty, string s,
        string empty1, int itemId, string itemName)
    {
        var hashCode = position.GetHashCodeByValue().ToString();
        var url = @"<a action=""bypass -h teleport_goto##objectId#?teleportId=#list_hash#,#id#"" msg=""811;#Name#""> #Name# - #Price# Adena </a><br1>";
        var htmlString = string.Empty;
        for (var i1 = 0; i1 < position.Count; i1++)
        {
            var teleportName = position[i1].Name;
            var replace = url.Replace("#objectId#", _npcInstance.ObjectId.ToString());
            replace = replace.Replace("#list_hash#", hashCode);
            replace = replace.Replace("#id#", i1.ToString());
            replace = replace.Replace("#Name#", teleportName);
            replace = replace.Replace("#Price#", position[i1].Price.ToString());
            htmlString += replace;
        }

        var html = "<html><body>&$556;<br><br>" + htmlString + "</body></html>";
        await ShowTeleportList(html, talker.PlayerInstance);
    }


    public async Task TeleportRequest(PlayerInstance playerInstance)
    {
        await _npcInstance.NpcAi().TeleportRequested(playerInstance);
    }
}