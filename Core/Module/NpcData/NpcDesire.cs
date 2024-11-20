using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.NpcAi;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;
using L2Logger;

namespace Core.Module.NpcData;

public class NpcDesire : CharacterDesire
{
    private readonly NpcInstance _npcInstance;
    private const int DefaultCapacity = 100;
    private DesireObject[] _desires;
    private int _desireCount;
    private CancellationTokenSource _decayCancellationTokenSource;
    private CancellationTokenSource _desireProcessingCancellationTokenSource;
    private bool _isProcessingDesires = false;
    private Task _decayTask;
    private readonly object _lockObject = new();
    private readonly NpcAi _npcAi;
        
    public NpcDesire(NpcInstance npcInstance) : base(npcInstance)
    {
        _npcInstance = npcInstance;
        _desires = new DesireObject[DefaultCapacity];
        _desireCount = 0;
        _npcAi = npcInstance.NpcAi();
    }
    
    public async Task AddEffectActionDesire(int actionId, int timeDesire, int desire)
    {
        AddNpcDesire(new DesireObject(Desire.EffectDesire, actionId, timeDesire, desire));
    }

    public async Task AddAttackDesire(Talker attacker, int i, int desire)
    {
        AddNpcDesire(new DesireObject(Desire.AttackDesire, attacker.PlayerInstance, desire));

        if (_decayTask is null || _decayTask.Status == TaskStatus.RanToCompletion || _decayTask.Status == TaskStatus.Canceled)
        {
            await StartDecayProcess();
        }
    }

    public async Task AddMoveAroundDesire(int timeDesire, int desire)
    {
        AddNpcDesire(new DesireObject(Desire.MoveAroundDesire, _npcInstance, desire));
        //start no desire when moving finished
        TaskManagerScheduler.Schedule(() =>
        {
            _npcInstance.NpcAi().NoDesire();
        }, timeDesire * 1000);
    }

    public void AddMoveToDesire(int startX, int startY, int startZ, int desire)
    {
        AddNpcDesire(new DesireObject(Desire.MoveToDesire, _npcInstance, startX, startY, startZ, desire));
        //start no desire when moving finished
        TaskManagerScheduler.Schedule(() =>
        {
            _npcInstance.NpcAi().NoDesire();
        }, desire * 1000);
    }

    /// <summary>
    /// Add Desire in array
    /// </summary>
    /// <param name="newDesire"></param>
    private void AddNpcDesire(DesireObject newDesire)
    {
        lock (_lockObject)
        {
            try
            {
                for (int i = 0; i < _desireCount; i++)
                {
                    if (_desires[i]?.Target == newDesire.Target && _desires[i]?.Type == newDesire.Type)
                    {
                        if (newDesire.Priority > _desires[i].Priority)
                        {
                            _desires[i].Priority = newDesire.Priority;
                        }
                        return;
                    }
                }

                if (_desireCount >= _desires.Length)
                {
                    Array.Resize(ref _desires, _desires.Length * 2);
                }

                _desires[_desireCount++] = newDesire;
                SortDesiresByPriority();
            }
            catch (Exception ex)
            {
                LoggerManager.Warn("NpcDesire: " + ex.Message);
            }
        }
    }
    
    /// <summary>
    /// Sort array by Priority
    /// </summary>
    private void SortDesiresByPriority()
    {
        Array.Sort(_desires, 0, _desireCount, Comparer<DesireObject>.Create((x, y) => y.Priority.CompareTo(x.Priority)));
    }

    /// <summary>
    /// Decrease all attack desires in array 
    /// </summary>
    private void DecayAttackDesires()
    {
        lock (_lockObject)
        {
            for (int i = 0; i < _desireCount; i++)
            {
                if (_desires[i]?.Type == Desire.AttackDesire)
                {
                    _desires[i].Priority -= _npcAi.GetDefaultNpc().Attack_DecayRatio;
                    if (_desires[i].Priority <= 0)
                    {
                        RemoveDesireAt(i);
                        i--;
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Remove Desire in Array
    /// </summary>
    /// <param name="index"></param>
    private void RemoveDesireAt(int index)
    {
        lock (_lockObject)
        {
            for (int i = index; i < _desireCount - 1; i++)
            {
                _desires[i] = _desires[i + 1];
            }
            _desires[--_desireCount] = null;
        }
    }
     
    /// <summary>
    /// Start Processing Desires of NPC
    /// </summary>
    public void StartProcessingDesires()
    {
        if (_isProcessingDesires)
        {
            LoggerManager.Info("Desire processing is already running.");
            return;
        }

        _isProcessingDesires = true;
        _desireProcessingCancellationTokenSource = new CancellationTokenSource();
        var token = _desireProcessingCancellationTokenSource.Token;

        TaskManagerScheduler.ScheduleAtFixedRate(async () =>
        {
            await ProcessTopDesire();
        }, 0, 1000, token);
    }

    /// <summary>
    /// StopProcessingDesires and Stop Decay Process
    /// </summary>
    public void StopProcessingDesires()
    {
        _desireProcessingCancellationTokenSource?.Cancel();
        _desireProcessingCancellationTokenSource = null;
        _isProcessingDesires = false;
        StopDecayProcess();
    }

    /// <summary>
    /// Process Desire in TOP of Array
    /// </summary>
    private async Task ProcessTopDesire()
    {
        var topDesire = GetTopDesire();
        if (topDesire == null)
        {
            return;
        }
        switch (topDesire.Type)
        {
            case Desire.MoveAroundDesire:
                await HandleMoveAroundDesire(topDesire);
                break;
            case Desire.EffectDesire:
                await HandleEffectDesire(topDesire);
                break;
            case Desire.AttackDesire:
                await HandleAttackDesire(topDesire);
                break;
            case Desire.MoveToDesire:
                await HandleMoveToDesire(topDesire);
                break;
        }
        //LoggerManager.Info($"Processing desire: {topDesire.Type} on {topDesire.Target} with priority {topDesire.Priority}");
        if (topDesire.Type != Desire.AttackDesire)
        {
            RemoveDesireAt(0);
        }
    }

    /// <summary>
    /// HandleAttackDesire
    /// </summary>
    /// <param name="topDesire"></param>
    private async Task HandleAttackDesire(DesireObject topDesire)
    {
        _npcInstance.CharacterDesire().AddDesire(Desire.AttackDesire, topDesire.Target);
        await _npcInstance.SendToKnownPlayers(new NpcInfo(_npcInstance));
        _npcInstance.CharacterMovement().CharacterMovementStatus().SetGroundHigh();
        await _npcInstance.SendToKnownPlayers(new ChangeMoveType(_npcInstance));
    }

    /// <summary>
    /// HandleEffectDesire
    /// </summary>
    /// <param name="topDesire"></param>
    private async Task HandleEffectDesire(DesireObject topDesire)
    {
        if (_npcInstance.CharacterDesire().GetDesire() == Desire.AttackDesire)
        {
            await SetGroundLow();
            StopDecayProcess();
            await _npcInstance.CharacterDesire().StopAutoAttackAndAbortAsync();
        }
        await _npcInstance.CharacterMovement().StopMoveAsync(_character.WorldObjectPosition().WorldPosition());
        await _npcInstance.SendToKnownPlayers(new SocialAction(_npcInstance.ObjectId, topDesire.ActionId));
        //start no desire when social action finished 
        TaskManagerScheduler.Schedule(() =>
        {
            _npcInstance.NpcAi().NoDesire();
        }, topDesire.TimeDesire * 1000);
    }

    /// <summary>
    /// HandleMoveToDesire
    /// </summary>
    /// <param name="topDesire"></param>
    private async Task HandleMoveAroundDesire(DesireObject topDesire)
    {
        if (_npcInstance.CharacterDesire().GetDesire() == Desire.AttackDesire)
        {
            if (topDesire.Type == Desire.AttackDesire)
            {
                RemoveDesireAt(0);
            }
            StopDecayProcess();
            await _npcInstance.CharacterDesire().StopAutoAttackAndAbortAsync();
        }

        if (_npcInstance.CharacterDesire().GetDesire() == Desire.IdleDesire)
        {
            await SetGroundLow();
            if (Rnd.Next(100) < 30)
            {
                var x1 = (_npcInstance.SpawnX + Rnd.Next(300 * 2)) - 300;
                var y1 = (_npcInstance.SpawnY + Rnd.Next(300 * 2)) - 300;
                var z1 = _npcInstance.SpawnZ;
                await _npcInstance.NpcDesire().MoveToAsync(x1, y1, z1);
            }
        }
    }
    
    private async Task HandleMoveToDesire(DesireObject topDesire)
    {
        if (_npcInstance.CharacterDesire().GetDesire() == Desire.AttackDesire)
        {
            if (topDesire.Type == Desire.AttackDesire)
            {
                RemoveDesireAt(0);
            }
            StopDecayProcess();
            await _npcInstance.CharacterDesire().StopAutoAttackAndAbortAsync();
        }

        if (_npcInstance.CharacterDesire().GetDesire() == Desire.IdleDesire)
        {
            await SetGroundLow();
            if (Rnd.Next(100) < 30)
            {
                var x1 = (_npcInstance.SpawnX + Rnd.Next(300 * 2)) - 300;
                var y1 = (_npcInstance.SpawnY + Rnd.Next(300 * 2)) - 300;
                var z1 = _npcInstance.SpawnZ;
                await _npcInstance.NpcDesire().MoveToAsync(x1, y1, z1);
            }
        }
    }

    /// <summary>
    /// Set Walking of NPC
    /// </summary>
    private async Task SetGroundLow()
    {
        if (_npcInstance.CharacterMovement().CharacterMovementStatus().IsGroundHigh())
        {
            _npcInstance.CharacterMovement().CharacterMovementStatus().SetGroundLow();
            await _npcInstance.SendToKnownPlayers(new ChangeMoveType(_npcInstance));
        }
    }

    /// <summary>
    /// Get Desire in TOP of array
    /// </summary>
    /// <returns></returns>
    public DesireObject GetTopDesire()
    {
        lock (_lockObject)
        {
            return _desireCount > 0 ? _desires[0] : null;
        }
    }

    /// <summary>
    /// Start Decay Process when NPC was Attacked
    /// </summary>
    /// <returns></returns>
    public Task StartDecayProcess()
    {
        _decayCancellationTokenSource = new CancellationTokenSource();
        var token = _decayCancellationTokenSource.Token;
        _decayTask = TaskManagerScheduler.ScheduleAtFixedRate(DecayAttackDesires, 0, 3300, token);
        return _decayTask;
    }

    /// <summary>
    /// Stop Decay Process when desire of attack player is disappeared
    /// </summary>
    public void StopDecayProcess()
    {
        lock (_lockObject)
        {
            for (int i = 0; i < _desireCount; i++)
            {
                if (_desires[i]?.Type == Desire.AttackDesire)
                {
                    RemoveDesireAt(i);
                    i--;
                }
            }
            _decayCancellationTokenSource?.Cancel();
            _decayCancellationTokenSource = null;
        }
    }
}