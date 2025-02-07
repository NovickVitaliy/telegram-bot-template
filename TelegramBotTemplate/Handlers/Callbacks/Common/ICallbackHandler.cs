using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTemplate.Handlers.Callbacks.Common;

public interface ICallbackHandler
{
    bool CanHandle(string callbackGroup);

    Task Handle(ITelegramBotClient client, Update update, CancellationToken cancellationToken);
}