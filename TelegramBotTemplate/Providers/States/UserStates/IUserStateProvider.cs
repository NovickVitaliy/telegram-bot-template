using TelegramBotTemplate.Models;

namespace TelegramBotTemplate.Providers.States.UserStates;

public interface IUserStateProvider
{
    Task<UserState> GetStateForUser(long userTelegramId);
    Task SetStateForUser(long userTelegramId, UserState state);
}