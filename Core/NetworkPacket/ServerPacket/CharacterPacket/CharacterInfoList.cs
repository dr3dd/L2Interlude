using Core.Controller;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    public class CharacterInfoList : Network.ServerPacket
    {
        private readonly string _accountName;
        private readonly int _sessionId;
        private readonly CharacterList _characterList;
        private readonly GameServiceController _controller;

        public CharacterInfoList(string accountName, GameServiceController controller)
        {
            _controller = controller;
            _sessionId = controller.SessionKey.PlayOkId1;
            _accountName = accountName;
            _characterList = new CharacterList();
        }
        
        public override void Write()
        {
            var list = _characterList.GetCharacterList(_accountName);
            _controller.GameServiceHelper.SetCharSelection(list);

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
                WriteInt(entity.XLoc); // x
                WriteInt(entity.YLoc); // y
                WriteInt(entity.ZLoc); // z
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
                
                //for (byte id = 0; id < 17; id++)
                //object ids of items
                WriteInt(0); //Under
                WriteInt(0); //Left Earning
                WriteInt(0); //Right Earning
                WriteInt(0); //Necklace
                WriteInt(0); //Left Finger
                WriteInt(0); //Right Finger
                WriteInt(0); //Head
                WriteInt(100); //Right hand
                WriteInt(200); //Left hand
                WriteInt(300); //Gloves
                WriteInt(500); //Chest
                WriteInt(0); //Legs
                WriteInt(400); //Feet
                WriteInt(0); //Back
                WriteInt(0); //Left Right Hand
                WriteInt(0); //Face 
                WriteInt(0); //Hair

                //for (byte id = 0; id < 17; id++)
                //items
                WriteInt(0); //Under
                WriteInt(0); //Left Earning
                WriteInt(0); //Right Earning
                WriteInt(0); //Necklace
                WriteInt(0); //Left Finger
                WriteInt(0); //Right Finger
                WriteInt(0); //Head
                WriteInt(2369); //Right hand
                WriteInt(6377); //Left hand
                WriteInt(6380); //Gloves
                WriteInt(6379); //Chest
                WriteInt(0); //Legs
                WriteInt(6381); //Feet
                WriteInt(0); //Back
                WriteInt(0); //Left Right Hand
                WriteInt(0); //Face 
                WriteInt(0); //Hair
                
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