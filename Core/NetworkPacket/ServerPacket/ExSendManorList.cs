using System.Collections.Generic;

namespace Core.NetworkPacket.ServerPacket
{
    public class ExSendManorList : Network.ServerPacket
    {
        private readonly List<string> _list;

        public ExSendManorList(List<string> list)
        {
            _list = list;
        }
        public override void Write()
        {
            WriteByte(0xFE);
            WriteShort(0x1B);
            WriteInt(_list.Count);

            int id = 1;
            foreach (string manor in _list)
            {
                WriteInt(id);
                id++;
                WriteString(manor);
            }
        }
    }
}