using Core.Module.CharacterData.Template;
using Helpers;
using L2Logger;
using System;

namespace Core.Module.Player
{
    public class PlayerCharacterInfo
    {
        public int CharacterId { get; set; }
        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        
        public long Exp { get; set; }
        public int Sp { get; set; }
        public int StUnderwear { get; set; }
        public int StRightEar { get; set; }
        public int StLeftEar { get; set; }
        public int StNeck { get; set; }
        public int StRightFinger { get; set; }
        public int StLeftFinger { get; set; }
        public int StHead { get; set; }
        public int StRightHand { get; set; }
        public int StLeftHand { get; set; }
        public int StGloves { get; set; }
        public int StChest { get; set; }
        public int StLegs { get; set; }
        public int StFeet { get; set; }
        public int StBack { get; set; }
        public int StBothHand { get; set; }
        public int StHair { get; set; }
        public int StFace { get; set; }
        public int StHairAll { get; set; }
        public BitStorage QuestFlag { get; set; } = new BitStorage(new byte[128]);

        private readonly PlayerInstance _playerInstance;
        private readonly ITemplateHandler _templateHandler;

        public PlayerCharacterInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _templateHandler = _playerInstance.TemplateHandler();
        }

        public int GetMAtkSpd()
        {
            return _templateHandler.GetBaseAttackSpeed();
        }

        private int ConvertToInternalOneTimeQuestID(int questId)
        {
            int result = 0;
            if (questId < 10255 || questId > 12047)
            {
                if (questId <= 12047)
                {
                    return questId;
                }
                else
                {
                    Console.WriteLine($"Current onetime quest id({questId}) is over 12047!!");
                }
            }
            else
            {
                return (questId - 10000);
            }
            return result;
        }

        public void SetOneTimeFlag(int questId, bool complete)
        {
            int c_quest_id = ConvertToInternalOneTimeQuestID(questId);
            QuestFlag.SetFlag(c_quest_id, complete);
        }

        public bool GetOneTimeFlag(int questId)
        {
            int c_quest_id = ConvertToInternalOneTimeQuestID(questId);
            return QuestFlag.GetFlag(c_quest_id);
        }

        internal byte[] GetQuestFlags()
        {
            return QuestFlag.GetAllFlags();
        }
    }
}