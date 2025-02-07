using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;
using TelegramBotTemplate.BackgroundTasks;
using TelegramBotTemplate.Exceptions;
using TelegramBotTemplate.Handlers;
using TelegramBotTemplate.Handlers.Callbacks.Common;
using TelegramBotTemplate.Handlers.Commands.Common;
using TelegramBotTemplate.Handlers.States.Common;
using TelegramBotTemplate.Managers.UserStates;
using TelegramBotTemplate.Providers.States.UserStates;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

await Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("appsettings.json", optional: false);
        builder.AddUserSecrets(typeof(Program).Assembly);
        builder.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var botToken = context.Configuration["TelegramBotToken"] ?? throw new TelegramBotTokenNotFoundException();
        services.AddSingleton<IUserStateProvider, InMemoryCacheUserStateProvider>();
        services.AddSingleton<IUserStateManager, UserStateManager>();
        services.AddMemoryCache();
        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(botToken));
        services.AddSingleton<UpdateHandler>();
        services.AddHostedService<BotHostedService>();

        services.Scan(scan => scan.FromAssemblyOf<IUserStateHandler>()
            .AddClasses(classes => classes.AssignableTo<IUserStateHandler>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.Scan(scan => scan.FromAssemblyOf<ICommandHandler>()
            .AddClasses(classes => classes.AssignableTo<ICommandHandler>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.Scan(x => x.FromAssemblyOf<ICallbackHandler>()
            .AddClasses(classes => classes.AssignableTo<ICallbackHandler>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        
        
    })
    .RunConsoleAsync();