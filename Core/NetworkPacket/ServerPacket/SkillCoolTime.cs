namespace Core.NetworkPacket.ServerPacket
{
    /// <summary>
    /// TODO not implemented
    /// </summary>
    internal sealed class SkillCoolTime : Network.ServerPacket
    {
        public override void Write()
        {
            WriteByte(0xc1);
        }
    }
}