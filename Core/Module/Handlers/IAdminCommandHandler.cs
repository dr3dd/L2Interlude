using Core.Module.Player;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:00:40

namespace Core.Module.Handlers
{
    public interface IAdminCommandHandler
    {
        Task Request(PlayerInstance admin, string alias);

        void Register(object processor);
    }
}
