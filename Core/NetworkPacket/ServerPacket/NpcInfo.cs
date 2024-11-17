using System;
using System.Threading.Tasks;
using Core.Module.NpcData;

namespace Core.NetworkPacket.ServerPacket
{
    public class NpcInfo : Network.ServerPacket
    {
        private readonly NpcInstance _npcInstance;
        private readonly int _highSpeed;
        private readonly int _lowSpeed;
        private readonly int _swimRunSpd;
        private readonly int _swimWalkSpd;
        private readonly double _moveMultiplier;
        private readonly int _flyRunSpd;
        private readonly int _flyWalkSpd;
        private readonly bool _isHighSpeed;
        public NpcInfo(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _moveMultiplier = _npcInstance.CharacterCombat().GetMovementSpeedMultiplier();
            _highSpeed = (int) Math.Round(_npcInstance.CharacterCombat().GetHighSpeed() / _moveMultiplier);
            _lowSpeed = (int) Math.Round(_npcInstance.CharacterCombat().GetLowSpeed() / _moveMultiplier);
            _swimRunSpd = (int) Math.Round(_npcInstance.CharacterCombat().GetHighSpeed() / _moveMultiplier);
            _swimWalkSpd = (int) Math.Round(_npcInstance.CharacterCombat().GetLowSpeed() / _moveMultiplier);
            _flyRunSpd = 0;
            _flyWalkSpd = 0;
            var movementStatus = _npcInstance.CharacterMovement().CharacterMovementStatus();
            _isHighSpeed = movementStatus.IsGroundHigh();
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x16);
            await WriteIntAsync(_npcInstance.ObjectId);
            await WriteIntAsync(_npcInstance.NpcHashId); //_npcInstance.NpcHashId
            await WriteIntAsync(_npcInstance.GetTemplate().GetStat().CanBeAttacked); //_npcInstance.Attackable
            await WriteIntAsync(_npcInstance.GetX());
            await WriteIntAsync(_npcInstance.GetY());
            await WriteIntAsync(_npcInstance.GetZ());
            await WriteIntAsync(_npcInstance.Heading);
            await WriteIntAsync(0x00);
            
            int pAtkSpd = _npcInstance.CharacterCombat().GetPhysicalAttackSpeed();
            int mAtkSpd = _npcInstance.CharacterCombat().GetMagicalAttackSpeed();

            await WriteIntAsync(mAtkSpd);
            await WriteIntAsync(pAtkSpd);
            await WriteIntAsync(_highSpeed);
            await WriteIntAsync(_lowSpeed);
            await WriteIntAsync(_swimRunSpd); // swimspeed (int) Math.round(creature.getSwimRunSpeed() / _moveMultiplier);
            await WriteIntAsync(_swimWalkSpd); // swimspeed (int) Math.round(creature.getSwimWalkSpeed() / _moveMultiplier);
            await WriteIntAsync(_flyRunSpd);
            await WriteIntAsync(_flyWalkSpd);
            await WriteIntAsync(_flyRunSpd);
            await WriteIntAsync(_flyWalkSpd);
            
            await WriteDoubleAsync(_moveMultiplier);
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetAttackSpeedMultiplier());
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionRadius());
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionHeight());
            await WriteIntAsync(0); // _npcInstance.Template.Stat.RHand right hand weapon
            await WriteIntAsync(0);
            await WriteIntAsync(0); // _npcInstance.Template.Stat.LHand left hand weapon
            await WriteByteAsync(1); // name above char 1=true ... ??
            await WriteByteAsync(_isHighSpeed ? 0x01 : 0x00); //(_npcInstance.Movement().IsRunning()) ? 1 : 0
            await WriteByteAsync(0); //_npc.isInCombat() ? 1 : 0
            await WriteByteAsync(0); //_npc.IsAlikeDead()
            await WriteByteAsync(0); // _npc.Summoned ? 2 : 0 invisible ?? 0=false  1=true   2=summoned (only works if model has a summon animation)
            await WriteStringAsync(""); //_npcInstance.GetTemplate().GetStat().Name
            await WriteStringAsync("NPC_ID: " + _npcInstance.GetTemplate().GetStat().Id + " OBJ_ID: " + _npcInstance.ObjectId); //_npcInstance.Template.Stat.Title
            await WriteIntAsync(0x00); // Title color 0=client default
            await WriteIntAsync(0x00); //pvp flag
            await WriteIntAsync(0x00); // karma
            
            await WriteIntAsync(0); //_creature.getAbnormalEffect()
            await WriteIntAsync(0);//_npc.ClanId
            await WriteIntAsync(0);//_npc.ClanCrestId
            await WriteIntAsync(0);//_npc.AllianceId
            await WriteIntAsync(0);//_npc.AllianceCrestId
            await WriteByteAsync(0); // _npc.IsFlying() ? 2 : 0 C2

            await WriteByteAsync(0); //_npc.TeamId
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionRadius());
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionHeight());
            await WriteIntAsync(0); // enchant
            await WriteIntAsync(0); //_npc.IsFlying() ? 1 : 0 C6
        }
    }
}