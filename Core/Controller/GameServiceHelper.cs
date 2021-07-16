using System.Collections.Generic;
using Core.Module.Player;
using DataBase.Entities;

namespace Core.Controller
{
    public class GameServiceHelper
    {
        private GameServiceController _gameServiceController;
        private List<CharacterEntity> _charSlotMapping;

        public GameServiceHelper(GameServiceController gameServiceController)
        {
            _gameServiceController = gameServiceController;
        }
        
        public void SetCharSelection(List<CharacterEntity> list)
        {
            _charSlotMapping = list;
        }

        public CharacterEntity GetCharacterBySlot(int charSlot)
        {
            return _charSlotMapping[charSlot];
        }
        
        public PlayerInstance CurrentPlayer { get; set; }
    }
}