using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using DataBase.Interfaces;
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
            
        public Location Location { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public int Heading { get; set; }
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
        public PlayerEffect PlayerEffect() => _playerEffect;
        public PlayerSkillMagic PlayerSkillMagic() => _playerSkillMagic;
        public PlayerMessage PlayerMessage() => _playerMessage;
        public PlayerZone PlayerZone() => _playerZone;
        internal PlayerTargetAction PlayerTargetAction() => _playerTargetAction;

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
            await Controller.SendPacketAsync(new UserInfo(this));
            //Broadcast.toKnownPlayers(this, new CharInfo(this)); TODO
        }
        
        public PlayerMoveToLocation PlayerLocation()
        {
            return _toLocation;
        }
        
        public void UpdateKnownObjects()
        {
            FindClosePlayers();
        }

        private void FindClosePlayers()
        {
            foreach (PlayerInstance targetInstance in _worldInit.GetVisiblePlayers(this))
            {
                SendPacketAsync(new CharInfo(targetInstance));
                targetInstance.SendPacketAsync(new CharInfo(this));
            }
        }

        public async Task SendToKnownPlayers(ServerPacket packet)
        {
            foreach (PlayerInstance targetInstance in _worldInit.GetVisiblePlayers(this))
            {
                await targetInstance.SendPacketAsync(packet);
                await SendPacketAsync(new CharInfo(targetInstance));
            }
        }

        public async Task OnActionAsync(WorldObject worldObject)
        {
            if (worldObject is PlayerInstance targetInstance)
            {
                await _playerTargetAction.OnTargetAsync(targetInstance);
            }
        }
    }
}