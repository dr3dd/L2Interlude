using Core.Enums;
using Core.Module.ItemData;
using Core.Module.Player.PlayerInventoryModel;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 25.11.2024 19:17:11

namespace Core.Module.Player
{
    public class PlayerQuest
    {
        private readonly PlayerInstance _playerInstance;
        private List<UserQuestEntity> _quests;
        private readonly IUserQuestRepository _userQuestRepository;
        public PlayerQuest(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _quests = new List<UserQuestEntity>();
            _userQuestRepository = playerInstance.GetUnitOfWork().UserQuest;

        }

        private long GetMaxMemoCount(QuestType questType)
        {
            switch (questType)
            {
                case QuestType.QUEST_NONREGIST:
                    return 20;
                case QuestType.QUEST_NORMAL:
                    return 41;
                default:
                    return 0;
            }
        }

        public async Task SetMemo(int quest_id)
        {
            await SetMemo(QuestType.QUEST_NORMAL, quest_id);
        }

        public async Task<int> SetMemo(QuestType questType, int quest_id)
        {
            if (HaveMemo(questType, quest_id))
            {
                return await Task.FromResult(0);
            }
            else
            {
                if (GetMaxMemoCount(questType) <= GetMemoCount())//TODO may be the condition is incorrect
                {
                    LoggerManager.Error($"SetMemo() failed. memo[{quest_id}] not enough slot on creature[{_playerInstance.CharacterName}]");
                    return await Task.FromResult(0);
                }
                else
                {
                    UserQuestEntity newMemo = new UserQuestEntity()
                    {
                        CharacterId = _playerInstance.PlayerCharacterInfo().CharacterId,
                        QuestNo = quest_id,
                        Journal = 0,
                        State1 = 0,
                        State2 = 0,
                        State3 = 0,
                        State4 = 0,
                        Type = (byte)questType
                    };

                    _quests.Add(newMemo);
                    return await _userQuestRepository.AddAsync(newMemo);
                }
            }
        }

        public bool HaveMemo(QuestType questType, int quest_id)
        {
            return _quests.Find(q => q.QuestNo == quest_id && q.Type == (byte)questType) != null;
        }
        
        public bool HaveMemo(int quest_id)
        {
            return HaveMemo(QuestType.QUEST_NORMAL, quest_id) || HaveMemo(QuestType.QUEST_NORMAL, quest_id);
        }

        public async Task SetFlagJournal(int quest_id, int flag)
        {
            var currentQuest = _quests.SingleOrDefault(q => q.QuestNo == quest_id);
            if (currentQuest != null)
            {
                currentQuest.Journal = flag;
                await _userQuestRepository.UpdateAsync(currentQuest);
                await SendQuestList();
            }
        }

        public int GetMemoCount()
        {
            return _quests.Count();
        }

        public List<UserQuestEntity> GetMemoAll()
        {
            return _quests;
        }

        public async Task RemoveMemo(int quest_id)
        {
            var currentQuest = _quests.SingleOrDefault(q => q.QuestNo == quest_id);
            if (currentQuest != null && _quests.Remove(currentQuest))
            {
                await _userQuestRepository.DeleteAsync(currentQuest);
                await SendQuestList();
            }
        }

        public async Task RestoreQuests()
        {
            var userQuests = await _userQuestRepository.GetAllAsync(_playerInstance.PlayerCharacterInfo().CharacterId);
            _quests.AddRange(userQuests);
        }

        public async Task SendQuestList() {
            await _playerInstance.SendPacketAsync(new QuestList(_playerInstance));
        }

        internal int GetMemoStateEx(int quest_id, int slot)
        {
            int result = 0;
            var currentQuest = _quests.SingleOrDefault(q => q.QuestNo == quest_id);
            if (currentQuest != null)
            {
                switch (slot)
                {
                    case 0:
                    case 1:
                        result = currentQuest.Journal; 
                        break;
                    case 2:
                        result = currentQuest.State1;
                        break;
                    case 3:
                        result = currentQuest.State2;
                        break;
                    case 4:
                        result = currentQuest.State3;
                        break;
                    case 5:
                        result = currentQuest.State4;
                        break;
                }
            }
            return result;
        }

        public async Task DestroyQuest(int questId)
        {
            await RemoveMemo(questId);
        }
    }
}
