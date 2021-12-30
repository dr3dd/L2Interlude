namespace Core.Module.Player
{
    public class PlayerAction
    {
        private readonly PlayerInstance _playerInstance;

        public bool IsSitting { get; set; }
        private bool _isTeleporting;

        public PlayerAction(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }


        public void SetTeleporting(bool teleporting)
        {
            _isTeleporting = teleporting;
        }

        public bool IsTeleporting()
        {
            return _isTeleporting;
        }
            
    }
}