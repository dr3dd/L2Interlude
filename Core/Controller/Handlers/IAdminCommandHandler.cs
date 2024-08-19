using Core.Module.Player;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:00:40

namespace Core.Controller.Handlers
{
    public interface IAdminCommandHandler
    {
        void Request(PlayerInstance admin, string alias);

        void Register(object processor);
    }
}
