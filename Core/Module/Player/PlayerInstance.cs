using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.DoorData;
using Core.Module.ItemData;
using Core.Module.NpcData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using DataBase.Interfaces;
using Helpers;
using L2Logger;
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
        private readonly PlayerBaseStatus _playerBaseStatus;
        private readonly PlayerCombat _playerCombat;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerUseItem _playerUseItem;
        private readonly PlayerSkill _playerSkill;
        private readonly PlayerEffect _playerEffect;
        private readonly PlayerSkillMagic _playerSkillMagic;
        private readonly PlayerZone _playerZone;
        private readonly PlayerKnownList _playerKnownList;
        private readonly PlayerAction _playerAction;
        private readonly PlayerShortCut _shortCut;
        private readonly PlayerMacros _macros;

        public Location Location { get; set; }
        public NpcInstance LastTalkedNpc { get; set; }
        public GameServiceController Controller { get; set; }
        private readonly IUnitOfWorkGame _unitOfWork;
        private readonly WorldInit _worldInit;
        public PlayerInstance(ITemplateHandler template, PlayerAppearance playerAppearance, IServiceProvider provider, IUnitOfWorkGame unitOfWork) : base(provider)
        {
            CharacterName = playerAppearance.CharacterName;
            _templateHandler = template;
            _playerAppearance = playerAppearance;
            _unitOfWork = unitOfWork;
            _playerModel = new PlayerModel(this);
            _playerAction = new PlayerAction(this);
            _playerCharacterInfo = new PlayerCharacterInfo(this);
            _toLocation = new PlayerMoveToLocation(this);
            _playerBaseStatus = new PlayerBaseStatus(this);
            _playerCombat = new PlayerCombat(this);
            _playerInventory = new PlayerInventory(this);
            _playerUseItem = new PlayerUseItem(this);
            _playerSkill = new PlayerSkill(this);
            _playerEffect = new PlayerEffect(this);
            _playerSkillMagic = new PlayerSkillMagic(this);
            _playerZone = new PlayerZone(this);
            _playerKnownList = new PlayerKnownList(this);
            _notifyEvent = new PlayerNotifyEvent(this);
            _shortCut = new PlayerShortCut(this);
            _macros = new PlayerMacros(this);

            Level = PlayerStatus().Level;
            _worldInit = provider.GetRequiredService<WorldInit>();
        }

        public IUnitOfWorkGame GetUnitOfWork() => _unitOfWork;

        public ITemplateHandler TemplateHandler() => _templateHandler;
        public PlayerAppearance PlayerAppearance() => _playerAppearance;
        public PlayerModel PlayerModel() => _playerModel;
        public PlayerCharacterInfo PlayerCharacterInfo() => _playerCharacterInfo;
        public PlayerBaseStatus PlayerStatus() => _playerBaseStatus;
        public PlayerCombat PlayerCombat() => _playerCombat;
        public PlayerInventory PlayerInventory() => _playerInventory;
        public PlayerUseItem PlayerUseItem() => _playerUseItem;
        public PlayerSkill PlayerSkill() => _playerSkill;
        //public PlayerEffect PlayerEffect() => _playerEffect;
        public PlayerSkillMagic PlayerSkillMagic() => _playerSkillMagic;
        public PlayerZone PlayerZone() => _playerZone;
        public PlayerAction PlayerAction() => _playerAction;
        public PlayerShortCut PlayerShortCut() => _shortCut;
        public PlayerMacros PlayerMacros() => _macros;
        public override Weapon GetActiveWeaponItem()
        {
            var itemInstance = _playerCombat.GetWeapon();
            itemInstance.ItemData ??= new Weapon(0, WeaponType.None);
            return itemInstance.ItemData as Weapon;
        }

        public override ICharacterCombat CharacterCombat() => _playerCombat;
        public override IPlayerBaseStatus CharacterBaseStatus() => _playerBaseStatus;
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

        public async Task ShowHtm(string fileName)
        {
            if (IsGM)
            {
                await SendPacketAsync(new SystemMessage(SystemMessageId.S1).AddString($"[HTML] {fileName}"));
            }

            if (fileName.EndsWithIgnoreCase(".htm"))
            {
                await SendPacketAsync(new NpcHtmlMessage(this, fileName, ObjectId));

            }
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
                        GetX(), GetY(), GetZ(), CharacterCombat().GetCollisionRadius(), false))
                {
                    continue;
                }
                if (CharacterKnownList().HasObjectInKnownList(npcInstance.ObjectId))
                {
                    continue;
                }
                await SendPacketAsync(new NpcInfo(npcInstance));
                //LoggerManager.Info($"Show NPC {npcInstance.ObjectId}");
                npcInstance.NpcAi().Created();
                CharacterKnownList().AddToKnownList(npcInstance.ObjectId, npcInstance);
                npcInstance.CharacterKnownList().AddToKnownList(ObjectId, this);
            }
        }

        public async Task FindCloseDoor()
        {
            foreach (DoorInstance doorInstance in _worldInit.GetVisibleDoor(this))
            {
                if (!CalculateRange.CheckIfInRange(2000, doorInstance.GetX(), doorInstance.GetY(),
                        doorInstance.GetZ(), 70,
                        GetX(), GetY(), GetZ(), CharacterCombat().GetCollisionRadius(), false))
                {
                    continue;
                }
                if (CharacterKnownList().HasObjectInKnownList(doorInstance.ObjectId))
                {
                    continue;
                }
                CharacterKnownList().AddToKnownList(doorInstance.ObjectId, doorInstance);
                doorInstance.DoorKnownList().AddToKnownList(ObjectId, this);
                await SendPacketAsync(new DoorInfo(doorInstance, true));
            }
        }

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
            CharacterMovement().CharacterMovementStatus().SetGroundHigh();
            StorePlayerObject();
        }
        private void StorePlayerObject()
        {
            ServiceProvider.GetRequiredService<WorldInit>().StorePlayerObject(this);
        }

        public override async Task SendStatusUpdate()
        {
            var su = new StatusUpdate(this);
            await SendPacketAsync(su);
        }

        public override Task DoDieProcess()
        {
            throw new NotImplementedException();
        }

        public async Task TeleportToLocation(int x, int y, int z)
        {
            await CharacterMovement().StopMoveAsync(new Location(GetX(), GetY(), GetZ(), Heading));
            PlayerAction().SetTeleporting(true);
            await CharacterTargetAction().RemoveTargetAsync();
            CharacterKnownList().RemoveMeFromKnownObjects();
            CharacterKnownList().RemoveAllKnownObjects();
            WorldObjectPosition().GetWorldRegion().RemoveFromZones(this);

            var teleportToLocation = new TeleportToLocation(this, x, y, z);
            await SendPacketAsync(teleportToLocation);
            await SendToKnownPlayers(teleportToLocation);
            WorldObjectPosition().SetXYZ(x, y, z);
        }
    }
}