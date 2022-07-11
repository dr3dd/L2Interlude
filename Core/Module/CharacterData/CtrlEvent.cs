namespace Core.Module.CharacterData
{
    public enum CtrlEvent
    {
        /**
		 * Something has changed, usually a previous step has being completed or maybe was completed, the AI must thing on next action
		 */
		EvtThink,
		
		/**
		 * The actor was attacked. This event comes each time a physical or magical attack was done on the actor. NPC may start attack in responce, or ignore this event if they already attack someone, or change target and so on.
		 */
		EvtAttacked,
		
		/** Increase/decrease aggression towards a target, or reduce global aggression if target is null */
		EvtAggression,
		
		/** Actor is in stun state */
		EvtStunned,
		
		/** Actor starts/stops sleeping */
		EvtSleeping,
		
		/** Actor is in rooted state (cannot move) */
		EvtRooted,
		
		/**
		 * An event that previous action was completed. The action may be an attempt to physically/magically hit an enemy, or an action that discarded attack attempt has finished.
		 */
		EvtReadyToAct,
		
		/**
		 * User's command, like using a combat magic or changing weapon, etc. The command is not intended to change final goal
		 */
		EvtUserCmd,
		
		/**
		 * The actor arrived to assigned location, or it's a time to modify movement destination (follow, interact, random move and others intentions).
		 */
		EvtArrived,
		
		/**
		 * The actor arrived to an intermidiate point, and needs revalidate destination. This is sent when follow/move to pawn if destination is far away.
		 */
		EvtArrivedRevalidate,
		
		/** The actor cannot move anymore. */
		EvtArrivedBlocked,
		
		/** Forgets an object (if it's used as attack target, follow target and so on */
		EvtForgetObject,
		
		/**
		 * Attempt to cancel current step execution, but not change the intention. For example, the actor was putted into a stun, so it's current attack or movement has to be canceled. But after the stun state expired, the actor may try to attack again. Another usage for CANCEL is a user's attempt to
		 * cancel a cast/bow attack and so on.
		 */
		EvtCancel,
		
		/** The creature is dead */
		EvtDead,
		
		/** The creature looks like dead */
		EvtFakeDeath,
		
		/** The creature attack anyone randomly **/
		EvtConfused,
		
		/** The creature cannot cast spells anymore **/
		EvtMuted,
		
		/** The creature flee in randoms directions **/
		EvtAffraid,
		
		/** The creature finish casting **/
		EvtFinishCasting,
		
		/** The creature betrayed its master */
		EvtBetrayed
    }
}