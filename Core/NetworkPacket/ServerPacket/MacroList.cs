using Core.Module.Player.Macroses;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.10.2024 23:51:08

namespace Core.NetworkPacket.ServerPacket
{
    internal class MacroList : Network.ServerPacket
    {
        private int _count;
        private MacrosModel _macros;
        private MacroUpdateType _updateType;

        public MacroList(int count, MacrosModel macros, MacroUpdateType updateType)
        {
            _count = count;
            _macros = macros;
            _updateType = updateType;
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xe7);
            await WriteByteAsync((byte)_updateType);
            await WriteIntAsync(_updateType != MacroUpdateType.LIST ? _macros.UserMacrosId : 0); // modified, created or deleted macro's id
            await WriteByteAsync(_count);
            await WriteByteAsync(_macros != null);

            if (_macros != null && (_updateType != MacroUpdateType.DELETE))
            {
                await WriteIntAsync(_macros.UserMacrosId); // Macro ID
                await WriteStringAsync(_macros.Name); // Macro Name
                await WriteStringAsync(_macros.Description); // Desc
                await WriteStringAsync(_macros.Acronym); // acronym
                await WriteByteAsync(_macros.Icon); // icon
                await WriteByteAsync(_macros.CommandArray.Count); // count
                int i = 0;
                foreach (MacrosCmd cmd in _macros.CommandArray)
                {
                    await WriteByteAsync(++i); // command count
                    await WriteByteAsync((byte)cmd.type); // type 1 = skill, 3 = action, 4 = shortcut
                    await WriteIntAsync(cmd.d1); // skill id
                    await WriteByteAsync(cmd.d2); // shortcut id
                    await WriteStringAsync(cmd.cmd); // command name
                }
            }
            
        }
    }
}
