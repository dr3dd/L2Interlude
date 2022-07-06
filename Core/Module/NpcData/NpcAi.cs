using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;

namespace Core.Module.NpcData
{
    public class NpcDesire : AbstractDesire
    {
        private readonly NpcInstance _npcInstance;
        
        public NpcDesire(NpcInstance npcInstance) : base(npcInstance)
        {
            _npcInstance = npcInstance;
        }

        protected override async Task MoveToDesireAsync(Location destination)
        {
            // Move the actor to Location (x,y,z) server side AND client side by sending Server->Client packet CharMoveToLocation (broadcast)
            ChangeDesire(Desire.MoveToDesire);
            await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
        }
        
        private async Task MoveToAsync(int x, int y, int z)
        {
            // Set AI movement data
            _npcInstance.CharacterMovement().MoveToLocation(x, y, z, 0);
            await _npcInstance.SendToKnownPlayers(new CharMoveToLocation(_npcInstance));
        }
        
        private void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(GetType().Name + ": " + obj.Exception);
            }
        }

        protected override Task CastDesireAsync(SkillDataModel arg0)
        {
            throw new System.NotImplementedException();
        }

        protected override Task IntentionInteractAsync(WorldObject worldObject)
        {
            throw new System.NotImplementedException();
        }

        protected override Task IntentionAttackAsync(Character target)
        {
            throw new System.NotImplementedException();
        }
    }
}