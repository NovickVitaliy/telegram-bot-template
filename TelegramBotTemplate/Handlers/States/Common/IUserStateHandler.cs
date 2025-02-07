using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTemplate.Models;

namespace TelegramBotTemplate.Handlers.States.Common;

public interface IUserStateHandler
{
    bool CanHandle(UserState state);

    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}