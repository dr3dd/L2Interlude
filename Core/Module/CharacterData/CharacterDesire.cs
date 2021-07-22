using System.Threading.Tasks;
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

        
        
        private void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(GetType().Name + ": " + obj.Exception);
            }
        }

        
    }
}