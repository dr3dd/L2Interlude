using Core.Module.Player;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:13:11

namespace Core.Controller.Handlers
{
    public interface IChatHandler
    {
        void Chat(PlayerInstance player, ChatType type, string target, string text);

        void Register(object processor);
    }
}
