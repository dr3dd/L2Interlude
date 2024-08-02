using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.Module.WorldData;
using L2Logger;

namespace Core.Module.CharacterData;

public class CharacterDesire : AbstractDesire
{
    private readonly CharacterDesireCast _characterDesireCast;
    public CharacterDesireCast DesireCast() => _characterDesireCast;
    public CharacterDesire(Character character) : base(character)
    {
        _characterDesireCast = new CharacterDesireCast(character);
    }

    protected override async Task MoveToDesireAsync(Location destination)
    {
        if (IsRestingOrCasting())
        {
            // Cancel action client side by sending Server->Client packet ActionFailed to the PlayerInstance actor
            await ClientActionFailedAsync();
            return;
        }
        ChangeDesire(Desire.MoveToDesire);
        await StopAutoAttackAndAbortAsync();
        await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
    }

    /// <summary>
    /// IsRestingOrCasting
    /// </summary>
    /// <returns></returns>
    private bool IsRestingOrCasting()
    {
        return GetDesire() == Desire.RestDesire || _characterDesireCast.IsCastingNow();
    }
    
    /// <summary>
    /// Stop the actor auto-attack client side by sending Server->Client packet AutoAttackStop (broadcast)
    /// Abort the attack of the Creature and send Server->Client ActionFailed packet
    /// </summary>
    private async Task StopAutoAttackAndAbortAsync()
    {
        await ClientStopAutoAttackAsync();
        await AbortAttackAsync();
    }

    /// <summary>
    /// AbortAttackAsync
    /// </summary>
    private async Task AbortAttackAsync()
    {
        await _character.PhysicalAttack().AbortAttackAsync();
    }

    /// <summary>
    /// CastDesireAsync
    /// </summary>
    /// <param name="skill"></param>
    protected override async Task CastDesireAsync(SkillDataModel skill)
    {
        ChangeDesire(Desire.CastDesire);
        await _characterDesireCast.DoCastAsync(skill);
    }

    protected override Task IntentionInteractAsync(WorldObject worldObject)
    {
        throw new System.NotImplementedException();
    }


    public void HandleException(Task obj)
    {
        if (obj.IsFaulted)
        {
            LoggerManager.Error(GetType().Name + ": " + obj.Exception);
        }
    }

    /// <summary>
    /// DesireAttackAsync
    /// </summary>
    /// <param name="target"></param>
    protected override async Task DesireAttackAsync(Character target)
    {
        if (target == null || IsRestingOrCasting())
        {
            await ClientActionFailedAsync();
            return;
        }
        // Check if the Intention is already AI_INTENTION_ATTACK
        if (IsAlreadyAttacking())
        {
            UpdateAttackTarget(target);
            return;
        }
        InitiateAttack(target);
    }
    
    private bool IsAlreadyAttacking()
    {
        return GetDesire() == Desire.AttackDesire;
    }
    
    /// <summary>
    /// Set the AI attack target (change target)
    /// </summary>
    /// <param name="target"></param>
    private void UpdateAttackTarget(Character target)
    {
        AttackTarget = target;
        StopFollow();
        _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtThink);
    }
    
    private void InitiateAttack(Character target)
    {
        ChangeDesire(Desire.AttackDesire);
        AttackTarget = target;
        StopFollow();
        _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtThink);
    }
    
    private bool IsActive()
    {
        return _character.CharacterDesire().GetDesire() == Desire.ActiveDesire;
    }
    
    /// <summary>
    /// Set the AI Intention to AI_INTENTION_ACTIVE
    /// </summary>
    private void ChangeToActiveDesire()
    {
        _character.CharacterDesire().ChangeDesire(Desire.ActiveDesire);
    }
    
    private void ResetTargets()
    {
        AttackTarget = null;
    }
    
    private async Task StopMovingAndAutoAttackAsync()
    {
        await _character.CharacterDesire().ClientStopMovingAsync(null);
        await ClientStopAutoAttackAsync();
    }

    protected override async Task DesireActiveAsync()
    {
        //LoggerManager.Info("CharacterAi: OnIntentionActive");
        // Check if the Intention is not already Active
        if (!IsActive())
        {
            ChangeToActiveDesire();
            ResetTargets();
            await StopMovingAndAutoAttackAsync();
            await _character.CharacterNotifyEvent().OnEvtThinkAsync();
            // Launch the Think Event
            await _character.CharacterNotifyEvent().OnEvtThinkAsync();
        }
    }
}