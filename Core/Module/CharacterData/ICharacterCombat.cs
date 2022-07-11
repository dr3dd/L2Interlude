namespace Core.Module.CharacterData
{
    public interface ICharacterCombat
    {
        int GetPhysicalAttack();
        int GetPhysicalDefence();
        float GetCollisionRadius();
        float GetCollisionHeight();
        int GetCharacterSpeed();
        int GetRunSpeed();
        int GetWalkSpeed();
        int GetEvasion();
        int GetAccuracy();
        int GetCriticalRate();
    }
}