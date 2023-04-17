using System.Threading;
using System.Threading.Tasks;

namespace Core.Module.CharacterData;

public class CharacterHpMpRegeneration
{
    private readonly Character _character;
    private readonly Timer _regenerationTimer;
    private readonly SemaphoreSlim _semaphore;
    private CancellationTokenSource _cancellationTokenSource;

    public CharacterHpMpRegeneration(Character character)
    {
        _character = character;
        _semaphore = new SemaphoreSlim(1);
        _regenerationTimer = new Timer(HpMpRegenerationCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    public void StartRegeneration()
    {
        if (_regenerationTimer == null) return;
        if (_regenerationTimer.Change(3300, Timeout.Infinite))
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }

    public void StopRegeneration()
    {
        _regenerationTimer.Change(Timeout.Infinite, Timeout.Infinite);
        _cancellationTokenSource?.Cancel();
    }

    private void HpMpRegenerationCallback(object state)
    {
        _semaphore.Wait();
        try
        {
            _character.CharacterStatus().CurrentHp += _character.CharacterBaseStatus().GetHpRegenRate();
            if (_character.CharacterStatus().CurrentHp >= _character.CharacterBaseStatus().GetMaxHp())
            {
                _character.CharacterStatus().CurrentHp = _character.CharacterBaseStatus().GetMaxHp();
                StopRegeneration();
            }
            Task.Run(() => _character.SendStatusUpdate());
        }
        finally
        {
            _semaphore.Release();
        }
    }
}