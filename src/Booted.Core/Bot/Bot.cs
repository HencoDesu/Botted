using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Plugins;
using Booted.Core.Plugins.Abstractions;
using Booted.Core.Providers;

namespace Booted.Core.Bot
{
	public class Bot : IBot
	{
		public IPluginManager PluginManager { get; }
		public ICommandManager CommandManager { get; }

		public Bot(IPluginManager pluginManager, ICommandManager commandManager)
		{
			PluginManager = pluginManager;
			CommandManager = commandManager;

			var pluginLoadingContext = new PluginLoadingContext();
			PluginManager.LoadAllPlugins(pluginLoadingContext);

			foreach (var command in pluginLoadingContext.Commands)
			{
				CommandManager.RegisterCommand(command);
			}

			foreach (var provider in pluginLoadingContext.Providers)
			{
				provider.MessageReceived += OnMessageReceived;
			}
		}

		private void OnMessageReceived(IProvider provider, BotMessage message)
		{
			var commandResult = CommandManager.TryExecuteCommand(message);

			var messageText = commandResult switch
			{
				CommandResult.OkCommandResult => commandResult.Text,
				CommandResult.ErrorCommandResult => $"Произошла ошибка: {commandResult.Text}",
				CommandResult.NoneCommandResult => "",
				_ => ""
			};

			if (!string.IsNullOrEmpty(messageText))
			{
				provider.SendMessage(new BotMessage
				{
					Provider = message.Provider,
					UserId = message.UserId,
					Text = messageText
				});
			}
		}
	}
}