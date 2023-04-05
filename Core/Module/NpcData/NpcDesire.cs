using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;

namespace Core.Module.NpcData
{
    public class NpcDesire : CharacterDesire
    {
        private readonly NpcInstance _npcInstance;
        private int _currentDesirePriority;
        private object _currentDesireAction;
        
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

        public async Task AddEffectActionDesire(int actionId, int timeDesire, int desire)
        {
            if (_currentDesirePriority > desire)
                return;
            _currentDesirePriority = desire;
            
            await _npcInstance.SendToKnownPlayers(new SocialAction(_npcInstance.ObjectId, actionId));
            //start no desire when social action finished 
            TaskManagerScheduler.Schedule(() =>
            {
                _currentDesirePriority = 0;
                _npcInstance.NpcAi().NoDesire();
            }, timeDesire);
        }

        public async Task AddMoveAroundDesire(int timeDesire, int desire)
        {
            if (_currentDesirePriority > desire)
                return;
            _currentDesirePriority = desire;
            
            var x1 = (_npcInstance.SpawnX + Rnd.Next(300 * 2)) - 300;
            var y1 = (_npcInstance.SpawnY + Rnd.Next(300 * 2)) - 300;
            var z1 = _npcInstance.SpawnZ;

            if (_npcInstance.CharacterMovement().IsMoving)
            {
                return;
            }

            await _npcInstance.NpcDesire().MoveToAsync(x1, y1, z1);
            
            //start no desire when moving finished 
            TaskManagerScheduler.Schedule(() =>
            {
                _currentDesirePriority = 0;
                _npcInstance.NpcAi().NoDesire();
            }, timeDesire);
        }
    }
}