using System;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;

namespace Core.Module.NpcData
{
    public sealed class NpcInstance : Character
    {
        private readonly NpcTemplateInit _npcTemplate;
        private readonly NpcKnownList _npcKnownList;
        private readonly NpcUseSkill _npcUseSkill;
        private readonly NpcCombat _npcCombat;
        private readonly NpcStatus _npcStatus;
        private readonly NpcDesire _npcDesire;
        public readonly int NpcId;
        public readonly int NpcHashId;
        
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }
        
        public NpcInstance(int objectId, NpcTemplateInit npcTemplateInit)
        {
            ObjectId = objectId;
            NpcHashId = npcTemplateInit.GetStat().Id + 1000000;
            CharacterName = npcTemplateInit.GetStat().Name;
            NpcId = npcTemplateInit.GetStat().Id;
            _npcKnownList = new NpcKnownList(this);
            _npcUseSkill = new NpcUseSkill(this);
            _npcTemplate = npcTemplateInit;
            _npcCombat = new NpcCombat(this);
            _npcStatus = new NpcStatus(this);
            _npcDesire = new NpcDesire(this);
        }

        public NpcUseSkill NpcUseSkill() => _npcUseSkill;
        public override Weapon GetActiveWeaponItem()
        {
            throw new NotImplementedException();
        }

        public override ICharacterCombat CharacterCombat() => _npcCombat;
        public override ICharacterKnownList CharacterKnownList() => _npcKnownList;
        public NpcTemplateInit GetTemplate() => _npcTemplate;
        public NpcDesire NpcDesire() => _npcDesire;
        
        public void OnSpawn(int x, int y, int z, int h)
        {
            Heading = h;
            SpawnMe(x, y, z);
        }

        private async Task SendRequestAsync(PlayerInstance playerInstance)
        {
            if (_npcTemplate.GetStat().CanBeAttacked == 1)
            {
                await playerInstance.SendPacketAsync(new ValidateLocation(this));
                if (Math.Abs(playerInstance.GetZ() - GetZ()) < 400) // this max height difference might need some tweaking
                {
                    // Set the PlayerInstance Intention to AI_INTENTION_ATTACK
                    playerInstance.CharacterDesire().AddDesire(Desire.AttackDesire, this);
                }
            }
            var npcServerRequest = new NpcServerRequest
            {
                EventName = _npcTemplate.GetStat().CanBeAttacked == 1 ? EventName.Attacked : EventName.Talked,
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

        public override int GetMaxHp()
        {
            return _npcStatus.GetMaxHp();
        }

        public override int GetMagicalAttack()
        {
            return _npcCombat.GetMagicalAttack();
        }

        public override int GetMagicalDefence()
        {
            return _npcCombat.GetMagicalDefence();
        }

        public override int GetPhysicalDefence()
        {
            return _npcCombat.GetPhysicalDefence();
        }

        public override int GetPhysicalAttackSpeed()
        {
            throw new NotImplementedException();
        }

        public override async Task RequestActionAsync(PlayerInstance playerInstance)
        {
            if (await IsTargetSelected(playerInstance))
            {
                await SendRequestAsync(playerInstance);
                return;
            }
            await base.RequestActionAsync(playerInstance);
            await ShowTargetInfoAsync(playerInstance);
        }
        
        private Task<bool> IsTargetSelected(PlayerInstance playerInstance)
        {
            return Task.FromResult(this == playerInstance.PlayerTargetAction().GetTarget());
        }
        
        private async Task ShowTargetInfoAsync(PlayerInstance playerInstance)
        {
            if (_npcTemplate.GetStat().CanBeAttacked == 1)
            {
                await playerInstance.SendPacketAsync(new MyTargetSelected(ObjectId, playerInstance.PlayerStatus().Level - _npcTemplate.GetStat().Level));
                // Send a Server->Client packet StatusUpdate of the NpcInstance to the PlayerInstance to update its HP bar
                StatusUpdate su = new StatusUpdate(ObjectId);
                su.AddAttribute(StatusUpdate.CurHp, (int) CharacterStatus().CurrentHp);
                su.AddAttribute(StatusUpdate.MaxHp, (int) _npcTemplate.GetStat().OrgHp);
                await playerInstance.SendPacketAsync(su);
                return;
            }
            await playerInstance.SendPacketAsync(new MyTargetSelected(ObjectId, 0));
            await playerInstance.SendPacketAsync(new ValidateLocation(this));
        }
    }
}