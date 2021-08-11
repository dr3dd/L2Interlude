using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Controller
{
    public class GameServiceHelper
    {
        private GameServiceController _gameServiceController;
        private List<CharacterListModel> _charSlotMapping;

        public GameServiceHelper(GameServiceController gameServiceController)
        {
            _gameServiceController = gameServiceController;
        }
        
        public void SetCharSelection(List<CharacterListModel> list)
        {
            _charSlotMapping = list;
        }

        public CharacterListModel GetCharacterBySlot(int charSlot)
        {
            return _charSlotMapping[charSlot];
        }
        
        public PlayerInstance CurrentPlayer { get; set; }
    }
}