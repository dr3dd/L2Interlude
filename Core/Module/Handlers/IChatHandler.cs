using Core.Enums;
using Core.Module.Player;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:13:11

namespace Core.Module.Handlers
{
    public interface IChatHandler
    {
        Task Chat(PlayerInstance player, ChatType type, string target, string text);

        void Register(object processor);
    }
}
