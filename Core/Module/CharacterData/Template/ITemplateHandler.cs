namespace Core.Module.CharacterData.Template
{
    public interface ITemplateHandler
    {
        byte GetClassId();
        byte GetRaceId();
        byte GetInt();
        byte GetStr();
        byte GetCon();
        byte GetMen();
        byte GetDex();
        byte GetWit();
        float GetCpBegin(int level);
        float GetHpBegin(int level);
        float GetMpBegin(int level);
        int GetBasePhysicalAttack();
        int GetBaseAttackSpeed();
        int GetBaseCritical();
        int GetBaseDefendUpperBody();
        int GetBaseDefendLowerBody();
        int GetBaseDefendPitch();
        int GetBaseDefendBoots();
        int GetBaseDefendGloves();
        int GetBaseDefendUnderwear();
        int GetBaseDefendMantle();
        int GetBaseMagicAttack();
        int GetBaseMagicDefendLeftEarring();
        int GetBaseMagicDefendRightEarring();
        int GetBaseMagicDefendLeftRing();
        int GetBaseMagicDefendRightRing();
        int GetBaseMagicDefendNecklace();
        int GetBaseAttackRange();
        int GetBaseGroundHighSpeed();
        int GetBaseGroundLowSpeed();
        
        public Location GetInitialStartPoint();
    }
}