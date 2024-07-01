using System.Collections.Generic;
using System.Text;

namespace Core.GeoEngine.Pathfinding.CellNodes;

public class BufferInfo
{
    public int MapSize { get; }
    public int Count { get; }
    public List<CellNodeBuffer> Buffers { get; }
    public int Uses { get; set; } = 0;
    public int PlayableUses { get; set; } = 0;
    public int Overflows { get; set; } = 0;
    public int PlayableOverflows { get; set; } = 0;
    public long Elapsed { get; set; } = 0;

    public BufferInfo(int size, int count)
    {
        MapSize = size;
        Count = count;
        Buffers = new List<CellNodeBuffer>(count);
    }

    public override string ToString()
    {
        var stat = new StringBuilder(100);
        stat.AppendFormat("{0}x{0} num:{1}/{2} uses:{3}/{4}", MapSize, Buffers.Count, Count, Uses, PlayableUses);
        if (Uses > 0)
        {
            stat.AppendFormat(" total/avg(ms):{0}/{1:0.00}", Elapsed, (double)Elapsed / Uses);
        }
        stat.AppendFormat(" ovf:{0}/{1}", Overflows, PlayableOverflows);

        return stat.ToString();
    }
}