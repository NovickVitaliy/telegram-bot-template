using TelegramBotTemplate.Models;

namespace TelegramBotTemplate.Managers.UserStates;

public interface IUserStateManager
{
    Task<UserState> GetStateForUser(long userTelegramId);
    Task SetStateForUser(long userTelegramId, UserState state);
    Task RemoveStateForUser(long userTelegramId);
}