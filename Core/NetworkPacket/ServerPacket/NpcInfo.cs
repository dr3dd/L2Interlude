using Core.Module.NpcData;

namespace Core.NetworkPacket.ServerPacket
{
    public class NpcInfo : Network.ServerPacket
    {
        private readonly NpcInstance _npcInstance;
        public NpcInfo(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }
        public override void Write()
        {
            WriteByte(0x16);
            WriteInt(_npcInstance.ObjectId);
            WriteInt(_npcInstance.NpcHashId); //_npcInstance.NpcHashId
            WriteInt(_npcInstance.GetTemplate().GetStat().CanBeAttacked); //_npcInstance.Attackable
            WriteInt(_npcInstance.GetX());
            WriteInt(_npcInstance.GetY());
            WriteInt(_npcInstance.GetZ());
            WriteInt(_npcInstance.Heading);
            WriteInt(0x00);
            
            int runSpeed = 85;//_npcInstance.GetTemplate().GetStat().Stat.GetRunSpeed()
            int walkSpeed = 60; //_npcInstance.Stat.GetWalkSpeed();
            int pAtkSpd = 10; //_npcInstance.Stat.GetPAtkSpd();
            int mAtkSpd = 10; //_npcInstance.Stat.GetMAtkSpd();

            WriteInt(mAtkSpd);
            WriteInt(pAtkSpd);
            WriteInt(runSpeed);
            WriteInt(walkSpeed);
            WriteInt(0); // swimspeed
            WriteInt(0); // swimspeed
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            
            WriteDouble(1.1);
            WriteDouble(pAtkSpd / 277.478340719);
            WriteDouble(_npcInstance.GetTemplate().GetStat().CollisionRadius[0]);
            WriteDouble(_npcInstance.GetTemplate().GetStat().CollisionHeight[0]);
            WriteInt(0); // _npcInstance.Template.Stat.RHand right hand weapon
            WriteInt(0);
            WriteInt(0); // _npcInstance.Template.Stat.LHand left hand weapon
            WriteByte(1); // name above char 1=true ... ??
            WriteByte(0); //(_npcInstance.Movement().IsRunning()) ? 1 : 0
            WriteByte(0); //_npc.isInCombat() ? 1 : 0
            WriteByte(0); //_npc.IsAlikeDead()
            WriteByte(0); // _npc.Summoned ? 2 : 0 invisible ?? 0=false  1=true   2=summoned (only works if model has a summon animation)
            WriteString(""); //_npcInstance.GetTemplate().GetStat().Name
            WriteString("NPC_ID: " + _npcInstance.GetTemplate().GetStat().Id); //_npcInstance.Template.Stat.Title
            WriteInt(0x00); // Title color 0=client default
            WriteInt(0x00); //pvp flag
            WriteInt(0x00); // karma
            
            WriteInt(0); //_creature.getAbnormalEffect()
            WriteInt(0);//_npc.ClanId
            WriteInt(0);//_npc.ClanCrestId
            WriteInt(0);//_npc.AllianceId
            WriteInt(0);//_npc.AllianceCrestId
            WriteByte(0); // _npc.IsFlying() ? 2 : 0 C2

            WriteByte(0); //_npc.TeamId
            WriteDouble(_npcInstance.GetTemplate().GetStat().CollisionRadius[0]);
            WriteDouble(_npcInstance.GetTemplate().GetStat().CollisionHeight[0]);
            WriteInt(0); // enchant
            WriteInt(0); //_npc.IsFlying() ? 1 : 0 C6
        }
    }
}