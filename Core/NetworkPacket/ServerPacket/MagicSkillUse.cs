using System.Threading.Tasks;
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

        public MagicSkillUse(Character character, Character target, int skillId, int skillLevel, float hitTime, float reuseDelay)
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
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x48);
            await WriteIntAsync(_objectId);
            await WriteIntAsync(_targetId);
            await WriteIntAsync(_skillId);
            await WriteIntAsync(_skillLevel);
            await WriteIntAsync(_hitTime);
            await WriteIntAsync(_reuseDelay);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
            await WriteShortAsync(0x00); // unknown loop but not AoE
            // for()
            // {
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
        }
    }
}