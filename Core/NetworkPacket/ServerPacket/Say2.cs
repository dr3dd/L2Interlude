using Core.Module.CharacterData;
using Core.Module.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 11.08.2024 0:13:14

namespace Core.NetworkPacket.ServerPacket
{
    public class Say2 : Network.ServerPacket
    {
        private readonly Character _sender;
        private int type;
        private int objectId;
        private string name;
        private string text;
        private int stringMsgId = -1;
        private int mask;
        private List<string> parameters = new List<string>();
        public Say2(Character sender, ChatType messageType, string message)
        {
            _sender = sender;
            name = sender == null ? "" : _sender.CharacterName;
            objectId = sender == null ? 0 : _sender.ObjectId;
            type = (int)messageType;
            text = message;
            stringMsgId = -1;
        }

        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x4A);
            await WriteIntAsync(_sender == null ? 0 : _sender.ObjectId);
            await WriteIntAsync(type);
            if (name != null)
            {
                await WriteStringAsync(name);
            }
            else
            {
                await WriteIntAsync(objectId);
            }
            await WriteIntAsync(stringMsgId); // NPCString ID
            if (text != null)
            {
                await WriteStringAsync(text);
            }
            else if (parameters.Count > 0)
            {
                foreach (string s in parameters)
                {
                    await WriteStringAsync(s);
                }
            }
        }

        /// <summary>
        /// parameter for argument S1,S2,.. in npcstring-e.dat
        /// </summary>
        /// <param name="text"></param>
        public void addStringParameter(string text)
        {
            parameters.Add(text);
        }
    }
}
