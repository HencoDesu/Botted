using System;
using System.Collections.Generic;
using System.Linq;
using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Events;
using Booted.Core.Events.Abstractions;
using Booted.Core.Events.EventData;
using Booted.Core.Providers.Abstractions;
using Pidgin;
using Parser = Booted.Core.Commands.Parser;

namespace Booted.Core.Bot
{
	public class Bot
	{
		private readonly IEnumerable<ICommand> _commands;
		
		public IEventService EventService { get; }

		public Bot(IEventService eventService, IEnumerable<ICommand> commands, IEnumerable<IProvider> providers)
		{
			_commands = commands;
			EventService = eventService;
			EventService.Subscribe<MessageReceived, BotMessage>(OnMessageReceived);
		}

		private void OnMessageReceived(BotMessage message)
		{
			var commandName = Parser.CommandName.ParseOrThrow(message.Text);
			var command = _commands.FirstOrDefault(c => c.Name == commandName);
			
			if (command is null || !command.ProviderLimitation.IsMatching(message.Provider))
			{
				return;
			}

			try
			{
				var data = command.Structure.ParseData(message);
				var result = command.Execute(data);
				OnCommandExecuted(result, message);
			} catch (Exception e)
			{
				OnCommandExecuted(CommandResult.Error(e.Message), message);
			}
		}

		private void OnCommandExecuted(ICommandResult commandResult, BotMessage inputMessage)
		{
			var finalMessage = new BotMessage
			{
				Provider = inputMessage.Provider,
				Text = commandResult.Text,
				UserId = inputMessage.UserId
			};
			EventService.Raise<NeedSendMessage, BotMessage>(finalMessage);
		}
	}
}