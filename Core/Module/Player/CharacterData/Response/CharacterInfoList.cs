using Network;

namespace Core.Module.Player.CharacterData.Response
{
    public class CharacterInfoList : ServerPacket
    {
        private readonly string _accountName;
        private readonly int _sessionId;

        public CharacterInfoList(string accountName, int sessionId)
        {
            _sessionId = sessionId;
            _accountName = accountName;
        }
        
        public override void Write()
        {
            WriteByte(0x13);
            /*
            var res = CharacterPackages;
            int size = res.Length;
            WriteByte(0x13);
            WriteInt(size);

            long lastAccess = 0;
            if (_activeId == -1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (lastAccess < res[i].LastAccess)
                    {
                        lastAccess = res[i].LastAccess;
                        _activeId = i;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                CharSelectInfoPackage charInfoPackage = res[i];
                
                WriteString(charInfoPackage.Name);
                WriteInt(charInfoPackage.CharacterId);
                WriteString(_accountName);
                WriteInt(_sessionId);
                WriteInt(charInfoPackage.ClanId);
                WriteInt(0x00);

                WriteInt(charInfoPackage.Sex);
                WriteInt(charInfoPackage.Race);
                
                if (charInfoPackage.ClassId == charInfoPackage.BaseClassId)
                {
                    WriteInt(charInfoPackage.ClassId);
                }
                else
                {
                    WriteInt(charInfoPackage.BaseClassId);
                }
                
                WriteInt(0x01); // active ??
			
                WriteInt(0x00); // x
                WriteInt(0x00); // y
                WriteInt(0x00); // z
			
                WriteDouble(charInfoPackage.CurrentHp); // hp cur
                WriteDouble(charInfoPackage.CurrentMp); // mp cur
			
                WriteInt(charInfoPackage.Sp);
                WriteLong(charInfoPackage.Exp);
                WriteInt(charInfoPackage.Level);
			
                WriteInt(charInfoPackage.Karma); // karma
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
                    WriteInt(charInfoPackage.GetPaperdollObjectId(id));

               
                for (byte id = 0; id < 17; id++)
                    WriteInt(charInfoPackage.GetPaperdollItemId(id));
                
                WriteInt(charInfoPackage.HairStyle);
                WriteInt(charInfoPackage.HairColor);
                WriteInt(charInfoPackage.Face);
			
                WriteDouble(charInfoPackage.MaxHp); // hp max
                WriteDouble(charInfoPackage.MaxMp); // mp max
                
                long deleteTime = charInfoPackage.DeleteTimer;
                int accesslevels = charInfoPackage.AccessLevel;
                int deletedays = 0;
                if (deleteTime > 0)
                {
                    deletedays = (int) ((deleteTime - DateTimeHelper.CurrentUnixTimeMillis()) / 1000);
                }
                else if (accesslevels < 0)
                {
                    deletedays = -1; // like L2OFF player looks dead if he is banned.
                }
			
                WriteInt(deletedays); // days left before
                // delete .. if != 0
                // then char is inactive
                WriteInt(charInfoPackage.ClassId);
			
                if (i == _activeId)
                {
                    WriteInt(0x01);
                }
                else
                {
                    WriteInt(0x00); // c3 auto-select char
                }
			
                WriteByte(charInfoPackage.GetEnchantEffect() > 127 ? 127 : charInfoPackage.GetEnchantEffect());
			
                WriteInt(charInfoPackage.AugmentationId);
            }
            */
        }
    }
}