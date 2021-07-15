using Core.Controller;
using Network;

namespace Core.Module.GameService.Response
{
    class KeyPacket : ServerPacket
    {
        private readonly byte[] _key;
        private byte _next;

        public KeyPacket(GameServiceController controller, byte n)
        {
            _key = controller.EnableCrypt();
            _next = n;
        }

        public override void Write()
        {
            WriteByte(0x00);
            WriteByte(0x01);
            WriteBytesArray(_key);
            WriteInt(0x01);
            WriteInt(0x01);
        }
    }
}