using System.Threading.Tasks;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.WorldData;
using L2Logger;

namespace Core.Module.CharacterData
{
    public class CharacterDesire : AbstractDesire
    {
        public CharacterDesire(Character character) : base(character)
        {
        }
        
        protected override async Task MoveToDesireAsync(Location destination)
        {
            if (GetDesire() == Desire.RestDesire)
            {
                // Cancel action client side by sending Server->Client packet ActionFailed to the PlayerInstance actor
                await ClientActionFailedAsync();
                return;
            }
            ChangeDesire(Desire.MoveToDesire);
            // Stop the actor auto-attack client side by sending Server->Client packet AutoAttackStop (broadcast)
            await ClientStopAutoAttackAsync();
            await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
        }

        protected override Task CastDesireAsync(SkillDataModel arg0)
        {
            throw new System.NotImplementedException();
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

        protected override async Task DesireAttackAsync(Character target)
        {
            if (target == null)
            {
                await ClientActionFailedAsync();
                return;
            }
            if (GetDesire() == Desire.RestDesire)
            {
                // Cancel action client side by sending Server->Client packet ActionFailed to the PlayerInstance actor
                await ClientActionFailedAsync();
                return;
            }
            // Check if the Intention is already AI_INTENTION_ATTACK
            if (GetDesire() == Desire.AttackDesire)
            {
                // Check if the AI already targets the Creature
                    // Set the AI attack target (change target)
                    AttackTarget = target;
				
                    StopFollow();
				
                    // Launch the Think Event
                    _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtThink, null);

            }
            else
            {
                // Set the Intention of this AbstractAI to AI_INTENTION_ATTACK
                ChangeDesire(Desire.AttackDesire);
			
                // Set the AI attack target
                AttackTarget = target;
			
                StopFollow();

                if (_character is PlayerInstance instance)
                {
                    await instance.PlayerMessage().SendMessageAsync("DEBUG: Hello dr3dd");
                }
			
                // Launch the Think Event
                _character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtThink);
            }
        }

        protected override async Task DesireActiveAsync()
        {
            //LoggerManager.Info("CharacterAi: OnIntentionActive");
            // Check if the Intention is not already Active
            if (_character.CharacterDesire().GetDesire() != Desire.ActiveDesire)
            {
                // Set the AI Intention to AI_INTENTION_ACTIVE
                _character.CharacterDesire().ChangeDesire(Desire.ActiveDesire);
			
                // Init cast and attack target
                AttackTarget = null;
			
                // Stop the actor movement server side AND client side by sending Server->Client packet StopMove/StopRotation (broadcast)
                await _character.CharacterDesire().ClientStopMovingAsync(null);
			
                // Stop the actor auto-attack client side by sending Server->Client packet AutoAttackStop (broadcast)
                await ClientStopAutoAttackAsync();
			
                // Launch the Think Event
                await _character.CharacterNotifyEvent().OnEvtThinkAsync();
            }
        }
    }
}