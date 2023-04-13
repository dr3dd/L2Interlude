using Core.Module.Player;

namespace Core.Module.NpcAi;

public struct Talker
{
    public int ObjectId { get; }
    public int Karma { get; }
    public int Level { get; }
    public int Occupation { get; }
    public int Race { get; }
    public PlayerInstance PlayerInstance { get; }
    public int IsPledgeMaster { get; }
    public int PledgeId { get; }

    public Talker(PlayerInstance playerInstance)
    {
        ObjectId = playerInstance.ObjectId;
        Karma = 0;
        Level = playerInstance.Level;
        Occupation = playerInstance.TemplateHandler().GetClassId();
        Race = playerInstance.TemplateHandler().GetRaceId();
        PlayerInstance = playerInstance;
        IsPledgeMaster = 0;
        PledgeId = 0;
    }
}