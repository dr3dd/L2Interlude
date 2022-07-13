using System.Threading.Tasks;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData
{
    public class CharacterTargetAction
    {
        private WorldObject _currentTarget;
        private readonly Character _character;

        public CharacterTargetAction(Character character)
        {
            _character = character;
        }

        public WorldObject GetTarget()
        {
            return _currentTarget;
        }

        public void SetTarget(WorldObject character)
        {
            _currentTarget = character;
        }
        
        public async Task CancelTargetAsync(int unselect)
        {
            if (unselect == 0)
            {
                await RemoveTargetAsync();
            }
            else if (GetTarget() != null)
            {
                await RemoveTargetAsync();
            }
        }
        
        public async Task RemoveTargetAsync()
        {
            await _character.SendPacketAsync(new TargetUnselected(_character));
            _currentTarget = null;
        }
    }
}