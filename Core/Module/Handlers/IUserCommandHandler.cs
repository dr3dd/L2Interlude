using Core.Module.Player;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 06.12.2024 10:20:02

namespace Core.Module.Handlers
{
    public interface IUserCommandHandler
    {
        Task Request(PlayerInstance player, int commandId);

        void Register(object processor);
    }
}
