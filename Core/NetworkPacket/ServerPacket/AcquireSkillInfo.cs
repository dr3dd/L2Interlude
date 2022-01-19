using System.Collections.Generic;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class AcquireSkillInfo : Network.ServerPacket
    {
        private readonly List<Req> _reqs;
        private readonly int _id;
        private readonly int _level;
        private readonly int _spCost;
        private readonly int _mode;
        
        private class Req
        {
            public int ItemId { get; }
            public int Count { get; }
            public int Type { get; }
            public int Unk { get; }
		
            public Req(int pType, int pItemId, int pCount, int pUnk)
            {
                ItemId = pItemId;
                Type = pType;
                Count = pCount;
                Unk = pUnk;
            }
        }

        public AcquireSkillInfo(int id, int level, int spCost, int mode)
        {
            _reqs = new List<Req>();
            _id = id;
            _level = level;
            _spCost = spCost;
            _mode = mode;
        }
        
        public void AddRequirement(int type, int id, int count, int unk)
        {
            _reqs.Add(new Req(type, id, count, unk));
        }
        
        public override void Write()
        {
            WriteByte(0x8b);
            WriteInt(_id);
            WriteInt(_level);
            WriteInt(_spCost);
            WriteInt(_mode); // c4
		
            WriteInt(_reqs.Count);
            _reqs.ForEach(t =>
            {
                WriteInt(t.Type);
                WriteInt(t.ItemId);
                WriteInt(t.Count);
                WriteInt(t.Unk);
            });
        }
    }
}