using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Core.TaskManager;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.CharacterData
{
    public class CharacterMovement
    {
        private readonly GameTimeController _timeController;
        private MoveData _move;
        private readonly Character _character;
        private bool _cursorKeyMovement = false;
        public bool IsMoving => _move != null;
        private readonly WorldInit _worldInit;
        private readonly CharacterMovementStatus _characterMovementStatus;
        public CharacterMovementStatus CharacterMovementStatus() => _characterMovementStatus;
        public Character Character() => _character;

        public CharacterMovement(Character character)
        {
            _character = character;
            _timeController = character.ServiceProvider.GetRequiredService<GameTimeController>();
            _worldInit = _character.ServiceProvider.GetRequiredService<WorldInit>();
            _characterMovementStatus = new CharacterMovementStatus(this);
        }
        
        public void MoveToLocation(int x, int y, int z, int offset)
        {
            // Get the Move Speed of the Creature
            var speed = _character.CharacterCombat().GetCharacterSpeed();
            
            // Get current position of the Creature
            int curX = _character.GetX();
            int curY = _character.GetY();
            int curZ = _character.GetZ();

            //LoggerManager.Info($"curSpeed: {speed} curX: {curX} curY: {curY} curZ: {curZ} distX: {x} distY: {y} distZ: {z}");
            
            double dx = (x - curX);
            double dy = (y - curY);
            double dz = (z - curZ);
            double distance = Utility.Hypot(dx,dy);
            
            double cos;
            double sin;

            bool verticalMovementOnly = false;
            
            // Check if a movement offset is defined or no distance to go through
            if ((offset > 0) || (distance < 1))
            {
                // approximation for moving closer when z coordinates are different
                // TODO: handle Z axis movement better
                offset -= (int)Math.Abs(dz);
                if (offset < 5)
                {
                    offset = 5;
                }
			
                // If no distance to go through, the movement is canceled
                if ((distance < 1) || ((distance - offset) <= 0))
                {
                    // Notify the AI that the Creature is arrived at destination
                    _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtArrived);
                    return;
                }
			
                // Calculate movement angles needed
                sin = dy / distance;
                cos = dx / distance;
                distance -= (offset - 5); // due to rounding error, we have to move a bit closer to be in range
			
                // Calculate the new destination with offset included
                x = curX + (int) (distance * cos);
                y = curY + (int) (distance * sin);
            }
            else
            {
                // Calculate movement angles needed
                sin = dy / distance;
                cos = dx / distance;
            }
            
            // Create and Init a MoveData object
            // GEODATA MOVEMENT CHECKS AND PATHFINDING
            // Initialize not on geodata path
            MoveData m = new MoveData {OnGeodataPathIndex = -1, DisregardingGeodata = false};
            
            double originalDistance = distance;
            int originalX = x;
            int originalY = y;
            int originalZ = z;
            
            int gtx = (originalX - _worldInit.MapMinX) >> 4;
            int gty = (originalY - _worldInit.MapMinY) >> 4;
            
            // Pathfinding checks.
            if (((originalDistance - distance) > 30))
            {
                m.OnGeodataPathIndex = 0; // on first segment
                m.GeoPathGtx = gtx;
                m.GeoPathGty = gty;
                m.GeoPathAccurateTx = originalX;
                m.GeoPathAccurateTy = originalY;
                x = m.GeoPath[m.OnGeodataPathIndex].GetX();
                y = m.GeoPath[m.OnGeodataPathIndex].GetY();
                z = m.GeoPath[m.OnGeodataPathIndex].GetZ();
                dx = x - curX;
                dy = y - curY;
                dz = z - curZ;
                distance = verticalMovementOnly ? Math.Pow(dz, 2) : Utility.Hypot(dx, dy);
                sin = dy / distance;
                cos = dx / distance;
            }

            int ticksToMove = 1 + (int) ((_timeController.TicksPerSecond * distance) / speed);
            m.XDestination = x;
            m.YDestination = y;
            m.ZDestination = z; // this is what was requested from client
            // Calculate and set the heading of the Creature
            m.Heading = 0; // initial value for coordinate sync
            
            // Does not break heading on vertical movements
            _character.Heading = CalculateRange.CalculateHeadingFrom(cos, sin);
            
            m.MoveStartTime = _timeController.GetGameTicks();
            
            // Set the Creature _move object to MoveData object
            _move = m;
            
            _timeController.RegisterMovingObject(_character);
            
            
            // Create a task to notify the AI that Creature arrives at a check point of the movement
            if ((ticksToMove * _timeController.MillisInTick) > 3000)
            {
                TaskManagerScheduler.Schedule(() => 
                {
                    _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtArrivedRevalidate);
                }, 2000);
            }
        }
        
        public async Task<bool> UpdatePosition(int gameTicks)
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
                m.XAccurate = _character.GetX();
                m.YAccurate = _character.GetY();
            }
            // Check if the position has already be calculated
            if (m.MoveTimestamp == gameTicks)
            {
                return false;
            }
            
            int xPrev = _character.GetX();
            int yPrev = _character.GetY();
            int zPrev = _character.GetZ(); // the z coordinate may be modified by coordinate synchronizations
            double dx;
            double dy;
            double dz;
            double distFraction;
            
            dx = m.XDestination - m.XAccurate;
            dy = m.YDestination - m.YAccurate;
            dz = m.ZDestination - zPrev;

            var speed = _character.CharacterCombat().GetCharacterSpeed();

            var distPassed = (speed * (gameTicks - m.MoveTimestamp)) / _timeController.TicksPerSecond;
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
                // Set the position of the Creature to the destination
                _character.SetXYZ(m.XDestination, m.YDestination, m.ZDestination);
            }
            else
            {
                m.XAccurate += dx * distFraction;
                m.YAccurate += dy * distFraction;
                
                // Set the position of the Creature to estimated after parcial move
                _character.SetXYZ((int) m.XAccurate, (int) m.YAccurate, zPrev + (int) ((dz * distFraction) + 0.5));
            }
            
            //LoggerManager.Info($"curX: {_character.GetX()} curY: {_character.GetY()} curZ: {_character.GetZ()} gameTicks: {gameTicks} speed: {speed}");
            
            // Set the timer of last position update to now
            m.MoveTimestamp = gameTicks;
            _character.CharacterZone().RevalidateZone();
            //await _character.SendToKnownPlayers(new CharMoveToLocation(_character));
		
            return await Task.FromResult(distFraction > 1);
        }
        
        public async Task<bool> MoveToNextRoutePoint()
        {
            if (!IsOnGeoDataPath())
            {
                // Cancel the move action
                _move = null;
                return false;
            }
            
            // Get the Move Speed of the Creature
            var speed = _character.CharacterCombat().GetCharacterSpeed();
            if ((speed <= 0))
            {
                // Cancel the move action
                _move = null;
                await Task.FromResult(false);
            }
            MoveData md = _move;
            if (md == null)
            {
                await Task.FromResult(false);
            }
            
            // Create and Init a MoveData object
            MoveData m = new MoveData();
		
            // Update MoveData object
            m.OnGeodataPathIndex = md.OnGeodataPathIndex + 1; // next segment
            m.GeoPath = md.GeoPath;
            m.GeoPathGtx = md.GeoPathGtx;
            m.GeoPathGty = md.GeoPathGty;
            m.GeoPathAccurateTx = md.GeoPathAccurateTx;
            m.GeoPathAccurateTy = md.GeoPathAccurateTy;
            
            if (md.OnGeodataPathIndex == (md.GeoPath.Count - 2))
            {
                m.XDestination = md.GeoPathAccurateTx;
                m.YDestination = md.GeoPathAccurateTy;
                m.ZDestination = md.GeoPath[m.OnGeodataPathIndex].GetZ();
            }
            else
            {
                m.XDestination = md.GeoPath[m.OnGeodataPathIndex].GetX();
                m.YDestination = md.GeoPath[m.OnGeodataPathIndex].GetY();
                m.ZDestination = md.GeoPath[m.OnGeodataPathIndex].GetZ();
            }
            double distance = Utility.Hypot(m.XDestination - _character.GetX(), m.YDestination - _character.GetY());
            // Calculate and set the heading of the Creature
            if (distance != 0)
            {
                _character.Heading = CalculateRange.CalculateHeadingFrom(_character.GetX(), _character.GetY(), m.XDestination, m.YDestination);
            }
            
            // Calculate the number of ticks between the current position and the destination
            // One tick added for rounding reasons
            int ticksToMove = 1 + (int) ((_timeController.TicksPerSecond * distance) / speed);
            m.Heading = 0; // initial value for coordinate sync
            m.MoveStartTime = _timeController.GetGameTicks();
            // Set the Creature _move object to MoveData object
            _move = m;
            _timeController.RegisterMovingObject(_character);
            
            // Create a task to notify the AI that Creature arrives at a check point of the movement
            if ((ticksToMove * _timeController.TicksPerSecond) > 3000)
            {
                TaskManagerScheduler.Schedule(() =>
                {
                    _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtArrivedRevalidate);
                }, 2000);
            }
            
            // the CtrlEvent.EVT_ARRIVED will be sent when the character will actually arrive to destination by GameTimeController
		
            // Send a Server->Client packet CharMoveToLocation to the actor and all PlayerInstance in its _knownPlayers
            await _character.SendToKnownPlayers(new CharMoveToLocation(_character));
            return await Task.FromResult(true);
        }
        
        public int GetXDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.XDestination;
            }
            return _character.GetX();
        }
        
        public int GetYDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.YDestination;
            }
            return _character.GetY();
        }
        
        public int GetZDestination()
        {
            MoveData m = _move;
            if (m != null)
            {
                return m.ZDestination;
            }
            return _character.GetZ();
        }

        public async Task StopMoveAsync(Location pos)
        {
            // Delete movement data of the Creature
            _move = null;
            _characterMovementStatus.SetStand();
            _character.WorldObjectPosition().SetXYZ(pos.GetX(), pos.GetY(), pos.GetZ());
            _character.Heading = pos.GetHeading();
			
            if (_character is PlayerInstance player)
            {
                _character.CharacterZone().RevalidateZone();
            }
            var stopMovePacket = new StopMove(_character);
            await _character.SendToKnownPlayers(stopMovePacket);
        }

        /// <summary>
        /// TODO Geo Engine
        /// </summary>
        /// <returns></returns>
        public bool IsOnGeoDataPath()
        {
            MoveData m = _move;
            if (m == null)
            {
                return false;
            }
            /*
             * if (m.onGeodataPathIndex == -1)
		        {
			        return false;
		        }
		        if (m.onGeodataPathIndex >= (m.geoPath.size() - 1))
		        {
			        return false;
		        }
             */
            return false;
        }
    }
}