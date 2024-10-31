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
        private readonly double _moveMultiplier;
        public NpcInfo(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _moveMultiplier = _npcInstance.CharacterCombat().GetMovementSpeedMultiplier();
            _highSpeed = (int) Math.Round(_npcInstance.CharacterCombat().GetHighSpeed() / _moveMultiplier);
            _lowSpeed = (int) Math.Round(_npcInstance.CharacterCombat().GetLowSpeed() / _moveMultiplier);
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
            
            int pAtkSpd = 10; //_npcInstance.Stat.GetPAtkSpd();
            int mAtkSpd = 10; //_npcInstance.Stat.GetMAtkSpd();

            await WriteIntAsync(mAtkSpd);
            await WriteIntAsync(pAtkSpd);
            await WriteIntAsync(_highSpeed);
            await WriteIntAsync(_lowSpeed);
            await WriteIntAsync(0); // swimspeed
            await WriteIntAsync(0); // swimspeed
            await WriteIntAsync(0);
            await WriteIntAsync(0);
            await WriteIntAsync(0);
            await WriteIntAsync(0);
            
            await WriteDoubleAsync(_moveMultiplier);
            await WriteDoubleAsync(pAtkSpd / 277.478340719);
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionRadius());
            await WriteDoubleAsync(_npcInstance.CharacterCombat().GetCollisionHeight());
            await WriteIntAsync(0); // _npcInstance.Template.Stat.RHand right hand weapon
            await WriteIntAsync(0);
            await WriteIntAsync(0); // _npcInstance.Template.Stat.LHand left hand weapon
            await WriteByteAsync(1); // name above char 1=true ... ??
            await WriteByteAsync(0); //(_npcInstance.Movement().IsRunning()) ? 1 : 0
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