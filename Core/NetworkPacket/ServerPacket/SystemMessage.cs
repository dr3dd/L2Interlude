using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class SystemMessage : Network.ServerPacket
    {
        private readonly List<object[]> _data = new List<object[]>();
        public readonly int MessageId;

        public SystemMessage(SystemMessageId msgId)
        {
            MessageId = (int)msgId;
        }

        public SystemMessage(string message)
        {
            AddString(message);
        }

        public SystemMessage AddString(string val)
        {
            _data.Add(new object[] { 0, val });
            return this;
        }

        public SystemMessage AddNumber(int val)
        {
            _data.Add(new object[] { 1, val });
            return this;
        }

        public SystemMessage AddNumber(double val)
        {
            _data.Add(new object[] { 1, (int)val });
            return this;
        }

        public SystemMessage AddNpcName(int val)
        {
            _data.Add(new object[] { 2, 1000000 + val });
            return this;
        }

        public SystemMessage AddItemName(int val)
        {
            _data.Add(new object[] { 3, val });
            return this;
        }

        public SystemMessage AddSkillName(int val, int lvl)
        {
            _data.Add(new object[] { 4, val, lvl });
            return this;
        }

        public void AddCastleName(int val)
        {
            _data.Add(new object[] { 5, val });
        }

        public void AddItemCount(int val)
        {
            _data.Add(new object[] { 6, val });
        }

        public void AddZoneName(int val, int y, int z)
        {
            _data.Add(new object[] { 7, val, y, z });
        }

        public void AddElementName(int val)
        {
            _data.Add(new object[] { 9, val });
        }

        public void AddInstanceName(int val)
        {
            _data.Add(new object[] { 10, val });
        }

        public SystemMessage AddPlayerName(string val)
        {
            _data.Add(new object[] { 12, val });
            return this;
        }

        public void AddSysStr(int val)
        {
            _data.Add(new object[] { 13, val });
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x64);
            await WriteIntAsync(MessageId);
            await WriteIntAsync(_data.Count);

            foreach (object[] d in _data)
            {
                int type = (int)d[0];

                await WriteIntAsync(type);

                switch (type)
                {
                    case 0: //text
                    case 12:
                        await WriteStringAsync((string)d[1]);
                        break;
                    case 1: //number
                    case 2: //npcid
                    case 3: //itemid
                    case 5:
                    case 9:
                    case 10:
                    case 13:
                        await WriteIntAsync((int)d[1]);
                        break;
                    case 4: //skillname
                        await WriteIntAsync((int)d[1]);
                        await WriteIntAsync((int)d[2]);
                        break;
                    case 6:
                        await WriteLongAsync((long)d[1]);
                        break;
                    case 7: //zone
                        await WriteIntAsync((int)d[1]);
                        await WriteIntAsync((int)d[2]);
                        await WriteIntAsync((int)d[3]);
                        break;
                }
            }
        }

        
    }
}