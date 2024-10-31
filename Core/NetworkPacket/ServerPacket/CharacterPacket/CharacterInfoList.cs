using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket;

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
        
    public override async Task WriteAsync()
    {
        var list = await _characterList.GetCharacterList(_accountName);
        _controller.GameServiceHelper.SetCharSelection(list);

        await WriteByteAsync(0x13);
        await WriteIntAsync(list.Count);
        foreach (var currentUser in list)
        {
            await WriteStringAsync(currentUser.CharacterName);
            await WriteIntAsync(currentUser.CharacterId);
            await WriteStringAsync(_accountName);
            await WriteIntAsync(_sessionId); //GetAccountID
            await WriteIntAsync(0x00); //ClanId GetPledgeID
            await WriteIntAsync(0x00); //GetBuilder
            await WriteIntAsync(currentUser.Gender);
            await WriteIntAsync(currentUser.Race);
            await WriteIntAsync(currentUser.ClassId);
            await WriteIntAsync(0x01); // GetWorld active ??
            await WriteIntAsync(currentUser.X); // x
            await WriteIntAsync(currentUser.Y); // y
            await WriteIntAsync(currentUser.Z); // z
            await WriteDoubleAsync(currentUser.Hp); // hp cur
            await WriteDoubleAsync(currentUser.Mp); // mp cur
            await WriteIntAsync(currentUser.Sp);
            await WriteLongAsync(currentUser.Exp);
            await WriteIntAsync(currentUser.Level);
            await WriteIntAsync(currentUser.Pk); // karma
            await WriteIntAsync(0x00); //GetDuel
            await WriteIntAsync(0x00); //GetPKPardon
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
            await WriteIntAsync(0x00);
                
            //for (byte id = 0; id < 17; id++)
            //user item ids of items
            await WriteIntAsync(currentUser.StUnderwear); //Under
            await WriteIntAsync(currentUser.StLeftEar); //Left Earning
            await WriteIntAsync(currentUser.StRightEar); //Right Earning
            await WriteIntAsync(currentUser.StNeck); //Necklace
            await WriteIntAsync(currentUser.StLeftFinger); //Left Finger
            await WriteIntAsync(currentUser.StRightFinger); //Right Finger
            await WriteIntAsync(currentUser.StHead); //Head
            await WriteIntAsync(currentUser.StRightHand); //Right hand
            await WriteIntAsync(currentUser.StLeftHand); //Left hand
            await WriteIntAsync(currentUser.StGloves); //Gloves
            await WriteIntAsync(currentUser.StChest); //Chest
            await WriteIntAsync(currentUser.StLegs); //Legs
            await WriteIntAsync(currentUser.StFeet); //Feet
            await WriteIntAsync(currentUser.StBack); //Back
            await WriteIntAsync(currentUser.StBothHand); //Left Right Hand
            await WriteIntAsync(currentUser.StFace); //Face 
            await WriteIntAsync(currentUser.StHair); //Hair

            //for (byte id = 0; id < 17; id++)
            //item ids
            await WriteIntAsync(_characterList.GetItem(currentUser.StUnderwear)); //Under
            await WriteIntAsync(_characterList.GetItem(currentUser.StLeftEar)); //Left Earning
            await WriteIntAsync(_characterList.GetItem(currentUser.StRightEar)); //Right Earning
            await WriteIntAsync(_characterList.GetItem(currentUser.StNeck)); //Necklace
            await WriteIntAsync(_characterList.GetItem(currentUser.StLeftFinger)); //Left Finger
            await WriteIntAsync(_characterList.GetItem(currentUser.StRightFinger)); //Right Finger
            await WriteIntAsync(_characterList.GetItem(currentUser.StHead)); //Head
            await WriteIntAsync(_characterList.GetItem(currentUser.StRightHand)); //Right hand
            await WriteIntAsync(_characterList.GetItem(currentUser.StLeftHand)); //Left hand
            await WriteIntAsync(_characterList.GetItem(currentUser.StGloves)); //Gloves
            await WriteIntAsync(_characterList.GetItem(currentUser.StChest)); //Chest
            await WriteIntAsync(_characterList.GetItem(currentUser.StLegs)); //Legs
            await WriteIntAsync(_characterList.GetItem(currentUser.StFeet)); //Feet
            await WriteIntAsync(_characterList.GetItem(currentUser.StBack)); //Back
            await WriteIntAsync(_characterList.GetItem(currentUser.StBothHand)); //Left Right Hand
            await WriteIntAsync(_characterList.GetItem(currentUser.StFace)); //Face 
            await WriteIntAsync(_characterList.GetItem(currentUser.StHair)); //Hair
                
            await WriteIntAsync(currentUser.HairShapeIndex);
            await WriteIntAsync(currentUser.HairColorIndex);
            await WriteIntAsync(currentUser.FaceIndex);
                
            await WriteDoubleAsync(currentUser.MaxHp); // hp max
            await WriteDoubleAsync(currentUser.MaxMp); // mp max
            await WriteIntAsync(0); // elapsedDaysToDelete
            await WriteIntAsync(currentUser.ClassId);
            await WriteIntAsync(0x01);
            await WriteByteAsync(0);
            await WriteIntAsync(0);
        }
    }
}