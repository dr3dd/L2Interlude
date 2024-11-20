using System.Collections.Concurrent;
using Core.Enums;
using Core.Module.Player;

namespace Core.Module.NpcAi;

public struct Talker
{
    public int ObjectId { get; }
    public int Karma { get; }
    public int Level { get; }
    public string Occupation { get; }
    public int OccupationId { get; }
    public int Race { get; }
    public PlayerInstance PlayerInstance { get; }
    public int IsPledgeMaster { get; }
    public int PledgeId { get; }
    public NoblessType NoblessType { get; }
    public HeroType HeroType { get; }
    public int quest_last_reward_time { get; set; }
    private ConcurrentDictionary<int, string> _questChoiceCollection;

    public Talker(PlayerInstance playerInstance)
    {
        var template = playerInstance.TemplateHandler();
        _questChoiceCollection = new ConcurrentDictionary<int, string>();
        ObjectId = playerInstance.ObjectId;
        Karma = 0;
        Level = playerInstance.Level;
        OccupationId = template.GetClassId();
        Occupation = template.GetClassKey();
        Race = template.GetRaceId();
        PlayerInstance = playerInstance;
        IsPledgeMaster = 0;
        PledgeId = 0;
        NoblessType = NoblessType.NONE;
        HeroType = HeroType.NONE;
        quest_last_reward_time = 0;
    }
    
    public void AddQuestChoice(int choice, string value)
    {
        _questChoiceCollection.TryAdd(choice, value);
    }

    public ConcurrentDictionary<int, string> GetQuestChoiceCollection()
    {
        return _questChoiceCollection;
    }

    public void Clear()
    {
        _questChoiceCollection.Clear();
    }
}