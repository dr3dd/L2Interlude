using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.NetworkPacket.ServerPacket;
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
        public Location Location { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public int Heading { get; set; }
        public GameServiceController Controller { get; set; }
        public PlayerInstance(ITemplateHandler template, PlayerAppearance playerAppearance, IServiceProvider provider)
        {
            ServiceProvider = provider;
            _templateHandler = template;
            _playerAppearance = playerAppearance;
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
        }

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
    }
}