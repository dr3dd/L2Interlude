using Core.Enums;
using Core.Module.NpcAi;
using Core.Module.NpcAi.Ai;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.11.2024 15:09:30

namespace Core.Module.NpcData
{
    public class NpcRadar
    {
        private readonly NpcInstance _npcInstance;
        public NpcRadar(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }

        public async Task ShowPositionOnRadar(int radarHashId, int radarId, PlayerInstance playerInstance)
        {
            var npcTeleport = (AnnounceRaidBossPosition)_npcInstance.NpcAi().GetDefaultNpc();
            var radarPosition = npcTeleport.GetPositionList(radarHashId)[radarId];
            await playerInstance.SendPacketAsync(new RadarControl(RadarControlType.DELETE_ALL, RadarPositionType.BOTH, radarPosition.GetX, radarPosition.GetY, radarPosition.GetZ));
            await playerInstance.SendPacketAsync(new RadarControl(RadarControlType.SHOW, RadarPositionType.BOTH, radarPosition.GetX, radarPosition.GetY, radarPosition.GetZ));
        }

        private async Task ShowRadarList(string html, PlayerInstance player)
        {
            var htmlText = new NpcHtmlMessage(_npcInstance.ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }

        public async Task Radar(Talker talker, IList<TeleportList> position)
        {
            var hashCode = position.GetHashCodeByValue().ToString();
            var url = @"<a action=""bypass -h show_radar##objectId#?radarId=#list_hash#,#id#""> #Name# </a><br1>";
            var htmlString = string.Empty;
            for (var i1 = 0; i1 < position.Count; i1++)
            {
                var positionName = position[i1].Name;
                var replace = url.Replace("#objectId#", _npcInstance.ObjectId.ToString());
                replace = replace.Replace("#list_hash#", hashCode);
                replace = replace.Replace("#id#", i1.ToString());
                replace = replace.Replace("#Name#", positionName);
                htmlString += replace;
            }

            var html = "<html><body><br><br>" + htmlString + "</body></html>";
            await ShowRadarList(html, talker.PlayerInstance);
        }
    }
}
