namespace Core.Module.CharacterData
{
    public interface ICharacterCombat
    {
        double GetMovementSpeedMultiplier();
        int GetPhysicalAttack();
        int GetPhysicalDefence();
        int GetMagicalDefence();
        float GetCollisionRadius();
        float GetCollisionHeight();
        double GetCharacterSpeed();
        double GetHighSpeed();
        double GetLowSpeed();
        int GetEvasion();
        int GetAccuracy();
        int GetCriticalRate();
        int GetMagicalAttack();
        int GetPhysicalAttackSpeed();
    }
}