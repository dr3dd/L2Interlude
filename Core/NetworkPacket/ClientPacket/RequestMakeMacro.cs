using Core.Controller;
using Core.Module.Player;
using Core.Module.Player.Macroses;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using L2Logger;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 28.08.2024 1:01:19

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestMakeMacro : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly PlayerInstance _playerInstance;
        const int maxMacro = 12;
        private int id;
        private string name;
        private string desc;
        private string acronym;
        private int icon;
        private List<MacrosCmd> commands = new List<MacrosCmd>();
        private int commandsCount;
        private int commandsLength;
        private MacrosModel macros;

        public RequestMakeMacro(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;

            id = packet.ReadInt();
            name = packet.ReadString().Trim();
            desc = packet.ReadString();
            acronym = packet.ReadString();
            icon = packet.ReadByte();
            commandsCount = packet.ReadByte();

            if (commandsCount > maxMacro)
            {
                commandsCount = maxMacro;
            }

            
            for (int i = 0; i < commandsCount; i++)
            {
                int entry = packet.ReadByte();
                int type = packet.ReadByte(); // 1 = skill, 3 = action, 4 = shortcut
                int d1 = packet.ReadInt(); // skill or page number for shortcuts
                int d2 = packet.ReadByte();
                string command = packet.ReadString();
                commandsLength += command.Length;
                commands.Add(new MacrosCmd(entry, (MacroType)((type < 1) || (type > 6) ? 0 : type), d1, d2, command));
            }

            macros = new MacrosModel() { UserMacrosId = id, Icon = icon, Name = name, Description = desc, Acronym = acronym, CommandArray = commands };
        }

        public override async Task Execute()
        {            
            if (commandsLength > 255)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.InvalidMacro));
            }

            if (_playerInstance.PlayerMacros().GetAllMacroses().Count >= 24)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.YouMayCreateUpTo24Macros));
                return;
            }
            
            if (name.Equals(string.Empty))
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.EnterTheMacroName));
                return;
            }

            if (desc.Length > 32)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.MacroDescriptionMax32Chars));
                return;
            }

            await _playerInstance.PlayerMacros().RegisterMacros(macros);
        }
    }
}
