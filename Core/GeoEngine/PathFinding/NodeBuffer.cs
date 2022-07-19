using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Core.Module.CharacterData;
using Helpers;

namespace Core.GeoEngine.PathFinding
{
    public class NodeBuffer
    {
        private int _bufferIndex;
        private Node[] _buffer;
        private GeoEngineInit _engineInit;
        private PriorityQueueMy<Node> _opened;
        private List<Node> _closed;
        private Node _current;
        private object _lock = new object();
        // Target coordinates.
        private int _gtx;
        private int _gty;
        private int _gtz;
        // Pathfinding statistics.
        private long _timeStamp;
        private long _lastElapsedTime;

        public NodeBuffer(int size)
        {
            _engineInit = Initializer.GeoEngineInit();
            _buffer = new Node[size];
            _opened = new PriorityQueueMy<Node>();
            _closed = new List<Node>();
            
            // Create Nodes.
            for (int i = 0; i < size; i++)
            {
                _buffer[i] = new Node(0, 0, 0);
            }
        }
        
        public ICollection<Location> FindPath(int gox, int goy, int goz, int gtx, int gty, int gtz)
        {
            // Set start timestamp.
            _timeStamp = DateTimeHelper.CurrentUnixTimeMillis();
		
            // Set target coordinates.
            _gtx = gtx;
            _gty = gty;
            _gtz = gtz;
		
            // Get node from buffer.
            _current = _buffer[_bufferIndex++];
		
            // Set node geodata coordinates and movement cost.
            _current.SetGeo(gox, goy, goz, _engineInit.GetNsweNearest(gox, goy, goz));
            _current.SetCost(null, 0, GetCostH(gox, goy, goz));
		
            int count = 0;
            do
            {
                // Move node to closed list.
                _closed.Add(_current);
			
                // Target reached, calculate path and return.
                if ((_current.GetGeoX() == _gtx) && (_current.GetGeoY() == _gty) && (_current.GetZ() == _gtz))
                {
                    return ConstructPath();
                }
			
                // Expand current node.
                Expand();
			
                // Get next node to expand.
                _current = _opened.Poll();
            }
            while ((_current != null) && (_bufferIndex < _buffer.Length) && (++count < 3500));
		
            // Iteration failed, return empty path.
            return new Collection<Location>();
        }
        
        public void Free()
        {
            _opened.Clear();
            _closed.Clear();
		
            for (int i = 0; i < (_bufferIndex - 1); i++)
            {
                _buffer[i].Clean();
            }
            _bufferIndex = 0;
		
            _current = null;
		
            _lastElapsedTime = DateTimeHelper.CurrentUnixTimeMillis() - _timeStamp;
            Monitor.Exit(_lock);
        }
        
        private ICollection<Location> ConstructPath()
        {
            // Create result.
            LinkedList<Location> path = new LinkedList<Location>();
		
            // Clear X/Y direction.
            int dx = 0;
            int dy = 0;
		
            // Get parent node.
            Node parent = _current.GetParent();
		
            // While parent exists.
            while (parent != null)
            {
                // Get parent node to current node X/Y direction.
                int nx = parent.GetGeoX() - _current.GetGeoX();
                int ny = parent.GetGeoY() - _current.GetGeoY();
			
                // Direction has changed?
                if ((dx != nx) || (dy != ny))
                {
                    // Add current node to the beginning of the path (Node must be cloned, as NodeBuffer reuses them).
                    path.AddFirst(_current.Clone());
				
                    // Update X/Y direction.
                    dx = nx;
                    dy = ny;
                }
			
                // Move current node and update its parent.
                _current = parent;
                parent = _current.GetParent();
            }
		
            return path;
        }
        
        public bool IsLocked()
        {
            return Monitor.TryEnter(_lock);
        }
        
        private void Expand()
        {
            // Movement is blocked, skip.
            sbyte nswe = _current.GetNSWE();
            if (nswe == GeoStructure.CellFlagNone)
            {
                return;
            }
		
            // Get geo coordinates of the node to be expanded.
            // Note: Z coord shifted up to avoid dual-layer issues.
            int x = _current.GetGeoX();
            int y = _current.GetGeoY();
            int z = _current.GetZ() + GeoStructure.CellIgnoreHeight;
		
            sbyte nsweN = GeoStructure.CellFlagNone;
            sbyte nsweS = GeoStructure.CellFlagNone;
            sbyte nsweW = GeoStructure.CellFlagNone;
            sbyte nsweE = GeoStructure.CellFlagNone;
		
            // Can move north, expand.
            if ((nswe & GeoStructure.CellFlagN) != 0)
            {
                nsweN = AddNode(x, y - 1, z, 10);
            }
		
            // Can move south, expand.
            if ((nswe & GeoStructure.CellFlagS) != 0)
            {
                nsweS = AddNode(x, y + 1, z, 10);
            }
		
            // Can move west, expand.
            if ((nswe & GeoStructure.CellFlagW) != 0)
            {
                nsweW = AddNode(x - 1, y, z, 10);
            }
		
            // Can move east, expand.
            if ((nswe & GeoStructure.CellFlagE) != 0)
            {
                nsweE = AddNode(x + 1, y, z, 10);
            }
		
            // Can move north-west, expand.
            if (((nsweW & GeoStructure.CellFlagN) != 0) && ((nsweN & GeoStructure.CellFlagW) != 0))
            {
                AddNode(x - 1, y - 1, z, 14);
            }
		
            // Can move north-east, expand.
            if (((nsweE & GeoStructure.CellFlagN) != 0) && ((nsweN & GeoStructure.CellFlagE) != 0))
            {
                AddNode(x + 1, y - 1, z, 14);
            }
		
            // Can move south-west, expand.
            if (((nsweW & GeoStructure.CellFlagS) != 0) && ((nsweS & GeoStructure.CellFlagW) != 0))
            {
                AddNode(x - 1, y + 1, z, 14);
            }
		
            // Can move south-east, expand.
            if (((nsweE & GeoStructure.CellFlagS) != 0) && ((nsweS & GeoStructure.CellFlagE) != 0))
            {
                AddNode(x + 1, y + 1, z, 14);
            }
        }
        
        private sbyte AddNode(int gx, int gy, int gzValue, int weight)
        {
            // Check new node is out of geodata grid (world coordinates).
            if ((gx < 0) || (gx >= GeoStructure.GeoCellsX) || (gy < 0) || (gy >= GeoStructure.GeoCellsY))
            {
                return GeoStructure.CellFlagNone;
            }
		
            // Check buffer has reached capacity.
            if (_bufferIndex >= _buffer.Length)
            {
                return GeoStructure.CellFlagNone;
            }
		
            // Get geodata block and check if there is a layer at given coordinates.
            ABlock block = _engineInit.GetBlock(gx, gy);
            int index = block.GetIndexBelow(gx, gy, gzValue);
            if (index < 0)
            {
                return GeoStructure.CellFlagNone;
            }
		
            // Get node geodata Z and nswe.
            int gz = block.GetHeight(index);
            sbyte nswe = block.GetNswe(index);
		
            // Get node from current index (don't move index yet).
            Node node = _buffer[_bufferIndex];
		
            // Set node geodata coordinates.
            node.SetGeo(gx, gy, gz, nswe);
		
            // Node is already added to opened list, return.
            if (_opened.Contains(node))
            {
                return nswe;
            }
		
            // Node was already expanded, return.
            if (_closed.Contains(node))
            {
                return nswe;
            }
		
            // The node is to be used. Set node movement cost and add it to opened list. Move the buffer index.
            node.SetCost(_current, nswe != GeoStructure.CellFlagAll ? 30 : weight, GetCostH(gx, gy, gz));
            _opened.Add(node);
            _bufferIndex++;
            return nswe;
        }
        
        private int GetCostH(int gx, int gy, int gz)
        {
            // Get differences to the target.
            int dx = Math.Abs(gx - _gtx);
            int dy = Math.Abs(gy - _gty);
            int dz = Math.Abs(gz - _gtz) / GeoStructure.CellHeight;
		
            // Get diagonal and axial differences to the target.
            int dd = Math.Min(dx, dy);
            int da = Math.Max(dx, dy) - dd;
		
            // Calculate the diagonal distance of the node to the target.
            return (dd * 18) + ((da + dz) * 12);
        }
        
        public long GetElapsedTime() => _lastElapsedTime;
    }
}