namespace Core.Module.Player.Validators
{
    public class GeneralPlayerMoveToLocationValidator : IPlayerMoveToLocationValidator
    {
        private readonly PlayerAction _playerAction;
        
        public GeneralPlayerMoveToLocationValidator(PlayerInstance playerInstance)
        {
            _playerAction = playerInstance.PlayerAction();
        }
        
        public bool IsValid()
        {
            if (IsSitting()) return false;
            if (IsTeleporting()) return false;
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
    }
}