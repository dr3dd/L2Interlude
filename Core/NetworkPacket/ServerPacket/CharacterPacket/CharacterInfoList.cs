using System.Threading.Tasks;
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
            list.ForEach(currentUser =>
            {
                WriteString(currentUser.CharacterName);
                WriteInt(currentUser.CharacterId);
                WriteString(_accountName);
                WriteInt(_sessionId); //GetAccountID
                WriteInt(0x00); //ClanId GetPledgeID
                WriteInt(0x00); //GetBuilder
                WriteInt(currentUser.Gender);
                WriteInt(currentUser.Race);
                WriteInt(currentUser.ClassId);
                WriteInt(0x01); // GetWorld active ??
                WriteInt(currentUser.X); // x
                WriteInt(currentUser.Y); // y
                WriteInt(currentUser.Z); // z
                WriteDouble(currentUser.Hp); // hp cur
                WriteDouble(currentUser.Mp); // mp cur
                WriteInt(currentUser.Sp);
                WriteLong(currentUser.Exp);
                WriteInt(currentUser.Level);
                WriteInt(currentUser.Pk); // karma
                WriteInt(0x00); //GetDuel
                WriteInt(0x00); //GetPKPardon
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                WriteInt(0x00);
                
                //for (byte id = 0; id < 17; id++)
                //user item ids of items
                WriteInt(currentUser.StUnderwear); //Under
                WriteInt(currentUser.StLeftEar); //Left Earning
                WriteInt(currentUser.StRightEar); //Right Earning
                WriteInt(currentUser.StNeck); //Necklace
                WriteInt(currentUser.StLeftFinger); //Left Finger
                WriteInt(currentUser.StRightFinger); //Right Finger
                WriteInt(currentUser.StHead); //Head
                WriteInt(currentUser.StRightHand); //Right hand
                WriteInt(currentUser.StLeftHand); //Left hand
                WriteInt(currentUser.StGloves); //Gloves
                WriteInt(currentUser.StChest); //Chest
                WriteInt(currentUser.StLegs); //Legs
                WriteInt(currentUser.StFeet); //Feet
                WriteInt(currentUser.StBack); //Back
                WriteInt(currentUser.StBothHand); //Left Right Hand
                WriteInt(currentUser.StFace); //Face 
                WriteInt(currentUser.StHair); //Hair

                //for (byte id = 0; id < 17; id++)
                //item ids
                WriteInt(_characterList.GetItem(currentUser.StUnderwear)); //Under
                WriteInt(_characterList.GetItem(currentUser.StLeftEar)); //Left Earning
                WriteInt(_characterList.GetItem(currentUser.StRightEar)); //Right Earning
                WriteInt(_characterList.GetItem(currentUser.StNeck)); //Necklace
                WriteInt(_characterList.GetItem(currentUser.StLeftFinger)); //Left Finger
                WriteInt(_characterList.GetItem(currentUser.StRightFinger)); //Right Finger
                WriteInt(_characterList.GetItem(currentUser.StHead)); //Head
                WriteInt(_characterList.GetItem(currentUser.StRightHand)); //Right hand
                WriteInt(_characterList.GetItem(currentUser.StLeftHand)); //Left hand
                WriteInt(_characterList.GetItem(currentUser.StGloves)); //Gloves
                WriteInt(_characterList.GetItem(currentUser.StChest)); //Chest
                WriteInt(_characterList.GetItem(currentUser.StLegs)); //Legs
                WriteInt(_characterList.GetItem(currentUser.StFeet)); //Feet
                WriteInt(_characterList.GetItem(currentUser.StBack)); //Back
                WriteInt(_characterList.GetItem(currentUser.StBothHand)); //Left Right Hand
                WriteInt(_characterList.GetItem(currentUser.StFace)); //Face 
                WriteInt(_characterList.GetItem(currentUser.StHair)); //Hair
                
                WriteInt(currentUser.HairShapeIndex);
                WriteInt(currentUser.HairColorIndex);
                WriteInt(currentUser.FaceIndex);
                
                WriteDouble(currentUser.MaxHp); // hp max
                WriteDouble(currentUser.MaxMp); // mp max
                WriteInt(0); // elapsedDaysToDelete
                WriteInt(currentUser.ClassId);
                WriteInt(0x01);
                WriteByte(0);
                WriteInt(0);
            });
        }
    }
}