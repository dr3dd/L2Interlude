using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class MagicSkillUse : Network.ServerPacket
    {
        private readonly int _targetId;
        private readonly int _skillId;
        private readonly int _skillLevel;
        private readonly float _hitTime;
        private readonly float _reuseDelay;
        private readonly int _objectId;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;

        public MagicSkillUse(Character character, PlayerInstance target, int skillId, int skillLevel, float hitTime, float reuseDelay)
        {
            _objectId = character.ObjectId;
            _targetId = target.ObjectId;
            _skillId = skillId;
            _skillLevel = skillLevel;
            _hitTime = hitTime;
            _reuseDelay = reuseDelay;
            _x = character.GetX();
            _y = character.GetY();
            _z = character.GetZ();
        }
        public override void Write()
        {
            WriteByte(0x48);
            WriteInt(_objectId);
            WriteInt(_targetId);
            WriteInt(_skillId);
            WriteInt(_skillLevel);
            WriteInt(_hitTime);
            WriteInt(_reuseDelay);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
            WriteShort(0x00); // unknown loop but not AoE
            // for()
            // {
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
        }
    }
}