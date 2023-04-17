namespace Core.Module.CharacterData;

public interface ICharacterBaseStatus
{
    int GetMaxHp();
    int GetMaxMp();
    float GetHpRegenRate();
    float GetMpRegenRate();
}