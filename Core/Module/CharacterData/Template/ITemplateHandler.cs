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
        float GetCpBegin(int level);
        float GetHpBegin(int level);
        float GetMpBegin(int level);
        int GetBasePhysicalAttack();
        int GetBaseAttackSpeed();
        int GetBaseCritical();
        int GetBaseDefend();
        int GetBaseMagicAttack();
        int GetBaseMagicDefend();
        int GetBaseAttackRange();
        
        public Location GetInitialStartPoint();
    }
}