using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.NpcData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using DataBase.Interfaces;
using Helpers;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.Module.Player
{
    public sealed class PlayerInstance : Character
    {
        private readonly PlayerAppearance _playerAppearance;
        private readonly PlayerCharacterInfo _playerCharacterInfo;
        private readonly PlayerModel _playerModel;
        private readonly ITemplateHandler _templateHandler;
        private static PlayerLoader _playerLoader;
        private readonly PlayerMoveToLocation _toLocation;
        private readonly PlayerStatus _playerStatus;
        private readonly PlayerCombat _playerCombat;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerUseItem _playerUseItem;
        private readonly PlayerSkill _playerSkill;
        private readonly PlayerEffect _playerEffect;
        private readonly PlayerSkillMagic _playerSkillMagic;
        private readonly PlayerZone _playerZone;
        private readonly PlayerKnownList _playerKnownList;
        private readonly PlayerAction _playerAction;
            
        public Location Location { get; set; }
        public NpcInstance LastTalkedNpc { get; set; }
        public GameServiceController Controller { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly WorldInit _worldInit;
        public PlayerInstance(ITemplateHandler template, PlayerAppearance playerAppearance, IServiceProvider provider, IUnitOfWork unitOfWork) : base(provider)
        {
            CharacterName = playerAppearance.CharacterName;
            _templateHandler = template;
            _playerAppearance = playerAppearance;
            _unitOfWork = unitOfWork;
            _playerModel = new PlayerModel(this);
            _playerAction = new PlayerAction(this);
            _playerCharacterInfo = new PlayerCharacterInfo(this);
            _toLocation = new PlayerMoveToLocation(this);
            _playerStatus = new PlayerStatus(this);
            _playerCombat = new PlayerCombat(this);
            _playerInventory = new PlayerInventory(this);
            _playerUseItem = new PlayerUseItem(this);
            _playerSkill = new PlayerSkill(this);
            _playerEffect = new PlayerEffect(this);
            _playerSkillMagic = new PlayerSkillMagic(this);
            _playerZone = new PlayerZone(this);
            _playerKnownList = new PlayerKnownList(this);
            _notifyEvent = new PlayerNotifyEvent(this);

            _worldInit = provider.GetRequiredService<WorldInit>();
        }

        public IUnitOfWork GetUnitOfWork() => _unitOfWork;

        public ITemplateHandler TemplateHandler() => _templateHandler;
        public PlayerAppearance PlayerAppearance() => _playerAppearance;
        public PlayerModel PlayerModel() => _playerModel;
        public PlayerCharacterInfo PlayerCharacterInfo() => _playerCharacterInfo;
        public PlayerStatus PlayerStatus() => _playerStatus;
        public PlayerCombat PlayerCombat() => _playerCombat;
        public PlayerInventory PlayerInventory() => _playerInventory;
        public PlayerUseItem PlayerUseItem() => _playerUseItem;
        public PlayerSkill PlayerSkill() => _playerSkill;
        //public PlayerEffect PlayerEffect() => _playerEffect;
        public PlayerSkillMagic PlayerSkillMagic() => _playerSkillMagic;
        public PlayerZone PlayerZone() => _playerZone;
        public PlayerAction PlayerAction() => _playerAction;
        public override Weapon GetActiveWeaponItem()
        {
            var itemInstance = PlayerInventory().GetBodyPartBySlotId((int)SlotBitType.RightHand);
            itemInstance.ItemData ??= new Weapon(0, WeaponType.None);
            return itemInstance.ItemData as Weapon;
        }

        public override ICharacterCombat CharacterCombat() => _playerCombat;
        public override ICharacterKnownList CharacterKnownList() => _playerKnownList;

        private static PlayerLoader PlayerLoader(IServiceProvider serviceProvider)
        {
            return _playerLoader ??= new PlayerLoader(serviceProvider);
        }

        public static Task<PlayerInstance> Load(int objectId, IServiceProvider serviceProvider)
        {
            return PlayerLoader(serviceProvider).Load(objectId);
        }
        
        public override Task SendPacketAsync(ServerPacket serverPacket)
        {
            if (Controller is null)
                return Task.CompletedTask;
            return Controller.SendPacketAsync(serverPacket);
        }
        
        public override async Task SendActionFailedPacketAsync()
        {
            await Controller.SendPacketAsync(new ActionFailed());
        }
        
        public async Task SendUserInfoAsync()
        {
            if (Controller is null) return;
            await Controller.SendPacketAsync(new UserInfo(this));
            //Broadcast.toKnownPlayers(this, new CharInfo(this)); TODO
        }
        
        public PlayerMoveToLocation PlayerLocation()
        {
            return _toLocation;
        }
        
        public async Task FindCloseNpc()
        {
            foreach (NpcInstance npcInstance in _worldInit.GetVisibleNpc(this))
            {
                if (!CalculateRange.CheckIfInRange(2000, npcInstance.GetX(), npcInstance.GetY(),
                        npcInstance.GetZ(), npcInstance.CharacterCombat().GetCollisionRadius(),
                        GetX(), GetY(), GetZ(), CharacterCombat().GetCollisionRadius(), true))
                {
                    continue;
                }
                if (CharacterKnownList().HasObjectInKnownList(npcInstance.ObjectId))
                {
                    continue;
                }
                /*
                var npcServerRequest = new NpcServerRequest
                {
                    EventName = EventName.Created,
                    NpcName = npcInstance.GetTemplate().GetStat().Name,
                    NpcType = npcInstance.GetTemplate().GetStat().Type,
                    NpcObjectId = npcInstance.ObjectId,
                    IsActiveNpc = true
                };
                await SendObjectToNpcServerAsync(npcServerRequest);
                */

                npcInstance.NpcAi().Created();
                CharacterKnownList().AddToKnownList(npcInstance.ObjectId, npcInstance);
                npcInstance.CharacterKnownList().AddToKnownList(ObjectId, this);
                await SendPacketAsync(new NpcInfo(npcInstance));
            }
        }

        /* delete
        public async Task SendObjectToNpcServerAsync(NpcServerRequest npcServerRequest)
        {
            await ServiceProvider.GetRequiredService<NpcServiceController>()
                    .SendMessageToNpcService(npcServerRequest);
        }
        */


        public override async Task SendToKnownPlayers(ServerPacket packet)
        {
            foreach (var (objectId, worldObject) in CharacterKnownList().GetKnownObjects())
            {
                if (worldObject is PlayerInstance targetInstance)
                {
                    await targetInstance.SendPacketAsync(packet);
                    await SendPacketAsync(new CharInfo(targetInstance));
                }
            }
        }

        public async Task DeleteMeAsync()
        {
            if (GetWorldRegion() != null)
            {
                GetWorldRegion().RemoveFromZones(this);
            }
            await CharacterTargetAction().RemoveTargetAsync();
            //_worldInit.RemoveObject(this);
            _worldInit.RemoveFromAllPlayers(this);
            _worldInit.RemoveVisibleObject(this, WorldObjectPosition().GetWorldRegion());
            WorldObjectPosition().SetWorldRegion(null);
            
            CharacterKnownList().RemoveMeFromKnownObjects();
            CharacterKnownList().RemoveAllKnownObjects();
        }

        public override int GetMaxHp()
        {
            return _playerStatus.GetMaxHp();
        }
        
        public override double GetHpRegenRate()
        {
            return _playerStatus.GetHpRegenRate();
        }

        public override int GetMagicalAttack()
        {
            return _playerCombat.GetMagicalAttack();
        }

        public override int GetMagicalDefence()
        {
            return _playerCombat.GetMagicalDefence();
        }

        public override int GetPhysicalDefence()
        {
            return _playerCombat.GetPhysicalDefence();
        }

        public override int GetPhysicalAttackSpeed()
        {
            return _playerCombat.GetPhysicalAttackSpeed();
        }

        private Task<bool> IsTargetSelected(PlayerInstance playerInstance)
        {
            return Task.FromResult(this == playerInstance.CharacterTargetAction().GetTarget());
        }

        public override async Task RequestActionAsync(PlayerInstance playerInstance)
        {
            if (!await IsTargetSelected(playerInstance))
            {
                await base.RequestActionAsync(playerInstance);
                // Set the target of the PlayerInstance player
                await playerInstance.SendPacketAsync(new MyTargetSelected(this.ObjectId, 0));
                return;
            }
            playerInstance.CharacterDesire().AddDesire(Desire.InteractDesire, this);
        }

        public override void SpawnMe(int x, int y, int z)
        {
            base.SpawnMe(x, y, z);
            CharacterMovement().SetRunning();
            StorePlayerObject();
        }
        private void StorePlayerObject()
        {
            Initializer.WorldInit().StorePlayerObject(this);
        }

        public override async Task SendStatusUpdate()
        {
            var su = new StatusUpdate(this);
            await SendPacketAsync(su);
        }
    }
}