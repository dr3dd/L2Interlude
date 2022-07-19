using System;
using System.Threading.Tasks;
using Core.GeoEngine;
using Core.Module.AreaData;
using Core.Module.CharacterData;
using Core.Module.Player.Validators;
using Core.NetworkPacket.ServerPacket;
using L2Logger;

namespace Core.Module.Player
{
    public sealed class PlayerMoveToLocation
    {
        private readonly PlayerInstance _playerInstance;
        private readonly GeoEngineInit _geoEngine;
        private readonly IPlayerMoveToLocationValidator _validator;
        public PlayerMoveToLocation(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _validator = new GeneralPlayerMoveToLocationValidator(playerInstance);
            //_geoEngine = playerInstance.ServiceProvider.GetRequiredService<GeoEngineInit>();
        }
        
        public async Task MoveToLocationAsync(int targetX, int targetY, int targetZ, int originX, int originY, int originZ)
        {
            if (!_validator.IsValid())
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }
            
            if ((targetX == originX) && (targetY == originY) && (targetZ == originZ))
            {
                await _playerInstance.SendPacketAsync(new StopMove(_playerInstance));
                await _playerInstance.SendActionFailedPacketAsync();
            }

            double dx = targetX - _playerInstance.GetX();
            double dy = targetY - _playerInstance.GetY();

            if (((dx * dx) + (dy * dy)) > 98010000)
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }

            //var loc = _geoEngine.GetValidLocation(originX, originY, originZ, targetX, targetY, targetZ);
            try
            {
                //TODO
                //var d = Initializer.GeoEngineInit().FindPath(originX, originY, originZ, targetX, targetY, targetZ);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            
            _playerInstance.CharacterDesire().AddDesire(Desire.MoveToDesire, new Location(targetX, targetY, targetZ));
        }
        

        public async Task ValidatePositionAsync(int x, int y, int z, int heading)
        {
            if (!_validator.IsValid())
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }
            
            int realX = _playerInstance.GetX();
            int realY = _playerInstance.GetY();
            int realZ = _playerInstance.GetZ();
            
            LoggerManager.Info($"Validate Location: X: {realX}, Y: {realY} Z: {realZ}");

            if ((x == 0) && (y == 0) && (realX != 0))
            {
                return;
            }

            int dx = x - realX;
            int dy = y - realY;
            int dz = z - realZ;
            double diffSq = ((dx * dx) + (dy * dy));
            
            if (_playerInstance.PlayerZone().IsInsideZone(AreaId.Water))
            {
                _playerInstance.SetXYZ(realX, realY, z);
                if (diffSq > 90000)
                {
                    await _playerInstance.SendPacketAsync(new ValidateLocation(_playerInstance));
                }
            }
            else if (diffSq < 360000) // if too large, messes observation
            {
                _playerInstance.SetXYZ(realX, realY, z);
                return;
            }
        }
    }
}