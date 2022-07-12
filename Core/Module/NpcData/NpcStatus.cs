namespace Core.Module.NpcData
{
    public class NpcStatus
    {
        private readonly NpcInstance _npcInstance;
        
        public NpcStatus(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }

        public int GetMaxHp()
        {
            return (int)_npcInstance.GetTemplate().GetStat().OrgHp;
        }
        public double GetHpRegenRate()
        {
            return 1.5f;
        }
    }
}