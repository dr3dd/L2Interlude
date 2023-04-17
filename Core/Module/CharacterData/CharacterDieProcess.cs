using System.Threading.Tasks;

namespace Core.Module.CharacterData;

public class CharacterDieProcess
{
    private readonly Character _character;
    private bool _isDying;

    public CharacterDieProcess(Character character)
    {
        _character = character;
    }

    /// <summary>
    /// Initiates the die process of the character.
    /// </summary>
    public void Start()
    {
        if (_isDying)
        {
            return;
        }

        _isDying = true;
        //_character.StopMoving();
        //_character.StopAttacking();
        //_character.BroadcastMessage($"{_character.Name} has died.");
        Task.Delay(15000).ContinueWith(t => Respawn());
    }

    /// <summary>
    /// Respawn the character and revive them with full HP and MP.
    /// </summary>
    private void Respawn()
    {
        _character.CharacterStatus().CurrentHp = _character.CharacterBaseStatus().GetMaxHp();
        _character.CharacterStatus().CurrentMp = _character.CharacterBaseStatus().GetMaxMp();
        _isDying = false;
        
        //_character.TeleportToRespawnPoint();
        //_character.BroadcastMessage($"{_character.Name} has been revived!");
    }
}