using System.Threading.Tasks;

namespace Core.NetworkPacket.ServerPacket
{
    /// <summary>
    /// TODO not implemented
    /// </summary>
    internal sealed class SkillCoolTime : Network.ServerPacket
    {
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xc1);
        }
    }
}