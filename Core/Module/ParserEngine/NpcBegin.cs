using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public struct NpcBegin
    {
        public string Name { get; set; }
        public IDictionary<string, int> Pos { get; set; }
        public IList<IDictionary<string, int>> PosAny { get; set; }
        public byte Total { get; set; }

        public NpcBegin(string name, IDictionary<string, int> pos, byte total,
            IList<IDictionary<string, int>> tmpLocationAny)
        {
            Name = name;
            Pos = pos;
            Total = total;
            PosAny = tmpLocationAny;
        }
    }
}