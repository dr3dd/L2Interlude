using Core.Attributes;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 06.12.2024 13:00:48

namespace Core.Module.Handlers.UserCommands
{

    [Command(CommandId = 0, CommandDescription="/loc")]
    class LocCommand : AbstractUserCommand
    {
        protected internal override async Task Use(PlayerInstance player, int commandId)
        {
            //TODO get message by param 'restart_point_begin/loc_name' from settings.txt 
            SystemMessage systemMessage = new SystemMessage(SystemMessageId.LocNeutralZoneS1S2S3);
            systemMessage.AddNumber(player.GetX());
            systemMessage.AddNumber(player.GetY());
            systemMessage.AddNumber(player.GetZ());
            await player.SendPacketAsync(systemMessage);
        }
    }
}
