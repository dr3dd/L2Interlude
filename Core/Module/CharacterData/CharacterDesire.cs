using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.Module.WorldData;
using L2Logger;

namespace Core.Module.CharacterData
{
    public class CharacterDesire : AbstractDesire
    {
        public CharacterDesire(Character character) : base(character)
        {
        }
        
        protected override async Task MoveToDesireAsync(Location destination)
        {
            ChangeDesire(Desire.MoveToDesire);
           // await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
        }

        protected override Task CastDesireAsync(SkillDataModel arg0)
        {
            throw new System.NotImplementedException();
        }

        protected override Task IntentionInteractAsync(WorldObject worldObject)
        {
            throw new System.NotImplementedException();
        }


        private void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(GetType().Name + ": " + obj.Exception);
            }
        }

        
    }
}