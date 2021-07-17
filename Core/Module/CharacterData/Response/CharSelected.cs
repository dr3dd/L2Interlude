using Core.Controller;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.Module.CharacterData.Response
{
    public class CharSelected : ServerPacket
    {
        private readonly PlayerInstance _playerInstance;
        private readonly int _sessionId;
        private readonly GameTimeController _gameTimeController;

        public CharSelected(PlayerInstance playerInstance, int sessionId)
        {
            _playerInstance = playerInstance;
            _sessionId = sessionId;
            _gameTimeController = Initializer.ServiceProvider.GetService<GameTimeController>();
        }
        
        public override void Write()
        {
            WriteByte(0x15);
		
            WriteString(_playerInstance.PlayerAppearance().CharacterName);
            WriteInt(_playerInstance.CharacterId); // ??
            WriteString("");
            WriteInt(_sessionId);
            WriteInt(0);
            WriteInt(0x00); // ??
            WriteInt(_playerInstance.PlayerAppearance().Gender);
            WriteInt(_playerInstance.TemplateHandler().GetRaceId());
            WriteInt(_playerInstance.TemplateHandler().GetClassId());
            WriteInt(0x01); // active ??
            WriteInt(-71338);
            WriteInt(258271);
            WriteInt(-3104);
		
            WriteInt(100);//_player.getCurrentHp()
            WriteInt(100);//_player.getCurrentMp()
            WriteInt(1);//_player.getSp()
            WriteLong(1);//_player.getExp()
            WriteInt(1);//_player.getLevel()
            WriteInt(0); // _player.getKarma()
            WriteInt(0x0); // ?
            WriteInt(_playerInstance.TemplateHandler().GetInt());//_player.getINT()
            WriteInt(_playerInstance.TemplateHandler().GetStr());//_player.getSTR()
            WriteInt(_playerInstance.TemplateHandler().GetCon());//_player.getCON()
            WriteInt(_playerInstance.TemplateHandler().GetMen());//_player.getMEN()
            WriteInt(_playerInstance.TemplateHandler().GetDex());//_player.getDEX()
            WriteInt(_playerInstance.TemplateHandler().GetWit());//_player.getWIT()
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