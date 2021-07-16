using System;

namespace Core.Module.CharacterData.Template
{
    public interface ITemplateHandler
    {
        int GetClassId();
        int GetRaceId();
        int GetInt();
        int GetStr();
        int GetCon();
        int GetMen();
        int GetDex();
        int GetWit();
    }
}