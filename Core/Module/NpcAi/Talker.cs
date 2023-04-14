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

    public Talker(PlayerInstance playerInstance)
    {
        var template = playerInstance.TemplateHandler();
        ObjectId = playerInstance.ObjectId;
        Karma = 0;
        Level = playerInstance.Level;
        OccupationId = template.GetClassId();
        Occupation = template.GetClassKey();
        Race = template.GetRaceId();
        PlayerInstance = playerInstance;
        IsPledgeMaster = 0;
        PledgeId = 0;
    }
}