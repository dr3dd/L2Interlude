using Core.Module.CharacterData;

namespace Core.Module.Player.Validators
{
    public class GeneralPlayerMoveToLocationValidator : IPlayerMoveToLocationValidator
    {
        private readonly PlayerAction _playerAction;
        private readonly CharacterDesireCast _desireCast;
        
        public GeneralPlayerMoveToLocationValidator(PlayerInstance playerInstance)
        {
            _playerAction = playerInstance.PlayerAction();
            _desireCast = playerInstance.CharacterDesire().DesireCast();
        }
        
        public bool IsValid()
        {
            if (IsSitting()) return false;
            if (IsTeleporting()) return false;
            if (IsCastingNow()) return false;
            return true;
        }

        private bool IsSitting()
        {
            return _playerAction.IsSitting;
        }

        private bool IsTeleporting()
        {
            return _playerAction.IsTeleporting();
        }

        private bool IsCastingNow()
        {
            return _desireCast.IsCastingNow();
        }
    }
}