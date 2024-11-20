using Core.Module.NpcAi;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 20.11.2024 1:47:23

namespace Core.Module.NpcData
{
    public class NpcChoice
    {
        private readonly NpcInstance _npcInstance;
        private ConcurrentDictionary<int, string> _choices;
        public NpcChoice(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _choices = new ConcurrentDictionary<int, string>();
        }

        public void Add(int choice, string value)
        {
            _choices.TryAdd(choice, value);
        }

        public void Clear()
        {
            _choices.Clear();
        }

        public async Task ShowChoice(Talker talker, int option)
        {
            var htmlString = string.Empty;
            var url = @"<a action=""bypass -h quest_choice##objectId#?choice=#choice#&option=#option#"">#value#</a><br1>";
            foreach (var choice in _choices)
            {
                var replace = url.Replace("#objectId#", _npcInstance.ObjectId.ToString());
                replace = replace.Replace("#choice#", choice.Key.ToString());
                replace = replace.Replace("#option#", option.ToString());
                replace = replace.Replace("#value#", choice.Value);
                htmlString += replace;
            }
            Clear();
            var html = "<html><body><br>" + htmlString + "</body></html>";
            await ShowChoicePage(html, talker.PlayerInstance);
        }

        private async Task ShowChoicePage(string html, PlayerInstance player)
        {
            var htmlText = new NpcHtmlMessage(_npcInstance.ObjectId, html);
            await player.SendPacketAsync(htmlText);
            await player.SendActionFailedPacketAsync();
        }
    }
}
