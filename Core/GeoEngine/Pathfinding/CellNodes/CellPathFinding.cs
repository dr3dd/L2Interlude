using System;
using System.Collections.Generic;
using Core.Module.ItemData;
using L2Logger;

namespace Core.GeoEngine.Pathfinding.CellNodes;

public class CellPathFinding : PathFindingAbstract
{
    private BufferInfo[] _allBuffers;
    private int _findSuccess = 0;
    private int _findFails = 0;
    private int _postFilterUses = 0;
    private int _postFilterPlayableUses = 0;
    private int _postFilterPasses = 0;
    private long _postFilterElapsed = 0;
	
    private List<ItemInstance> _debugItems = null;
    private const string PATHFIND_BUFFERS = "100x6;128x6;192x6;256x4;320x4;384x4;500x2";
    public const bool ADVANCED_DIAGONAL_STRATEGY = false;
    private const bool DEBUG_PATH = false;
    private const int MAX_POSTFILTER_PASSES = 3;

    public CellPathFinding(GeoEngineInit geoEngineInit)
    {
	    GeoEngineInit = geoEngineInit;
        try
        {
            var array = PATHFIND_BUFFERS.Split(";");
            _allBuffers = new BufferInfo[array.Length];
			
            string buf;
            string[] args;
            for (var i = 0; i < array.Length; i++)
            {
                buf = array[i];
                args = buf.Split("x");
                if (args.Length != 2)
                {
                    throw new Exception("Invalid buffer definition: " + buf);
                }
                _allBuffers[i] = new BufferInfo(int.Parse(args[0]), int.Parse(args[1]));
            }
        }
        catch (Exception e)
        {
            LoggerManager.Warn("CellPathFinding: Problem during buffer init: " + e.Message);
            throw new Exception("CellPathFinding: load aborted");
        }
    }
    
    public override bool PathNodesExist(short regionOffset)
    {
        return false;
    }

    public override LinkedList<AbstractNodeLoc> FindPath(int x, int y, int z, int tx, int ty, int tz, int instanceId, bool playable)
    {
        var gx = GeoEngineInit.GetGeoX(x);
		var gy = GeoEngineInit.GetGeoY(y);
		if (!GeoEngineInit.HasGeo(x, y))
		{
			return null;
		}
		var gz = GeoEngineInit.GetHeight(x, y, z);
		var gtx = GeoEngineInit.GetGeoX(tx);
		var gty = GeoEngineInit.GetGeoY(ty);
		if (!GeoEngineInit.HasGeo(tx, ty))
		{
			return null;
		}
		var gtz = GeoEngineInit.GetHeight(tx, ty, tz);
		CellNodeBuffer buffer = Alloc(64 + (2 * Math.Max(Math.Abs(gx - gtx), Math.Abs(gy - gty))), playable);
		if (buffer == null)
		{
			return null;
		}
		
		var debug = DEBUG_PATH && playable;
		
		if (debug)
		{
			if (_debugItems == null)
			{
				//_debugItems = new LinkedList<>();
			}
			else
			{
				foreach (ItemInstance item in _debugItems)
				{
					//item.DecayMe();
				}
				_debugItems.Clear();
			}
		}
		
		LinkedList<AbstractNodeLoc> path = null;
		try
		{
			CellNode result = buffer.FindPath(gx, gy, gz, gtx, gty, gtz);
			
			if (debug)
			{
				foreach (CellNode n in buffer.DebugPath())
				{
					if (n.GetCost() < 0)
					{
						DropDebugItem(1831, (int) (-n.GetCost() * 10), n.GetLoc());
					}
					else
					{
						// Known nodes.
						DropDebugItem(57, (int) (n.GetCost() * 10), n.GetLoc());
					}
				}
			}
			
			if (result == null)
			{
				_findFails++;
				return null;
			}
			
			path = ConstructPath(result);
		}
		catch (Exception e)
		{
			//LOGGER.log(Level.WARNING, "", e);
			return null;
		}
		finally
		{
			buffer.Free();
		}
		
		if ((path.Count < 3) || (MAX_POSTFILTER_PASSES <= 0))
		{
			_findSuccess++;
			return path;
		}
		
		long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		_postFilterUses++;
		if (playable)
		{
			_postFilterPlayableUses++;
		}
		
		LinkedList<AbstractNodeLoc>.Enumerator middlePoint;
		int currentX;
		int currentY;
		int currentZ;
		int pass = 0;
		bool remove;
		do
		{
			pass++;
			_postFilterPasses++;

			remove = false;
			middlePoint = path.GetEnumerator();
			currentX = x;
			currentY = y;
			currentZ = z;

			AbstractNodeLoc locEnd = null;
			List<AbstractNodeLoc> nodesToRemove = new List<AbstractNodeLoc>();
			while (middlePoint.MoveNext())
			{
				AbstractNodeLoc locMiddle = middlePoint.Current;

				if (!middlePoint.MoveNext())
				{
					break;
				}

				locEnd = middlePoint.Current;
				if (GeoEngineInit.CanMoveToTarget(currentX, currentY, currentZ, locEnd.GetX(), locEnd.GetY(), locEnd.GetZ(), instanceId))
				{
					nodesToRemove.Add(locMiddle); // Mark for removal
					remove = true;
					if (debug)
					{
						DropDebugItem(735, 1, locMiddle);
					}
				}
				else
				{
					currentX = locMiddle.GetX();
					currentY = locMiddle.GetY();
					currentZ = locMiddle.GetZ();
				}
			}

			// Remove the marked nodes after iteration
			foreach (var node in nodesToRemove)
			{
				path.Remove(node);
			}
		} while (remove);
		
		// Only one postfilter pass for AI.
		while (playable && remove && (path.Count > 2) && (pass < MAX_POSTFILTER_PASSES))
		{
			// Весь цикл обробки
		}

		if (debug)
		{
			foreach (var n in path)
			{
				DropDebugItem(1061, 1, n);
			}
		}

		_findSuccess++;
		_postFilterElapsed += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - timeStamp;
		return path;
    }
    
    private void DropDebugItem(int itemId, int num, AbstractNodeLoc loc)
    {
	    /*
	    ItemInstance item = new ItemInstance(IdManager.getInstance().getNextId(), itemId);
	    item.setCount(num);
	    item.spawnMe(loc.getX(), loc.getY(), loc.getZ());
	    _debugItems.add(item);
	    */
    }
    
    private LinkedList<AbstractNodeLoc> ConstructPath(AbstractNode<NodeLoc> node)
    {
        var path = new LinkedList<AbstractNodeLoc>();
        var previousDirectionX = int.MinValue;
        var previousDirectionY = int.MinValue;
        int directionX;
        int directionY;
		
        var tempNode = node;
        while (tempNode.GetParent() != null)
        {
            if (!ADVANCED_DIAGONAL_STRATEGY && (tempNode.GetParent().GetParent() != null))
            {
                int tmpX = tempNode.GetLoc().GetNodeX() - tempNode.GetParent().GetParent().GetLoc().GetNodeX();
                int tmpY = tempNode.GetLoc().GetNodeY() - tempNode.GetParent().GetParent().GetLoc().GetNodeY();
                if (Math.Abs(tmpX) == Math.Abs(tmpY))
                {
                    directionX = tmpX;
                    directionY = tmpY;
                }
                else
                {
                    directionX = tempNode.GetLoc().GetNodeX() - tempNode.GetParent().GetLoc().GetNodeX();
                    directionY = tempNode.GetLoc().GetNodeY() - tempNode.GetParent().GetLoc().GetNodeY();
                }
            }
            else
            {
                directionX = tempNode.GetLoc().GetNodeX() - tempNode.GetParent().GetLoc().GetNodeX();
                directionY = tempNode.GetLoc().GetNodeY() - tempNode.GetParent().GetLoc().GetNodeY();
            }
			
            // Only add a new route point if moving direction changes.
            if ((directionX != previousDirectionX) || (directionY != previousDirectionY))
            {
                previousDirectionX = directionX;
                previousDirectionY = directionY;

                path.AddFirst(tempNode.GetLoc());
                tempNode.SetLoc(null);
            }
            tempNode = tempNode.GetParent();
        }
        return path;
    }
    
    private CellNodeBuffer Alloc(int size, bool playable)
    {
        CellNodeBuffer current = null;
        foreach (var bufferInfo in _allBuffers)
        {
            if (bufferInfo.MapSize >= size)
            {
                foreach (var buf in bufferInfo.Buffers)
                {
                    if (buf.TryLock())
                    {
                        bufferInfo.Uses++;
                        if (playable)
                        {
                            bufferInfo.PlayableUses++;
                        }
                        bufferInfo.Elapsed += buf.GetElapsedTime();
                        current = buf;
                        break;
                    }
                }

                if (current != null)
                {
                    break;
                }

                // Not found, allocate temporary buffer.
                current = new CellNodeBuffer(GeoEngineInit, bufferInfo.MapSize);
                current.TryLock();
                if (bufferInfo.Buffers.Count < bufferInfo.Count)
                {
                    bufferInfo.Buffers.Add(current);
                    bufferInfo.Uses++;
                    if (playable)
                    {
                        bufferInfo.PlayableUses++;
                    }
                    break;
                }

                bufferInfo.Overflows++;
                if (playable)
                {
                    bufferInfo.PlayableOverflows++;
                }
            }
        }

        return current;
    }
}
