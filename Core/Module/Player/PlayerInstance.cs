﻿using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData.Template;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.Module.Player
{
    public sealed class PlayerInstance
    {
        private readonly PlayerAppearance _playerAppearance;
        private readonly PlayerCharacterInfo _playerCharacterInfo;
        private readonly PlayerModel _playerModel;
        private readonly ITemplateHandler _templateHandler;
        private static PlayerLoader _playerLoader;
        private readonly PlayerMoveToLocation _toLocation;
        public GameServiceController Controller { get; set; }
        public PlayerInstance(ITemplateHandler template, PlayerAppearance playerAppearance)
        {
            _templateHandler = template;
            _playerModel = new PlayerModel(this);
            _playerCharacterInfo = new PlayerCharacterInfo(this);
            _playerAppearance = playerAppearance;
            _toLocation = new PlayerMoveToLocation(this);
        }

        public ITemplateHandler TemplateHandler() => _templateHandler;
        public PlayerAppearance PlayerAppearance() => _playerAppearance;
        public PlayerModel PlayerModel() => _playerModel;
        public PlayerCharacterInfo PlayerCharacterInfo() => _playerCharacterInfo;
        
        public static PlayerLoader PlayerLoader()
        {
            return _playerLoader ??= new PlayerLoader();
        }

        public static Task<PlayerInstance> Load(int objectId)
        {
            return PlayerLoader().Load(objectId);
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
        
        public PlayerMoveToLocation Location()
        {
            return _toLocation;
        }
        
    }
}