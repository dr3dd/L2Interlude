using Core.Module.CharacterData;

namespace Core.Module.Player;

public interface IPlayerBaseStatus : ICharacterBaseStatus
{
    int GetMaxCp();
    int GetMaxLoad();
    int GetCurrentLoad();
}