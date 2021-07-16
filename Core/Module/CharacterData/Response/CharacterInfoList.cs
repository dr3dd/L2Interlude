using Network;

namespace Core.Module.CharacterData.Response
{
    public class CharacterInfoList : ServerPacket
    {
        private readonly string _accountName;
        private readonly int _sessionId;
        private readonly CharacterList _characterList;

        public CharacterInfoList(string accountName, int sessionId)
        {
            _sessionId = sessionId;
            _accountName = accountName;
            _characterList = new CharacterList();
        }
        
        public override void Write()
        {
            var list = _characterList.GetCharacterList(_accountName);
            WriteByte(0x13);
            WriteInt(list.Count);
            list.ForEach(entity =>
            {
                WriteString(entity.CharacterName);
                WriteInt(entity.CharacterId);
                WriteString(_accountName);
                WriteInt(_sessionId);
                WriteInt(0x00); //ClanId
                WriteInt(0x00);
                WriteInt(entity.Gender);
                WriteInt(entity.Race);
                WriteInt(entity.ClassId);
                WriteInt(0x01); // active ??
                WriteInt(0x00); // x
                WriteInt(0x00); // y
                WriteInt(0x00); // z
                WriteDouble(entity.Hp); // hp cur
                WriteDouble(entity.Mp); // mp cur
                WriteInt(entity.Sp);
                WriteLong(entity.Exp);
                WriteInt(entity.Level);
                WriteInt(entity.Pk); // karma
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                
                for (byte id = 0; id < 17; id++)
                    WriteInt(0);

                for (byte id = 0; id < 17; id++)
                    WriteInt(0);
                
                WriteInt(entity.HairStyle);
                WriteInt(entity.HairColor);
                WriteInt(entity.Face);
                
                WriteDouble(entity.MaxHp); // hp max
                WriteDouble(entity.MaxMp); // mp max
                WriteInt(0); // days left before
                WriteInt(entity.ClassId);
                WriteInt(0x01);
                WriteByte(0);
                WriteInt(0);
            });
        }
    }
}