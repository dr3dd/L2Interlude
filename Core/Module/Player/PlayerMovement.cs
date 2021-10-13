using System;
using Core.Controller;
using Core.Module.CharacterData;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerMovement
    {
        private readonly PlayerInstance _playerInstance;
        private readonly GameTimeController _timeController;
        private MoveData _move;
        public bool IsMoving => _move != null;
        private bool _cursorKeyMovement = false;
        public PlayerMovement(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _timeController = playerInstance.ServiceProvider.GetRequiredService<GameTimeController>();
        }
        
        
        public void MoveToLocation(int x, int y, int z, int offset)
        {
            // Get the Move Speed of the Creature
            int speed = _playerInstance.PlayerCombat().GetGroundHighSpeed();
            
            // Get current position of the Creature
            int curX = _playerInstance.Location.GetX();
            int curY = _playerInstance.Location.GetY();
            int curZ = _playerInstance.Location.GetZ();

            LoggerManager.Info($"curSpeed: {speed} curX: {curX} curY: {curY} curZ: {curZ} distX: {x} distY: {y} distZ: {z}");
            
            double dx = (x - curX);
            double dy = (y - curY);
            double dz = (z - curZ);
            double distance = Utility.Hypot(dx,dy);
            
            double cos;
            double sin;
            
            // Calculate movement angles needed
            sin = dy / distance;
            cos = dx / distance;
            
            // Create and Init a MoveData object
            // GEODATA MOVEMENT CHECKS AND PATHFINDING
            // Initialize not on geodata path
            MoveData m = new MoveData {OnGeodataPathIndex = -1, DisregardingGeodata = false};
            
            double originalDistance = distance;
            int originalX = x;
            int originalY = y;
            int originalZ = z;

            int ticksToMove = 1 + (int) ((_timeController.TicksPerSecond * distance) / speed);
            m.XDestination = x;
            m.YDestination = y;
            m.ZDestination = z; // this is what was requested from client
            // Calculate and set the heading of the Creature
            m.Heading = 0; // initial value for coordinate sync
            
            // Does not break heading on vertical movements
            _playerInstance.Heading = CalculateRange.CalculateHeadingFrom(cos, sin);
            
            m.MoveStartTime = _timeController.GetGameTicks();
            
            // Set the Creature _move object to MoveData object
            _move = m;
            
            _timeController.RegisterMovingObject(_playerInstance);
        }
        
        public bool UpdatePosition(int gameTicks)
        {
            MoveData m = _move;
            if (m == null)
            {
                return true;
            }
            
            // Check if the position has already be calculated
            if (m.MoveTimestamp == 0)
            {
                m.MoveTimestamp = m.MoveStartTime;
                m.XAccurate = _playerInstance.Location.GetX();
                m.YAccurate = _playerInstance.Location.GetY();
            }
            // Check if the position has already be calculated
            if (m.MoveTimestamp == gameTicks)
            {
                return false;
            }
            
            int xPrev = _playerInstance.Location.GetX();
            int yPrev = _playerInstance.Location.GetY();
            int zPrev = _playerInstance.Location.GetZ(); // the z coordinate may be modified by coordinate synchronizations
            double dx;
            double dy;
            double dz;
            double distFraction;
            
            dx = m.XDestination - m.XAccurate;
            dy = m.YDestination - m.YAccurate;
            dz = m.ZDestination - zPrev;

            int speed = 125;

            double distance = Utility.Hypot(dx, dy);
            if (_cursorKeyMovement // In case of cursor movement, avoid moving through obstacles.
                || (distance > 3000)
            ) // Stop movement when player has clicked far away and intersected with an obstacle.
            {
                double angle = Utility.ConvertHeadingToDegree(_playerInstance.Heading);
                double radian = angle.ToRadians();
                double course = Utility.ToRadians(180);
                double frontDistance = 10 * (125 / 100);
                int x1 = (int) (Math.Cos(Math.PI + radian + course) * frontDistance);
                int y1 = (int) (Math.Sin(Math.PI + radian + course) * frontDistance);
                int x = xPrev + x1;
                int y = yPrev + y1;
            }

            // Prevent player moving on ledges.
            if ((dz > 180) && (distance < 300))
            {
                _move.OnGeodataPathIndex = -1;
                //StopMove(getActingPlayer().getLastServerPosition());
                return false;
            }
            
            int distPassed = (speed * (gameTicks - m.MoveTimestamp)) / _timeController.TicksPerSecond;
            if ((((dx * dx) + (dy * dy)) < 10000) && ((dz * dz) > 2500)) // close enough, allows error between client and server geodata if it cannot be avoided
            {
                distFraction = distPassed / Math.Sqrt((dx * dx) + (dy * dy));
            }
            else
            {
                distFraction = distPassed / Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
            }
            
            if (distFraction > 1)
            {
                _playerInstance.Location.SetXYZ(m.XDestination, m.YDestination, m.ZDestination);
                // Set the position of the Creature to the destination
                //_character.SetXYZ(m.XDestination, m.YDestination, m.ZDestination);
            }
            else
            {
                m.XAccurate += dx * distFraction;
                m.YAccurate += dy * distFraction;
                
                // Set the position of the Creature to estimated after parcial move
                //_character.SetXYZ((int) m.XAccurate, (int) m.YAccurate, zPrev + (int) ((dz * distFraction) + 0.5));
                _playerInstance.Location.SetXYZ((int) m.XAccurate, (int) m.YAccurate, zPrev + (int) ((dz * distFraction) + 0.5));
            }
            // Set the timer of last position update to now
            m.MoveTimestamp = gameTicks;
		
            return distFraction > 1;
        }
        
        public int GetXDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.XDestination;
            }
            return _playerInstance.Location.GetX();
        }
        
        public int GetYDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.YDestination;
            }
            return _playerInstance.Location.GetY();
        }
        
        public int GetZDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.ZDestination;
            }
            return _playerInstance.Location.GetZ();
        }
    }
}