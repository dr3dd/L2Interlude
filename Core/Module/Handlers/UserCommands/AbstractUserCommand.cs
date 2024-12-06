using Core.Module.Player;
using L2Logger;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 06.12.2024 12:52:30

namespace Core.Module.Handlers.UserCommands
{
    public abstract class AbstractUserCommand
    {
        internal async Task UseCommand(PlayerInstance player, int commandId)
        {

            LoggerManager.Debug($"{GetType().Name}: char {player.CharacterName} commandId {commandId}");

            await Use(player, commandId);
        }

        protected internal abstract Task Use(PlayerInstance admin, int commandId);
    }
}
