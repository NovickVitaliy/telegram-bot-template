using TelegramBotTemplate.Models;
using TelegramBotTemplate.Providers.States.UserStates;

namespace TelegramBotTemplate.Managers.UserStates;

public class UserStateManager : IUserStateManager
{
    private readonly IUserStateProvider _userStateProvider;
    
    public UserStateManager(IUserStateProvider userStateProvider)
    {
        _userStateProvider = userStateProvider;
    }
    
    public Task<UserState> GetStateForUser(long userTelegramId)
    {
        return _userStateProvider.GetStateForUser(userTelegramId);
    }
    
    public Task SetStateForUser(long userTelegramId, UserState state)
    {
        return _userStateProvider.SetStateForUser(userTelegramId, state);
    }
    
    public Task RemoveStateForUser(long userTelegramId)
    {
        return _userStateProvider.SetStateForUser(userTelegramId, UserState.None);
    }
}