using Core.GeoEngine.Pathfinding.GeoNodes;

namespace Core.GeoEngine.Utils;

public class BinaryNodeHeap
{
    private readonly GeoNode[] _list;
    private int _size;

    public BinaryNodeHeap(int size)
    {
        _list = new GeoNode[size + 1];
        _size = 0;
    }

    public void Add(GeoNode n)
    {
        _size++;
        int pos = _size;
        _list[pos] = n;
        while (pos != 1)
        {
            int p2 = pos / 2;
            if (_list[pos].GetCost() <= _list[p2].GetCost())
            {
                var temp = _list[p2];
                _list[p2] = _list[pos];
                _list[pos] = temp;
                pos = p2;
            }
            else
            {
                break;
            }
        }
    }

    public GeoNode RemoveFirst()
    {
        var first = _list[1];
        _list[1] = _list[_size];
        _list[_size] = null;
        _size--;
        int pos = 1;
        int cpos;
        int dblcpos;
        GeoNode temp;
        while (true)
        {
            cpos = pos;
            dblcpos = cpos * 2;
            if (dblcpos + 1 <= _size)
            {
                if (_list[cpos].GetCost() >= _list[dblcpos].GetCost())
                {
                    pos = dblcpos;
                }
                if (_list[pos].GetCost() >= _list[dblcpos + 1].GetCost())
                {
                    pos = dblcpos + 1;
                }
            }
            else if (dblcpos <= _size)
            {
                if (_list[cpos].GetCost() >= _list[dblcpos].GetCost())
                {
                    pos = dblcpos;
                }
            }

            if (cpos != pos)
            {
                temp = _list[cpos];
                _list[cpos] = _list[pos];
                _list[pos] = temp;
            }
            else
            {
                break;
            }
        }
        return first;
    }

    public bool Contains(GeoNode n)
    {
        if (_size == 0)
        {
            return false;
        }
        for (int i = 1; i <= _size; i++)
        {
            if (_list[i].Equals(n))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }
}
