namespace Core.Module.NpcAi.Ai;

public interface ICitizen
{
    void Talked(Talker talker);
    void Created();
}