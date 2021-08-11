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
            var list = _characterList.GetCharacterList(_accountName).Result;
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
                WriteInt(0); // x
                WriteInt(0); // y
                WriteInt(0); // z
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
                WriteInt(0); //Right hand
                WriteInt(0); //Left hand
                WriteInt(0); //Gloves
                WriteInt(0); //Chest
                WriteInt(0); //Legs
                WriteInt(0); //Feet
                WriteInt(0); //Back
                WriteInt(0); //Left Right Hand
                WriteInt(0); //Face 
                WriteInt(0); //Hair

                //for (byte id = 0; id < 17; id++)
                //items
                WriteInt(entity.StUnderwear); //Under
                WriteInt(entity.StLeftEar); //Left Earning
                WriteInt(entity.StRightEar); //Right Earning
                WriteInt(entity.StNeck); //Necklace
                WriteInt(entity.StLeftFinger); //Left Finger
                WriteInt(entity.StRightFinger); //Right Finger
                WriteInt(entity.StHead); //Head
                WriteInt(entity.StRightHand); //Right hand
                WriteInt(entity.StLeftHand); //Left hand
                WriteInt(entity.StGloves); //Gloves
                WriteInt(entity.StChest); //Chest
                WriteInt(entity.StLegs); //Legs
                WriteInt(entity.StFeet); //Feet
                WriteInt(entity.StBack); //Back
                WriteInt(entity.StBothHand); //Left Right Hand
                WriteInt(entity.StFace); //Face 
                WriteInt(entity.StHair); //Hair
                
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