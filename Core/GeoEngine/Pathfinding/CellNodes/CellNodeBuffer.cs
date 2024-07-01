using System;
using System.Collections.Generic;
using System.Threading;

namespace Core.GeoEngine.Pathfinding.CellNodes;

public class CellNodeBuffer
{
    private const int MAX_ITERATIONS = 3500;
    private const float LOW_WEIGHT = 0.5f;
    private const float DIAGONAL_WEIGHT = 0.707f;
    private const float MEDIUM_WEIGHT = 2;
    private const float HIGH_WEIGHT = 3;

    private readonly object _lock = new object();
    private readonly int _mapSize;
    private readonly CellNode[,] _buffer;

    private int _baseX = 0;
    private int _baseY = 0;

    private int _targetX = 0;
    private int _targetY = 0;
    private int _targetZ = 0;

    private long _timeStamp = 0;
    private long _lastElapsedTime = 0;

    private CellNode _current = null;

    private GeoEngineInit _geoEngineInit;

    public CellNodeBuffer(GeoEngineInit geoEngineInit, int size)
    {
        _mapSize = size;
        _geoEngineInit = geoEngineInit;
        _buffer = new CellNode[_mapSize, _mapSize];
    }

    public bool TryLock()
    {
        return Monitor.TryEnter(_lock);
    }

    public void Unlock()
    {
        Monitor.Exit(_lock);
    }

    public CellNode FindPath(int x, int y, int z, int tx, int ty, int tz)
    {
        _timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        _baseX = x + ((tx - x - _mapSize) / 2);
        _baseY = y + ((ty - y - _mapSize) / 2);
        _targetX = tx;
        _targetY = ty;
        _targetZ = tz;
        _current = GetNode(x, y, z);
        _current.SetCost(GetCost(x, y, z, HIGH_WEIGHT));

        for (int count = 0; count < MAX_ITERATIONS; count++)
        {
            if ((_current.GetLoc().GetNodeX() == _targetX) && (_current.GetLoc().GetNodeY() == _targetY) && (Math.Abs(_current.GetLoc().GetZ() - _targetZ) < 64))
            {
                return _current;
            }

            GetNeighbors();
            if (_current.GetNext() == null)
            {
                return null;
            }

            _current = _current.GetNext();
        }
        return null;
    }
    
    public void Free()
    {
        _current = null;

        for (int i = 0; i < _mapSize; i++)
        {
            for (int j = 0; j < _mapSize; j++)
            {
                CellNode node = _buffer[i, j];
                if (node != null)
                {
                    node.Free();
                }
            }
        }

        Unlock();
        _lastElapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _timeStamp;
    }
    
    public List<CellNode> DebugPath()
    {
        List<CellNode> result = new List<CellNode>();
        for (CellNode n = _current; n.GetParent() != null; n = (CellNode)n.GetParent())
        {
            result.Add(n);
            n.SetCost(-n.GetCost());
        }

        for (int i = 0; i < _mapSize; i++)
        {
            for (int j = 0; j < _mapSize; j++)
            {
                CellNode n = _buffer[i, j];
                if (n == null || !n.IsInUse() || n.GetCost() <= 0)
                {
                    continue;
                }

                result.Add(n);
            }
        }
        return result;
    }
    
    private void GetNeighbors()
    {
        if (_current.GetLoc().CanGoNone())
        {
            return;
        }

        int x = _current.GetLoc().GetNodeX();
        int y = _current.GetLoc().GetNodeY();
        int z = _current.GetLoc().GetZ();

        CellNode nodeE = null;
        CellNode nodeS = null;
        CellNode nodeW = null;
        CellNode nodeN = null;

        // East
        if (_current.GetLoc().CanGoEast())
        {
            nodeE = AddNode(x + 1, y, z, false);
        }

        // South
        if (_current.GetLoc().CanGoSouth())
        {
            nodeS = AddNode(x, y + 1, z, false);
        }

        // West
        if (_current.GetLoc().CanGoWest())
        {
            nodeW = AddNode(x - 1, y, z, false);
        }

        // North
        if (_current.GetLoc().CanGoNorth())
        {
            nodeN = AddNode(x, y - 1, z, false);
        }

        if (!CellPathFinding.ADVANCED_DIAGONAL_STRATEGY)
        {
            return;
        }

        // SouthEast
        if (nodeE != null && nodeS != null && nodeE.GetLoc().CanGoSouth() && nodeS.GetLoc().CanGoEast())
        {
            AddNode(x + 1, y + 1, z, true);
        }

        // SouthWest
        if (nodeS != null && nodeW != null && nodeW.GetLoc().CanGoSouth() && nodeS.GetLoc().CanGoWest())
        {
            AddNode(x - 1, y + 1, z, true);
        }

        // NorthEast
        if (nodeN != null && nodeE != null && nodeE.GetLoc().CanGoNorth() && nodeN.GetLoc().CanGoEast())
        {
            AddNode(x + 1, y - 1, z, true);
        }

        // NorthWest
        if (nodeN != null && nodeW != null && nodeW.GetLoc().CanGoNorth() && nodeN.GetLoc().CanGoWest())
        {
            AddNode(x - 1, y - 1, z, true);
        }
    }
    
    private CellNode GetNode(int x, int y, int z)
    {
        int aX = x - _baseX;
        if (aX < 0 || aX >= _mapSize)
        {
            return null;
        }

        int aY = y - _baseY;
        if (aY < 0 || aY >= _mapSize)
        {
            return null;
        }

        CellNode result = _buffer[aX, aY];
        if (result == null)
        {
            result = new CellNode(new NodeLoc(_geoEngineInit, x, y, z));
            _buffer[aX, aY] = result;
        }
        else if (!result.IsInUse())
        {
            result.SetInUse();
            if (result.GetLoc() != null)
            {
                result.GetLoc().Set(x, y, z);
            }
            else
            {
                result.SetLoc(new NodeLoc(_geoEngineInit, x, y, z));
            }
        }

        return result;
    }
    
    private CellNode AddNode(int x, int y, int z, bool diagonal)
    {
        CellNode newNode = GetNode(x, y, z);
        if (newNode == null)
        {
            return null;
        }
        if (newNode.GetCost() >= 0)
        {
            return newNode;
        }

        int geoZ = newNode.GetLoc().GetZ();
        int stepZ = Math.Abs(geoZ - _current.GetLoc().GetZ());
        float weight = diagonal ? DIAGONAL_WEIGHT : LOW_WEIGHT;

        if (!newNode.GetLoc().CanGoAll() || stepZ > 16)
        {
            weight = HIGH_WEIGHT;
        }
        else if (IsHighWeight(x + 1, y, geoZ) || IsHighWeight(x - 1, y, geoZ) || IsHighWeight(x, y + 1, geoZ) || IsHighWeight(x, y - 1, geoZ))
        {
            weight = MEDIUM_WEIGHT;
        }

        newNode.SetParent(_current);
        newNode.SetCost(GetCost(x, y, geoZ, weight));

        CellNode node = _current;
        int count = 0;
        while (node.GetNext() != null && count < MAX_ITERATIONS * 4)
        {
            count++;
            if (node.GetNext().GetCost() > newNode.GetCost())
            {
                newNode.SetNext(node.GetNext());
                break;
            }
            node = node.GetNext();
        }
        if (count == MAX_ITERATIONS * 4)
        {
            Console.Error.WriteLine($"Pathfinding: too long loop detected, cost: {newNode.GetCost()}");
        }

        node.SetNext(newNode);
        return newNode;
    }
    
    private bool IsHighWeight(int x, int y, int z)
    {
        CellNode result = GetNode(x, y, z);
        return result == null || !result.GetLoc().CanGoAll() || Math.Abs(result.GetLoc().GetZ() - z) > 16;
    }
    
    private double GetCost(int x, int y, int z, float weight)
    {
        int dX = x - _targetX;
        int dY = y - _targetY;
        int dZ = z - _targetZ;
        double result = Math.Sqrt((dX * dX) + (dY * dY) + ((dZ * dZ) / 256.0));
        if (result > weight)
        {
            result += weight;
        }

        return result > float.MaxValue ? float.MaxValue : result;
    }

    public long GetElapsedTime()
    {
        return _lastElapsedTime;
    }
}
