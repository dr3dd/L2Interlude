using System.Threading.Tasks;
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
        
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x15);
		
            await WriteStringAsync(_playerAppearance.CharacterName);
            await WriteIntAsync(_playerInstance.ObjectId); // ObjectId
            await WriteStringAsync("");
            await WriteIntAsync(_sessionId);
            await WriteIntAsync(0);
            await WriteIntAsync(0x00); // ??
            await WriteIntAsync(_playerAppearance.Gender);
            await WriteIntAsync(_template.GetRaceId());
            await WriteIntAsync(_template.GetClassId());
            await WriteIntAsync(0x01); // active ??
            await WriteIntAsync(_playerInstance.GetX());
            await WriteIntAsync(_playerInstance.GetY());
            await WriteIntAsync(_playerInstance.GetZ());
		
            await WriteIntAsync(_playerInstance.CharacterStatus().CurrentHp);//_player.getCurrentHp()
            await WriteIntAsync(_playerInstance.CharacterStatus().CurrentMp);//_player.getCurrentMp()
            await WriteIntAsync(_characterInfo.Sp);//_player.getSp()
            await WriteLongAsync(_characterInfo.Exp);//_player.getExp()
            await WriteIntAsync(_level);//_player.getLevel()
            await WriteIntAsync(0); // _player.getKarma()
            await WriteIntAsync(0x0); // ?
            await WriteIntAsync(_template.GetInt());//_player.getINT()
            await WriteIntAsync(_template.GetStr());//_player.getSTR()
            await WriteIntAsync(_template.GetCon());//_player.getCON()
            await WriteIntAsync(_template.GetMen());//_player.getMEN()
            await WriteIntAsync(_template.GetDex());//_player.getDEX()
            await WriteIntAsync(_template.GetWit());//_player.getWIT()
            for (int i = 0; i < 30; i++)
            {
                await WriteIntAsync(0x00);
            }
            await WriteIntAsync(0x00); // c3 work
            await WriteIntAsync(0x00); // c3 work
		
            // extra info
            await WriteIntAsync(_gameTimeController.GetGameTime());//WriteIntAsync(GameTimeController.getInstance().getGameTime()); // in-game time
            
		
            await WriteIntAsync(0x00); //
		
            await WriteIntAsync(0x00); // c3
		
            await WriteIntAsync(0x00); // c3 InspectorBin
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
		
            await WriteIntAsync(0x00); // c3 InspectorBin for 528 client
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
            await WriteIntAsync(0x00); // c3
        }
    }
}