namespace Core.Module.CharacterData
{
    public interface ICharacterCombat
    {
        double GetMovementSpeedMultiplier();
        int GetPhysicalAttack();
        int GetPhysicalDefence();
        float GetCollisionRadius();
        float GetCollisionHeight();
        float GetCharacterSpeed();
        float GetRunSpeed();
        float GetWalkSpeed();
        int GetEvasion();
        int GetAccuracy();
        int GetCriticalRate();
    }
}