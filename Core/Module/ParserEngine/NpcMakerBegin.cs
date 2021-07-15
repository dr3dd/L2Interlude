using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public struct NpcMakerBegin
    {
        public string Name { get; }
        public IList<NpcBegin> NpcBegins { get; }

        public NpcMakerBegin(string name, IList<NpcBegin> npcBegins)
        {
            Name = name;
            NpcBegins = npcBegins;
        }
    }
}