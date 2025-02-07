# Telegram Bot .NET Template

## Description

This is a .NET template for creating Telegram bots quickly and efficiently. It provides a pre-configured structure with DI and console hosting included for faster and better development. Docker included :)

Link on the nuget.org: https://www.nuget.org/packages/TelegramBotConsoleDotNet/
## Features

- Easy setup and configuration
- Built-in support for handling updates and commands
- Logging and error handling included
- Environment variable management for secure token storage

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [Telegram Bot Token](https://core.telegram.org/bots#3-how-do-i-create-a-bot)
- Docker (optional, for containerized deployment)

## Installation
1. Pull the repository to local machine
   ```bash 
   git pull https://github.com/NovickVitaliy/telegram-bot-template
   ```
   or download it from the nuget.org
   ```bash 
   dotnet new install TelegramBotConsoleDotNet::1.0.0
   ```
2. **Install the template:**
Execute next command inside the core folder of the template
   ```bash
   dotnet new -i .
   ```

3. **Create a new bot project:**
   ```bash
   dotnet new telegram-bot-console -n YourBotName
   ```

4. **Navigate to your project directory:**
   ```bash
   cd YourBotName
   ```

5. **Configure your bot token:**
    - Set the `TELEGRAM_BOT_TOKEN` environment variable or create user secrets file or store it anywhere safe.

## Usage

### Running the Bot

1. **Run locally:**
   ```bash
   dotnet run
   ```

2. **Run with Docker:**
   ```bash
   docker build -t yourbotname .
   docker run -e TELEGRAM_BOT_TOKEN=your_token_here yourbotname
   ```

### Intro
Main concepts of this template that I created are:

- Commands
- State
- Callbacks

P.S: In the future template might be changed and new concepts will be added so this README will change accordingly 

## Commands
**Commands** are the messages sent by the users that start with `/`. Basically it is a telegram command.
 Commands can conduct some operations and transition users from one state to another using `IUserStateManager` interface. 

❗❗ `IUserStateManager` uses `IUserStateProvider` interface. Its purpose is to store `UserState` in different stores. By default 
the `InMemoryCacheUserStateProvider` is registered and it stores state inside the memory cache that is purged after every restart of the application. 
Make sure to implement `IUserStateProvider` with persistent store, like MongoDB, PostgreSQL, etc. Also it is worth mentioning that `UserState` 
should be stored within the User model defined by the developer for less database roundtrips.  ❗❗

### Adding New Commands

1. **Navigate to the `Handlers/Commands` folder.**
2. **Create a new class inheriting from `ICommandHandler`.**
3. **Specify the command that this handler can process via implementing the `bool CanHandle(string command)` method.**
4. **Implement `Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)` method.**

## Callbacks
**Callbacks** are the telegram callbacks used in the inline keyboard buttons. Whenever you define callback you should name it in a such way:
[callback-group]\_[callback-data]\_[some-additional?]. And when you choose the appropriate callback handler you 
must pass the callback-group value to the method `bool CanHandle(string callbackGroup)` in the `ICallbackHandler` interface.

? In this case means optional.

### Adding New Callback Handlers

1. **Navigate to the `Handlers/Callbacks` folder.**
2. **Create a new class inheriting from `ICallbackHandler`.**
3. **Specify the callback group that this handler can process via implementing the `bool CanHandle(string callbackGroup)` method.**
4. **Implement `Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)` method.**

## States

**States** is the concept I created myself. Basically I used them for managing flow of user interaction with bot without commands. 
I used it for cases when user has to type in some data. For example - user initiates new command `/start-input` which is then processed by the respective
`ICommandHandler`. This handler prints prompt to the user for some data and transitions the user to the state `EnteringData`. 
And when user types in some data and sends the message the main handler checks if user is in some state at the moment of sending the message.
If so it retrieves the `IStateHandler` that handles the request. If not it checks for callbacks and commands further. If none can be applied it simply finishes the execution.
In case if it was a command it will notify user that such a command does not exist.

### Adding New Callback Handlers

1. **Navigate to the `Handlers/States` folder.**
2. **Create a new class inheriting from `IUserStateHandler`.**
3. **Specify the callback group that this handler can process via implementing the `bool CanHandle(UserState state)` method.**
4. **Implement `Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)` method.**

### Logging and Error Handling

- Logging is configured using [Serilog](https://serilog.net/). You can adjust settings in `appsettings.json` and `Program.cs` for your needs.

##

Although it is inevitable that this template may not be full or even shitty in some cases im still proud of it cause it lets me 
develop bots faster with ease. Maybe it is just suitable for my needs, and it may be insufficient for bots with more complex business logic.

## Contact
s
For questions or support, please contact at novickvitaliy@gmail.com

