using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestBypass : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        private readonly string _command;
        private readonly WorldInit _worldInit;
        
        public RequestBypass(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _worldInit = serviceProvider.GetRequiredService<WorldInit>();
            _command = packet.ReadString();
        }

        public override async Task Execute()
        {
            var split = _command.Split("#");
            var command = split.First();
            switch (command)
            {
                case "teleport_request":
                {
                    var npcObjectId = Convert.ToInt32(split.Last());
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.TeleportRequest(_playerInstance);
                    break;
                }
                case "teleport_goto":
                {
                    var parseNpc = split[1].Split("?");
                    var npcObjectId = Convert.ToInt32(parseNpc.First());
                    var teleportId = Convert.ToInt32(parseNpc.Last().Split("=")[1]);
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.TeleportToLocation(teleportId, _playerInstance);
                    break;
                }
                case "menu_select":
                {
                    var spl = split.Last();
                    int charLocation = spl.IndexOf("?", StringComparison.Ordinal);
                    int npcObjectId = Convert.ToInt32(spl[..charLocation]);
                    //var match = Regex.Match(spl, @"ask=(.+?)&").Groups[1].Value;
                    int askId  = Convert.ToInt32(BetweenStrings(spl, "ask=", "&"));
                    
                    int lasCharLocation = spl.LastIndexOf("=", StringComparison.Ordinal);
                    int replyId = Convert.ToInt32(spl.Substring(lasCharLocation + 1));
                    
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.MenuSelect(askId, replyId, _playerInstance);
                    break;
                }
                case "learn_skill":
                {
                    var npcObjectId = Convert.ToInt32(split.Last());
                    var npcInstance = GetNpcInstance(npcObjectId);
                    await npcInstance.LearnSkillRequest(_playerInstance);
                    break;
                }
            }

            var id = _command;
            
        }

        public string BetweenStrings(string text, string start, string end)
        {
            int p1 = text.IndexOf(start, StringComparison.Ordinal) + start.Length;
            int p2 = text.IndexOf(end, p1, StringComparison.Ordinal);

            return end == "" ? text[p1..] : text.Substring(p1, p2 - p1);
        }
        
        private NpcInstance GetNpcInstance(int objectId)
        {
            return _worldInit.GetNpcInstance(objectId);
        }
    }
}