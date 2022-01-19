using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using Network;

namespace Core.Module.NpcData
{
    public sealed class NpcInstance : Character
    {
        private readonly NpcTemplateInit _npcTemplate;
        private readonly NpcKnownList _playerKnownList;
        private readonly NpcUseSkill _npcUseSkill;
        public readonly int NpcHashId;
        public int Heading;
        public NpcInstance(int objectId, NpcTemplateInit npcTemplateInit)
        {
            ObjectId = objectId;
            NpcHashId = npcTemplateInit.GetStat().Id + 1000000;
            _playerKnownList = new NpcKnownList(this);
            _npcUseSkill = new NpcUseSkill(this);
            _npcTemplate = npcTemplateInit;
        }

        public NpcKnownList NpcKnownList() => _playerKnownList;
        public NpcUseSkill NpcUseSkill() => _npcUseSkill;
        
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
            await Initializer.SendMessageToNpcService(npcServerRequest);
        }

        public async Task ShowPage(PlayerInstance player, string fnHi)
        {
            await NpcChatWindow.ShowPage(player, fnHi, this);
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
            await NpcTeleport.TeleportRequest(playerInstance, this);
        }

        public async Task ShowTeleportList(string html, PlayerInstance player)
        {
            await NpcTeleport.ShowTeleportList(html, player, this);
        }

        public async Task TeleportToLocation(int teleportId, PlayerInstance playerInstance)
        {
            await NpcTeleport.TeleportToLocation(teleportId, playerInstance, this);
        }

        public async Task DoTeleportToLocation(TeleportList teleport, PlayerInstance player)
        {
            await NpcTeleport.TeleportToLocation(teleport.GetX, teleport.GetY, teleport.GetZ, player);
        }

        public async Task MenuSelect(int askId, int replyId, PlayerInstance playerInstance)
        {
            await NpcChatWindow.MenuSelect(askId, replyId, playerInstance, this);
        }

        public async Task CastleGateOpenClose(string doorName, int openClose, PlayerInstance player)
        {
            await player.SendPacketAsync(new DoorStatusUpdate(ObjectId, openClose));
        }

        public async Task ShowSkillList(PlayerInstance playerInstance)
        {
            await NpcLearnSkill.ShowSkillList(playerInstance);
        }

        public async Task LearnSkillRequest(PlayerInstance playerInstance)
        {
            await NpcLearnSkill.LearnSkillRequest(playerInstance, this);
        }
    }
}