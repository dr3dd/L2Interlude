using Core.Module.NpcAi;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 20.11.2024 1:47:23

namespace Core.Module.NpcData
{
    public class NpcChoice
    {
        private readonly NpcAi _npcAi;
        public NpcChoice(NpcAi npcAi)
        {
            _npcAi = npcAi;
        }

        public async Task ShowChoice(Talker talker, int option)
        {
            var htmlString = string.Empty;
            var url = @"<a action=""bypass -h quest_choice##objectId#?choice=#choice#&option=#option#"">#value#</a><br1>";
            foreach (var choice in _npcAi.GetTalker().GetQuestChoiceCollection())
            {
                var replace = url.Replace("#objectId#", _npcAi.NpcInstance().ObjectId.ToString());
                replace = replace.Replace("#choice#", choice.Key.ToString());
                replace = replace.Replace("#option#", option.ToString());
                replace = replace.Replace("#value#", choice.Value);
                htmlString += replace;
            }
            var html = "<html><body><br>" + htmlString + "</body></html>";
            await ShowChoicePage(html, talker.PlayerInstance);
        }

        private async Task ShowChoicePage(string html, PlayerInstance player)
        {
            var htmlText = new NpcHtmlMessage(_npcAi.NpcInstance().ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
    }
}
