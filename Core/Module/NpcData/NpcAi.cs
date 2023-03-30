using NpcAi.Ai;
using NpcAi.Handlers;
using NpcAi.Model;

namespace Core.Module.NpcData
{
    public class NpcAi
    {
        private readonly NpcInstance _npcInstance;
        private readonly DefaultNpc _defaultNpc;
        
        public NpcAi(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            var npcName = _npcInstance.GetStat().Name;
            var npcType = _npcInstance.GetStat().Type;
            _defaultNpc = NpcHandler.GetNpcHandler(npcName, npcType);
            var npcAiData = _npcInstance.GetStat().NpcAiData;
            NpcAiDefault.SetDefaultAiParams(_defaultNpc, npcAiData);
        }

        public void Created()
        {
            _defaultNpc.MySelf = new NpcCreature
            {
                Level = _npcInstance.GetStat().Level,
                Race = 1,
                NpcObjectId = _npcInstance.ObjectId
            };
            _defaultNpc.Created();
        }

        public void NoDesire()
        {
            _defaultNpc.NoDesire();
        }

        public void Attacker()
        {
            throw new System.NotImplementedException();
        }

        public void Talked()
        {
            throw new System.NotImplementedException();
        }
    }
}