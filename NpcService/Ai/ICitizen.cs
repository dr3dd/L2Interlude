namespace NpcService.Ai
{
    public interface ICitizen
    {
        void Talked(Talker talker);
        void Created();
    }
}