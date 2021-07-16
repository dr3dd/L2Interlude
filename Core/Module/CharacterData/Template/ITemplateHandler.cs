namespace Core.Module.CharacterData.Template
{
    public interface ITemplateHandler
    {
        byte GetClassId();
        byte GetRaceId();
        int GetInt();
        int GetStr();
        int GetCon();
        int GetMen();
        int GetDex();
        int GetWit();
    }
}