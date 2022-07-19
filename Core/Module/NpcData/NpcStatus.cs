namespace Core.Module.NpcData
{
    public class NpcStatus
    {
        private readonly NpcInstance _npcInstance;
        
        public NpcStatus(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }

        private NpcTemplateInit GetTemplate() => _npcInstance.GetTemplate();
        private NpcStat GetStat() => GetTemplate().GetStat();
        

        public int GetMaxHp()
        {
            return (int)GetStat().OrgHp;
        }
        public double GetHpRegenRate()
        {
            return GetStat().OrgHpRegen;
        }
    }
}