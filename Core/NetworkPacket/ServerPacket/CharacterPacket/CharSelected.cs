using Core.Controller;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    public class CharSelected : Network.ServerPacket
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerAppearance _playerAppearance;
        private readonly int _level;
        private readonly ITemplateHandler _template;
        private readonly PlayerCharacterInfo _characterInfo;
        private readonly Location _location;
        private readonly int _sessionId;
        private readonly GameTimeController _gameTimeController;

        public CharSelected(PlayerInstance playerInstance, int sessionId)
        {
            _playerInstance = playerInstance;
            
            _characterInfo = _playerInstance.PlayerCharacterInfo();
            _level = _playerInstance.PlayerStatus().Level;
            _template = _playerInstance.TemplateHandler();
            _location = _playerInstance.Location;
            _playerAppearance = _playerInstance.PlayerAppearance();
            _sessionId = sessionId;
            _gameTimeController = Initializer.TimeController();
        }
        
        public override void Write()
        {
            WriteByte(0x15);
		
            WriteString(_playerAppearance.CharacterName);
            WriteInt(_playerInstance.ObjectId); // ObjectId
            WriteString("");
            WriteInt(_sessionId);
            WriteInt(0);
            WriteInt(0x00); // ??
            WriteInt(_playerAppearance.Gender);
            WriteInt(_template.GetRaceId());
            WriteInt(_template.GetClassId());
            WriteInt(0x01); // active ??
            WriteInt(_playerInstance.GetX());
            WriteInt(_playerInstance.GetY());
            WriteInt(_playerInstance.GetZ());
		
            WriteInt(_playerInstance.PlayerStatus().CurrentHp);//_player.getCurrentHp()
            WriteInt(_playerInstance.PlayerStatus().CurrentMp);//_player.getCurrentMp()
            WriteInt(_characterInfo.Sp);//_player.getSp()
            WriteLong(_characterInfo.Exp);//_player.getExp()
            WriteInt(_level);//_player.getLevel()
            WriteInt(0); // _player.getKarma()
            WriteInt(0x0); // ?
            WriteInt(_template.GetInt());//_player.getINT()
            WriteInt(_template.GetStr());//_player.getSTR()
            WriteInt(_template.GetCon());//_player.getCON()
            WriteInt(_template.GetMen());//_player.getMEN()
            WriteInt(_template.GetDex());//_player.getDEX()
            WriteInt(_template.GetWit());//_player.getWIT()
            for (int i = 0; i < 30; i++)
            {
                WriteInt(0x00);
            }
            WriteInt(0x00); // c3 work
            WriteInt(0x00); // c3 work
		
            // extra info
            WriteInt(_gameTimeController.GetGameTime());//WriteInt(GameTimeController.getInstance().getGameTime()); // in-game time
            
		
            WriteInt(0x00); //
		
            WriteInt(0x00); // c3
		
            WriteInt(0x00); // c3 InspectorBin
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
		
            WriteInt(0x00); // c3 InspectorBin for 528 client
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
            WriteInt(0x00); // c3
        }
    }
}