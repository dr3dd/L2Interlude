using System.Threading.Tasks;
using Core.Module.Player;
using L2Logger;
using static System.Net.Mime.MediaTypeNames;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class NpcHtmlMessage : Network.ServerPacket
    {
        private readonly int _npcObjId;
	
        private string _html;

        public NpcHtmlMessage(int npcObjId, string text)
        {
            _npcObjId = npcObjId;
            _html = text;
            SetHtml(text);
        }
        
        public NpcHtmlMessage(int npcObjId)
        {
            _npcObjId = npcObjId;
        }

        public NpcHtmlMessage(PlayerInstance player, string fileName, int objId)
        {
            var html = Initializer.HtmlCacheInit().GetHtmlText(fileName);
            _npcObjId = objId;
            SetHtml(html);
        }

        private void SetHtml(string text)
        {
            if (text == null)
            {
                LoggerManager.Warn("Html is null! this will crash the client!");
                _html = "<html><body></body></html>";
                return;
            }
		
            if (text.Length > 8192)
            {
                LoggerManager.Warn("Html is too long! this will crash the client!");
                _html = "<html><body>Html was too long,<br>Try to use DB for this action</body></html>";
                return;
            }
            //Replace("\r\n", "\n");
            //Replace("(?s)<!--.*?-->", ""); // Remove html comments
            _html = text; // html code must not exceed 8192 bytes
        }

        private string GetContent()
        {
            var content = _html;
            content = content.Replace("teleport_request", "teleport_request#" + _npcObjId); //add ObjectId
            content = content.Replace("menu_select", "menu_select#" + _npcObjId); //add ObjectId
            content = content.Replace("learn_skill", "learn_skill#" + _npcObjId); //add ObjectId
            content = content.Replace("talk_select", "talk_select#" + _npcObjId); //add ObjectId
            content = content.Replace("quest_accept", "quest_accept#" + _npcObjId); //add ObjectId
            return content;
        }
        
        public void Replace(string pattern, string value)
        {
            _html = _html.Replace(pattern, value);
        }
	
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x0f);
            await WriteIntAsync(_npcObjId);
            await WriteStringAsync(GetContent());
            await WriteIntAsync(0x00);
        }
    }
}