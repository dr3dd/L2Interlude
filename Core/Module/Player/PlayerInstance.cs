using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
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
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerDesire _playerDesire;
        private readonly PlayerStatus _playerStatus;
        private readonly PlayerCombat _playerCombat;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerUseItem _playerUseItem;
        private readonly PlayerSkill _playerSkill;
        private readonly PlayerEffect _playerEffect;
        private readonly PlayerSkillMagic _playerSkillMagic;
        private readonly PlayerMessage _playerMessage;
        private readonly PlayerZone _playerZone;
        private readonly PlayerTargetAction _playerTargetAction;
        private readonly PlayerKnownList _playerKnownList;
        private readonly PlayerAction _playerAction;
            
        public Location Location { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public int Heading { get; set; }
        public NpcInstance LastTalkedNpc { get; set; }
        public GameServiceController Controller { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly WorldInit _worldInit;
        public PlayerInstance(ITemplateHandler template, PlayerAppearance playerAppearance, IServiceProvider provider, IUnitOfWork unitOfWork)
        {
            ServiceProvider = provider;
            _templateHandler = template;
            _playerAppearance = playerAppearance;
            _unitOfWork = unitOfWork;
            _playerModel = new PlayerModel(this);
            _playerAction = new PlayerAction(this);
            _playerCharacterInfo = new PlayerCharacterInfo(this);
            _toLocation = new PlayerMoveToLocation(this);
            _playerMovement = new PlayerMovement(this);
            _playerDesire = new PlayerDesire(this);
            _playerStatus = new PlayerStatus(this);
            _playerCombat = new PlayerCombat(this);
            _playerInventory = new PlayerInventory(this);
            _playerUseItem = new PlayerUseItem(this);
            _playerSkill = new PlayerSkill(this);
            _playerEffect = new PlayerEffect(this);
            _playerSkillMagic = new PlayerSkillMagic(this);
            _playerMessage = new PlayerMessage(this);
            _playerZone = new PlayerZone(this);
            _playerTargetAction = new PlayerTargetAction(this);
            _playerKnownList = new PlayerKnownList(this);

            _worldInit = provider.GetRequiredService<WorldInit>();
        }

        public IUnitOfWork GetUnitOfWork() => _unitOfWork;

        public ITemplateHandler TemplateHandler() => _templateHandler;
        public PlayerAppearance PlayerAppearance() => _playerAppearance;
        public PlayerModel PlayerModel() => _playerModel;
        public PlayerCharacterInfo PlayerCharacterInfo() => _playerCharacterInfo;
        public PlayerMovement PlayerMovement() => _playerMovement;
        public PlayerDesire PlayerDesire() => _playerDesire;
        public PlayerStatus PlayerStatus() => _playerStatus;
        public PlayerCombat PlayerCombat() => _playerCombat;
        public PlayerInventory PlayerInventory() => _playerInventory;
        public PlayerUseItem PlayerUseItem() => _playerUseItem;
        public PlayerSkill PlayerSkill() => _playerSkill;
        //public PlayerEffect PlayerEffect() => _playerEffect;
        public PlayerSkillMagic PlayerSkillMagic() => _playerSkillMagic;
        public PlayerMessage PlayerMessage() => _playerMessage;
        public PlayerZone PlayerZone() => _playerZone;
        internal PlayerTargetAction PlayerTargetAction() => _playerTargetAction;
        public PlayerKnownList PlayerKnownList() => _playerKnownList;
        public PlayerAction PlayerAction() => _playerAction;

        private static PlayerLoader PlayerLoader(IServiceProvider serviceProvider)
        {
            return _playerLoader ??= new PlayerLoader(serviceProvider);
        }

        public static Task<PlayerInstance> Load(int objectId, IServiceProvider serviceProvider)
        {
            return PlayerLoader(serviceProvider).Load(objectId);
        }
        
        public Task SendPacketAsync(ServerPacket serverPacket)
        {
            if (Controller is null)
                return Task.CompletedTask;
            return Controller.SendPacketAsync(serverPacket);
        }
        
        public async Task SendActionFailedPacketAsync()
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
        
        public async Task UpdateKnownObjects()
        {
            await FindClosePlayers();
            await FindCloseNpc();
        }

        public async Task RemoveKnownObjects()
        {
            foreach (var (objectId, worldObject) in _playerKnownList.GetKnownObjects())
            {
                if (!CalculateRange.CheckIfInRange(2000, worldObject.GetX(), worldObject.GetY(),
                        worldObject.GetZ(), 20,
                        GetX(), GetY(), GetZ(), 20, true))
                {
                    switch (worldObject)
                    {
                        case PlayerInstance playerInstance:
                            playerInstance.PlayerKnownList().RemoveKnownObject(this);
                            await playerInstance.SendPacketAsync(new DeleteObject(ObjectId));
                            break;
                        case NpcInstance npcInstance:
                        {
                            npcInstance.NpcKnownList().RemoveKnownObject(this);
                            if (npcInstance.NpcKnownList().GetKnownPlayers().IsEmpty)
                            {
                                var npcServerRequest = new NpcServerRequest
                                {
                                    EventName = EventName.NoDesire,
                                    NpcName = npcInstance.GetTemplate().GetStat().Name,
                                    NpcType = npcInstance.GetTemplate().GetStat().Type,
                                    PlayerObjectId = ObjectId,
                                    NpcObjectId = npcInstance.ObjectId
                                };
                                await SendObjectToNpcServerAsync(npcServerRequest);
                            }
                            break;
                        }
                    }

                    if (_playerKnownList.GetKnownObjects() != null)
                    {
                        _playerKnownList.RemoveKnownObject(worldObject);
                        await SendPacketAsync(new DeleteObject(objectId));
                    }
                }
            }
        }

        private async Task FindCloseNpc()
        {
            foreach (NpcInstance npcInstance in _worldInit.GetVisibleNpc(this))
            {
                if (!CalculateRange.CheckIfInRange(2000, npcInstance.GetX(), npcInstance.GetY(),
                        npcInstance.GetZ(), 20,
                        GetX(), GetY(), GetZ(), 20, true))
                {
                    continue;
                }
                if (PlayerKnownList().HasObjectInKnownList(npcInstance.ObjectId))
                {
                    continue;
                }
                var npcAi = npcInstance.GetTemplate().GetStat().NpcAi;
                npcAi.TryGetValue("MoveAroundSocial", out var moveAroundSocial);
                npcAi.TryGetValue("MoveAroundSocial1", out var moveAroundSocial1);
                npcAi.TryGetValue("MoveAroundSocial2", out var moveAroundSocial2);
                npcAi.TryGetValue("fnHi", out var fnHi);
                npcAi.TryGetValue("fnNobless", out var fnNobless);
                npcAi.TryGetValue("fnNoNobless", out var fnNoNobless);
                npcAi.TryGetValue("fnNoNoblessItem", out var fnNoNoblessItem);
                npcAi.TryGetValue("fnYouAreChaotic", out var fnYouAreChaotic);

                var npcServerRequest = new NpcServerRequest
                {
                    EventName = EventName.Created,
                    NpcName = npcInstance.GetTemplate().GetStat().Name,
                    NpcType = npcInstance.GetTemplate().GetStat().Type,
                    PlayerObjectId = ObjectId,
                    NpcObjectId = npcInstance.ObjectId
                };
                if (moveAroundSocial != null)
                {
                    npcServerRequest.MoveAroundSocial = Convert.ToInt16(moveAroundSocial);
                }
                if (moveAroundSocial1 != null)
                {
                    npcServerRequest.MoveAroundSocial1 = Convert.ToInt16(moveAroundSocial1);
                }
                if (moveAroundSocial2 != null)
                {
                    npcServerRequest.MoveAroundSocial2 = Convert.ToInt16(moveAroundSocial2);
                }
                if (fnHi != null)
                {
                    npcServerRequest.FnHi = fnHi;
                }
                if (fnNobless != null)
                {
                    npcServerRequest.FnNobless = fnNobless;
                }
                if (fnNoNobless != null)
                {
                    npcServerRequest.FnNoNobless = fnNoNobless;
                }
                if (fnNoNoblessItem != null)
                {
                    npcServerRequest.FnNoNoblessItem = fnNoNoblessItem;
                }
                if (fnYouAreChaotic != null)
                {
                    npcServerRequest.FnYouAreChaotic = fnYouAreChaotic;
                }
                await SendObjectToNpcServerAsync(npcServerRequest);
            }
        }

        public async Task SendObjectToNpcServerAsync(NpcServerRequest npcServerRequest)
        {
            await ServiceProvider.GetRequiredService<NpcServiceController>()
                    .SendMessageToNpcService(npcServerRequest);
        }

        private async Task FindClosePlayers()
        {
            foreach (PlayerInstance targetInstance in _worldInit.GetVisiblePlayers(this))
            {
                if (!CalculateRange.CheckIfInRange(2000, targetInstance.GetX(), targetInstance.GetY(),
                        targetInstance.GetZ(), 20,
                        GetX(), GetY(), GetZ(), 20, true))
                {
                    continue;
                }
                if (PlayerKnownList().HasObjectInKnownList(targetInstance.ObjectId))
                {
                    continue;
                }
                PlayerKnownList().AddToKnownList(targetInstance.ObjectId, targetInstance);
                await SendPacketAsync(new CharInfo(targetInstance));
                await targetInstance.SendPacketAsync(new CharInfo(this));
                targetInstance.PlayerKnownList().AddToKnownList(ObjectId, this);
            }
        }

        public async Task SendToKnownPlayers(ServerPacket packet)
        {
            foreach (var (objectId, worldObject) in PlayerKnownList().GetKnownObjects())
            {
                if (worldObject is PlayerInstance targetInstance)
                {
                    await targetInstance.SendPacketAsync(packet);
                    await SendPacketAsync(new CharInfo(targetInstance));
                }
            }
        }

        public async Task OnActionAsync(WorldObject worldObject)
        {
            await _playerTargetAction.OnTargetAsync(worldObject);
        }

        public async Task DeleteMeAsync()
        {
            if (GetWorldRegion() != null)
            {
                GetWorldRegion().RemoveFromZones(this);
            }
            await PlayerTargetAction().RemoveTargetAsync();
            //_worldInit.RemoveObject(this);
            _worldInit.RemoveFromAllPlayers(this);
            _worldInit.RemoveVisibleObject(this, WorldObjectPosition().GetWorldRegion());
            WorldObjectPosition().SetWorldRegion(null);
            
            PlayerKnownList().RemoveMeFromKnownObjects();
            PlayerKnownList().RemoveAllKnownObjects();
        }

        public override int GetMaxHp()
        {
            return _playerStatus.GetMaxHp();
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
    }
}