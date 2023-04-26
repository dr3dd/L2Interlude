using System.Collections.Generic;

namespace Core.Module.CharacterData.Template
{
    public interface ITemplateHandler
    {
        byte GetClassId();
        string GetClassKey();
        byte GetRaceId();
        byte GetInt();
        byte GetStr();
        byte GetCon();
        byte GetMen();
        byte GetDex();
        byte GetWit();
        float GetCpBegin(byte level);
        float GetHpBegin(byte level);
        float GetMpBegin(byte level);
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
        int GetBaseUnderWaterLowSpeed();
        int GetBaseUnderWaterHighSpeed();
        IEnumerable<string> GetInitialEquipment();
        Location GetInitialStartPoint();
        float GetCollisionRadius();
        float GetCollisionHeight();
        float GetBaseHpRegen(int level);
    }
}