using System;
using System.Threading.Tasks;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.CharacterData
{
    public abstract class CharacterNotifyEventAbstract
    {
        private bool _clientAutoAttacking;
        public abstract Task OnEvtThinkAsync();
        public abstract Task OnEvtAttackedAsync(Character arg0);
        public abstract Task OnEvtArrivedRevalidate();
        public abstract Task OnEvtReadyToAct();
        public abstract Task OnEvtArrivedAsync();
        public abstract Task OnEvtArrivedBlockedAsync(Location arg0);
        public abstract Task OnEvtDeadAsync();

        protected readonly Character _character;

        protected CharacterNotifyEventAbstract(Character character)
        {
            _character = character;
        }

        public void NotifyEvent(CtrlEvent evt, object arg0 = null, object arg1 = null)
        {
            switch (evt)
            {
                case CtrlEvent.EvtThink:
                    Task.Run(OnEvtThinkAsync);
                    break;
                case CtrlEvent.EvtAttacked:
                    Task.Run(() => OnEvtAttackedAsync((Character) arg0));
                    break;
                case CtrlEvent.EvtAggression:
                    break;
                case CtrlEvent.EvtArrivedRevalidate:
                    Task.Run(OnEvtArrivedRevalidate);
                    break;
                case CtrlEvent.EvtStunned:
                    break;
                case CtrlEvent.EvtSleeping:
                    break;
                case CtrlEvent.EvtRooted:
                    break;
                case CtrlEvent.EvtReadyToAct:
                    Task.Run(OnEvtReadyToAct);
                    break;
                case CtrlEvent.EvtUserCmd:
                    break;
                case CtrlEvent.EvtArrived:
                    Task.Run(OnEvtArrivedAsync);
                    break;
                case CtrlEvent.EvtArrivedBlocked:
                    Task.Run(() => OnEvtArrivedBlockedAsync((Location) arg0));
                    break;
                case CtrlEvent.EvtForgetObject:
                    break;
                case CtrlEvent.EvtCancel:
                    break;
                case CtrlEvent.EvtDead:
                    Task.Run(OnEvtDeadAsync);
                    break;
                case CtrlEvent.EvtFakeDeath:
                    break;
                case CtrlEvent.EvtConfused:
                    break;
                case CtrlEvent.EvtMuted:
                    break;
                case CtrlEvent.EvtAffraid:
                    break;
                case CtrlEvent.EvtFinishCasting:
                    break;
                case CtrlEvent.EvtBetrayed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt), evt, null);
            }
        }
        
        public async Task ClientStartAutoAttackAsync()
        {
            if (!_clientAutoAttacking)
            {
                // Send a Server->Client packet AutoAttackStart to the actor and all PlayerInstance in its _knownPlayers
                await _character.SendToKnownPlayers(new AutoAttackStart(_character.ObjectId));
                SetAutoAttacking(true);
            }
        }
        
        public void SetAutoAttacking(bool isAutoAttacking)
        {
            _clientAutoAttacking = isAutoAttacking;
        }
    }
}