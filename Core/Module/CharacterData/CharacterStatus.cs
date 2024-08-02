namespace Core.Module.CharacterData;

public class CharacterStatus
{
    private readonly Character _character;
    private readonly CharacterHpMpRegeneration _characterHpRegeneration;
    private readonly CharacterDieProcess _characterDieProcess;
    public bool IsDead { get; set; } = false;

    public CharacterStatus(Character character)
    {
        _character = character;
        _characterHpRegeneration = new CharacterHpMpRegeneration(character);
        _characterDieProcess = new CharacterDieProcess(character);
    }

    public float CurrentMp { get; set; }
    public double CurrentHp { get; set; }

    public void IncreaseCurrentHp(double heal)
    {
        CurrentHp += heal;
        if (CurrentHp >= _character.CharacterBaseStatus().GetMaxHp())
        {
            CurrentHp = _character.CharacterBaseStatus().GetMaxHp();
            _characterHpRegeneration.StopRegeneration();
        }
    }

    public void DecreaseCurrentHp(double damage)
    {
        if (damage <= 0) return;
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            _characterDieProcess.Start();
            return;
        }
        _characterHpRegeneration.StartRegeneration();
    }
}