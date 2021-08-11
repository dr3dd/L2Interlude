using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.CharacterData
{
    public class CharacterList
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IUserItemRepository _userItemRepository;
        private readonly IDictionary<int, int> _userItem;
        private readonly List<CharacterListModel> _listModels;
        public CharacterList()
        {
            _characterRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWork>().Characters;
            _userItemRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWork>().UserItems;
            _userItem = new Dictionary<int, int>();
            _listModels = new List<CharacterListModel>();
        }

        public async Task<List<CharacterListModel>> GetCharacterList(string accountName)
        {
            var list = await _characterRepository.GetCharactersByAccountNameAsync(accountName);
            list.ForEach(PrepareCharacterList);
            return _listModels;
        }

        private async void PrepareCharacterList(CharacterEntity entity)
        {
            var items = await _userItemRepository.GetInventoryItemsByOwnerId(entity.CharacterId);
            items.ForEach(item => { _userItem.Add(item.UserItemId, item.ItemId); });

            var characterList = new CharacterListModel
            {
                CharacterId = entity.CharacterId,
                CharacterName = entity.CharacterName,
                Level = entity.Level,
                ClassId = entity.ClassId,
                Race = entity.Race,
                Gender = entity.Gender,
                Hp = entity.Hp,
                Mp = entity.Mp,
                MaxHp = entity.MaxHp,
                MaxMp = entity.MaxMp,
                Sp = entity.Sp,
                Exp = entity.Exp,
                Pk = entity.Pk,
                HairStyle = entity.HairStyle,
                HairColor = entity.HairColor,
                Face = entity.Face,
                StUnderwear = _userItem.ContainsKey(entity.StUnderwear) ? _userItem[entity.StUnderwear] : 0,
                StRightEar = _userItem.ContainsKey(entity.StRightEar) ? _userItem[entity.StRightEar] : 0,
                StLeftEar = _userItem.ContainsKey(entity.StLeftEar) ? _userItem[entity.StLeftEar] : 0,
                StNeck = _userItem.ContainsKey(entity.StNeck) ? _userItem[entity.StNeck] : 0,
                StRightFinger = _userItem.ContainsKey(entity.StRightFinger) ? _userItem[entity.StRightFinger] : 0,
                StLeftFinger = _userItem.ContainsKey(entity.StLeftFinger) ? _userItem[entity.StLeftFinger] : 0,
                StHead = _userItem.ContainsKey(entity.StHead) ? _userItem[entity.StHead] : 0,
                StRightHand = _userItem.ContainsKey(entity.StRightHand) ? _userItem[entity.StRightHand] : 0,
                StLeftHand = _userItem.ContainsKey(entity.StLeftHand) ? _userItem[entity.StLeftHand] : 0,
                StGloves = _userItem.ContainsKey(entity.StGloves) ? _userItem[entity.StGloves] : 0,
                StChest = _userItem.ContainsKey(entity.StChest) ? _userItem[entity.StChest] : 0,
                StLegs = _userItem.ContainsKey(entity.StLegs) ? _userItem[entity.StLegs] : 0,
                StFeet = _userItem.ContainsKey(entity.StFeet) ? _userItem[entity.StFeet] : 0,
                StBack = _userItem.ContainsKey(entity.StBack) ? _userItem[entity.StBack] : 0,
                StBothHand = _userItem.ContainsKey(entity.StBothHand) ? _userItem[entity.StBothHand] : 0,
                StHair = _userItem.ContainsKey(entity.StHair) ? _userItem[entity.StHair] : 0,
                StFace = _userItem.ContainsKey(entity.StFace) ? _userItem[entity.StFace] : 0,
                StHairAll = _userItem.ContainsKey(entity.StHairAll) ? _userItem[entity.StHairAll] : 0,
            };
            _listModels.Add(characterList);
        }
    }
}