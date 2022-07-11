using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.Module.WorldData;

namespace Core.Module.NpcData
{
    public class NpcDesire : CharacterDesire
    {
        private readonly NpcInstance _npcInstance;
        
        public NpcDesire(NpcInstance npcInstance) : base(npcInstance)
        {
            _npcInstance = npcInstance;
        }

        protected override Task CastDesireAsync(SkillDataModel arg0)
        {
            throw new System.NotImplementedException();
        }

        protected override Task IntentionInteractAsync(WorldObject worldObject)
        {
            throw new System.NotImplementedException();
        }

        protected override Task DesireAttackAsync(Character target)
        {
            throw new System.NotImplementedException();
        }
    }
}