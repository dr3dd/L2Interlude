namespace NpcService.Model
{
    public struct DesireObject : IPrioritizable
    {
        public double Priority { get; set; }
        public NpcDesire NpcDesire { get; }
        public ActionDesire ActionDesire { get; }
        
        public DesireObject(double priority, ActionDesire actionDesire, NpcDesire npcDesire)
        {
            Priority = priority;
            ActionDesire = actionDesire;
            NpcDesire = npcDesire;
        }
    }
}