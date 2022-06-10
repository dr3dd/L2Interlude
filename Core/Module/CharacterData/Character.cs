using Core.Module.WorldData;

namespace Core.Module.CharacterData
{
    public abstract class Character : WorldObject
    {
        private readonly CharacterStatus _characterStatus;
        private readonly CharacterEffect _characterEffect;
        public abstract int GetMaxHp();
        public abstract int GetMagicalAttack();
        public abstract int GetMagicalDefence();
        public abstract int GetPhysicalDefence();
        public CharacterStatus CharacterStatus() => _characterStatus;
        public CharacterEffect CharacterEffect() => _characterEffect;
        protected Character()
        {
            _characterStatus = new CharacterStatus(this);
            _characterEffect = new CharacterEffect(this);
        }

    }
}