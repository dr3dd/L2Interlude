using Core.Module.ItemData;
using Core.Module.NpcData;
using Core.Module.Player;

namespace Core.Module.CharacterData.PhysicalAttack
{
    public class GeneralCharacterPhysicalAttackValidator : ICharacterPhysicalAttackValidator
    {
        private readonly Character _character;
        public GeneralCharacterPhysicalAttackValidator(Character character)
        {
            _character = character;
        }
        public bool IsValid(Character target)
        {
            if (IsAlikeDead()) return false;
            if (IsCharacterDead(target)) return false;
            if (IsKnownListEmpty(target)) return false;
            if (IsAttackingDisabled()) return false;
            if (IsAttackingSelf(target)) return false;
            if (IsAlreadyAttacking()) return false;
            return true;
        }

        public bool IsValidBowAttack(WeaponType weapon)
        {
            if (IsNotEnoughArrows()) return false;
            if (IsNotEnoughMana()) return false;
            if (IsDisabledBowAttack()) return false;
            return true;
        }

        private bool IsNotEnoughMana()
        {
            return false;
        }

        private bool IsNotEnoughArrows()
        {
            // Cancel the action because the PlayerInstance have no arrow
            //_character.AI.SetIntention(CtrlIntention.AiIntentionIdle);
            //_character.SendActionFailedPacket();
            //_character.SendPacket(new SystemMessage(SystemMessageId.NotEnoughArrows));
            return false;
        }

        private bool IsDisabledBowAttack()
        {
            return true;
        }

        private bool IsAlikeDead()
        {
            return false;
        }

        /// <summary>
        /// If PlayerInstance is dead or the target is dead, the action is stopped
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsCharacterDead(Character target)
        {
            return false;
        }

        private bool IsKnownListEmpty(Character target)
        {
            return false;
        }

        private bool IsAttackingDisabled()
        {
            return false;
        }

        private bool IsAttackingSelf(Character target)
        {
            return _character.ObjectId == target.ObjectId;
        }

        private bool IsAlreadyAttacking()
        {
            return _character.PhysicalAttack().IsAttackingNow();
        }
    }
}